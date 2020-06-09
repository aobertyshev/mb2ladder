import { Component, OnInit, Input } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { RegisterComponent } from '../register/register.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {

  @Input() loginModalController: ModalController;

  constructor() { }

  ngOnInit() { }

  async register() {
    const modal = await this.loginModalController.create({
      component: RegisterComponent,
      cssClass: 'my-custom-class',
      componentProps: {
        registerModalController: this.loginModalController
      }
    });
    return await modal.present();
  }

}

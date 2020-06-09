import { Component, OnInit, Input } from '@angular/core';
import { ModalController } from '@ionic/angular';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {

  @Input() registerModalController: ModalController;

  constructor() { }

  ngOnInit() { }

  async register() {
    this.registerModalController.dismiss();
  }

}

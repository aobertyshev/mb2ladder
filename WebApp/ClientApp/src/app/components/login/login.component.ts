import { Component, OnInit, Input } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { RegisterComponent } from '../register/register.component';
import { AuthService } from 'src/app/services/auth/auth.service';
import { Login } from 'src/app/models/login';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {

  @Input() loginModalController: ModalController;
  loginForm: FormGroup;
  details: Login;

  constructor(private readonly _authService: AuthService, private readonly formBuilder: FormBuilder) { }

  async ngOnInit() {
    this.loginForm = this.formBuilder.group({
      email: [undefined, Validators.compose([
        Validators.required, Validators.email])],
      password: [undefined, Validators.required]
    });
  }

  async signIn(): Promise<any> {
    if (this.loginForm.valid) {
      this.details = this.loginForm.value;
      const response = await this._authService.signIn(this.details);
      if (response.status === 200) {
        this.loginModalController.dismiss();
      }
    }
  }

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

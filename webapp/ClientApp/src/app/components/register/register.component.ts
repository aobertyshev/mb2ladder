import { Component, OnInit, Input } from '@angular/core';
import { ModalController, ToastController } from '@ionic/angular';
import { AuthService } from 'src/app/services/auth/auth.service';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { Register } from 'src/app/models/register';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  details: Register;

  @Input() registerModalController: ModalController;

  constructor(private readonly _authService: AuthService, private readonly toastController: ToastController,
    private readonly formBuilder: FormBuilder) { }

  async ngOnInit() {
    this.registerForm = this.formBuilder.group({
      email: [undefined, Validators.compose([
        Validators.required, Validators.email])],
      password: [undefined, Validators.required],
      passwordRepeat: [undefined, Validators.required],
      nick: [undefined, Validators.required],
      clanName: [undefined],
      region: [{ value: 'EU', disabled: true }],
      discord: [undefined, Validators.required]
    });
  }


  async register() {
    if (this.registerForm.valid) {
      if (this.registerForm.value.password !== this.registerForm.value.passwordRepeat) {
        const toast = await this.toastController.create({
          message: 'Passwords aren\'t equal',
          duration: 2000
        });
        toast.present();
        return;
      }
      this.details = this.registerForm.value;
      delete this.details['passwordRepeat'];
      try {
        await this._authService.register(this.details);
        this.registerModalController.dismiss();
      } catch (e) {
        if (e.status === 409) {
          const toast = await this.toastController.create({
            message: 'User with this email already exists.',
            duration: 2000
          });
          toast.present();
        }
      }
    }
  }
}

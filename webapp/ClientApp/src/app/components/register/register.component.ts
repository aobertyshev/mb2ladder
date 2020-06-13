import { Component, OnInit, Input } from '@angular/core';
import { ModalController, ToastController } from '@ionic/angular';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {

  @Input() registerModalController: ModalController;

  constructor(private readonly _authService: AuthService, private readonly toastController: ToastController) { }

  ngOnInit() { }

  async register() {
    try {
      await this._authService.register({
        email: 'test@',
        nick: 'Helix',
        password: 'test'
      });
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

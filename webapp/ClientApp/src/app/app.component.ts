import { Component, OnInit } from '@angular/core';

import { Platform, ModalController } from '@ionic/angular';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { StatusBar } from '@ionic-native/status-bar/ngx';
import { LoginComponent } from './components/login/login.component';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss']
})
export class AppComponent implements OnInit {
  public selectedIndex = 0;
  public appPages = [
    {
      title: 'Home',
      url: '/',
      icon: 'home'
    },
    {
      title: 'Matches',
      url: '/matches',
      icon: 'rocket'
    },
    {
      title: 'Players',
      url: '/players',
      icon: 'person'
    },
    {
      title: 'Teams',
      url: '/teams',
      icon: 'people'
    },
  ];

  constructor(
    private platform: Platform,
    private splashScreen: SplashScreen,
    private statusBar: StatusBar,
    private readonly modalController: ModalController
  ) {
    this.initializeApp();
  }

  initializeApp() {
    this.platform.ready().then(() => {
      this.statusBar.styleDefault();
      this.splashScreen.hide();
    });
  }

  ngOnInit() {
    const component = window.location.pathname.split('/')[1].toLowerCase();
    if (!!component) {
      this.selectedIndex = this.appPages.findIndex(page => page.title.toLowerCase() === component);
    }
  }

  async presentModal() {
    const modal = await this.modalController.create({
      component: LoginComponent,
      cssClass: 'my-custom-class',
      componentProps: {
        loginModalController: this.modalController
      }
    });
    return await modal.present();
  }
}

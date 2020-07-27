import { Component, OnInit } from '@angular/core';

import { Platform, ModalController } from '@ionic/angular';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { StatusBar } from '@ionic-native/status-bar/ngx';
import { LoginComponent } from './components/login/login.component';
import { AuthService } from './services/auth/auth.service';
import { ToolsService } from './services/tools/tools.service';
import { QueueService } from './services/queue/queue.service';

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
      icon: 'home',
      disabled: false,
    },
    {
      title: 'Matches',
      url: '/matches',
      icon: 'rocket',
      disabled: false,
    },
    {
      title: 'Players',
      url: '/players',
      icon: 'person',
      disabled: false,
    },
    {
      title: 'Teams',
      url: '/teams',
      icon: 'people',
      disabled: true,
    },
  ];

  constructor(
    private platform: Platform,
    private splashScreen: SplashScreen,
    private statusBar: StatusBar,
    private readonly modalController: ModalController,
    private readonly auth: AuthService,
    private readonly tools: ToolsService,
    private readonly queue: QueueService
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

  async joinQueue(): Promise<any> {
    await this.queue.joinQueue();
  }
}

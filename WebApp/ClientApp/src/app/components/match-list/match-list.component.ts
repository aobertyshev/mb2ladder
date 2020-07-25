import { Component, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { CreateMatchComponent } from '../create-match/create-match.component';

@Component({
  selector: 'app-match-list',
  templateUrl: './match-list.component.html',
  styleUrls: ['./match-list.component.scss'],
})
export class MatchListComponent implements OnInit {

  constructor(private readonly modalController: ModalController) { }

  async ngOnInit() {

  }

  async createMatch() {
    const modal = await this.modalController.create({
      component: CreateMatchComponent,
      cssClass: 'my-custom-class',
      componentProps: {
        createMatchController: this.modalController
      }
    });
    return await modal.present();
  }

}

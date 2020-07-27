import { Component, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { CreateMatchComponent } from '../create-match/create-match.component';
import { MatchService } from 'src/app/services/match/match.service';
import { Match } from 'src/app/models/match';

@Component({
  selector: 'app-match-list',
  templateUrl: './match-list.component.html',
  styleUrls: ['./match-list.component.scss'],
})
export class MatchListComponent implements OnInit {
  loaded = false;
  matches: Array<Match>;

  constructor(
    private readonly modalController: ModalController,
    private readonly matchService: MatchService) { }

  async ngOnInit() {
    this.matches = await this.matchService.getMatches();
    this.loaded = true;
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

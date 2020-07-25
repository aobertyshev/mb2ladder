import { Component, OnInit } from '@angular/core';
import { PlayerService } from 'src/app/services/player/player.service';
import { Player } from 'src/app/models/player';

@Component({
  selector: 'app-player-list',
  templateUrl: './player-list.component.html',
  styleUrls: ['./player-list.component.scss'],
})
export class PlayerListComponent implements OnInit {
  loaded = false;
  players: Array<Player>;

  constructor(private readonly _playerService: PlayerService) { }

  async ngOnInit() {
    this.players = (await this._playerService.getPlayerList()).body;
    this.loaded = true;
  }

}

import { Component, OnInit, Input } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { PlayerService } from 'src/app/services/player/player.service';
import { Player } from 'src/app/models/register/player';

@Component({
  selector: 'app-create-match',
  templateUrl: './create-match.component.html',
  styleUrls: ['./create-match.component.scss'],
})
export class CreateMatchComponent implements OnInit {

  @Input() createMatchController: ModalController;
  createMatchForm: FormGroup;
  players: Array<Player>;
  playersInMatch: Array<Player>;

  constructor(private readonly formBuilder: FormBuilder, private readonly playerService: PlayerService) { }

  async ngOnInit() {
    this.playersInMatch = [];
    this.createMatchForm = this.formBuilder.group({
      dateTime: [undefined, Validators.compose([
        Validators.required, Validators.email])],
      type: [{ value: 'PUG', disabled: true }, Validators.required],
      playerSearch: ['']
    });
    this.players = await (await this.playerService.getPlayerList()).body;
  }

  async createMatch() {

  }

  shouldShowItem(value: string): boolean {
    return value.toLowerCase().includes(this.createMatchForm.value.playerSearch.toLowerCase());
  }

  addPlayer(player: Player) {
    this.playersInMatch.push(player);
  }

  playerAlreadyInMatch(player: Player): boolean {
    return this.playersInMatch.indexOf(player) !== -1;
  }

  deletePlayerFromMatch(player: Player) {
    var index = this.playersInMatch.indexOf(player);
    if (index !== -1) {
      this.playersInMatch.splice(index, 1);
    }
  }

}

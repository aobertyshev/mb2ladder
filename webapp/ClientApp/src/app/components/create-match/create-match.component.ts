import { Component, OnInit, Input } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { PlayerService } from 'src/app/services/player/player.service';
import { Player } from 'src/app/models/player';
import { Team } from 'src/app/models/team';
import { Match } from 'src/app/models/match';
import { ToolsService } from 'src/app/services/tools/tools.service';
import { MatchService } from 'src/app/services/match/match.service';

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
  unselectedPlayers: Array<Player>;
  teams: Array<Team>;

  constructor(private readonly formBuilder: FormBuilder, private readonly playerService: PlayerService, private readonly tools: ToolsService, private readonly matchService: MatchService) { }

  async ngOnInit() {
    this.teams = [{ id: undefined, playerIds: [] }, { id: undefined, playerIds: [] }];
    this.playersInMatch = [];
    this.unselectedPlayers = [];
    this.createMatchForm = this.formBuilder.group({
      dateTime: [undefined, Validators.compose([
        Validators.required, Validators.email])],
      type: [{ value: 'PUG', disabled: true }, Validators.required],
      playerSearch: ['']
    });
    this.players = await (await this.playerService.getPlayerList()).body;
  }

  async createMatch() {
    if (this.createMatchForm.valid /*&& this.playersInMatch.length === 10*/) {
      const now = this.tools.convertDateToUTC(new Date());
      const match: Match = {
        id: undefined,
        date: this.createMatchForm.value.dateTime,
        dateCreated: now,
        dateUpdated: now,
        // xd
        maps: ['mb2_dotf', 'mb2_lunarbase'],
        score: '',
        teams: []
      }
      await this.matchService.createMatch(match);
    }
  }

  shuffle() {
    this.teams.forEach((team) => team.playerIds.forEach((playerId) => this.unselectedPlayers.push(playerId)));
    this.teams[0].playerIds = [];
    this.teams[1].playerIds = [];
    this.unselectedPlayers.forEach((player) => {
      //doesnt work with Ids - todo fixme
      const teamIndex = (Math.random() >= 0.5 && this.teams[0].playerIds.length < 5) ? 0 : 1;
      this.teams[teamIndex].playerIds.push(player);
    });
    this.unselectedPlayers = [];
  }

  shouldShowItem(value: string): boolean {
    return value.toLowerCase().includes(this.createMatchForm.value.playerSearch.toLowerCase());
  }

  addPlayer(player: Player) {
    this.playersInMatch.push(player);
    this.unselectedPlayers.push(player);
  }

  playerAlreadyInMatch(player: Player): boolean {
    return this.playersInMatch.indexOf(player) !== -1;
  }

  deletePlayerFromMatch(player: Player) {
    let index = this.playersInMatch.indexOf(player);
    if (index !== -1) {
      this.playersInMatch.splice(index, 1);
    }

    index = this.teams[0].playerIds.indexOf(player);
    if (index !== -1) {
      this.teams[0].playerIds.splice(index, 1);
    }

    index = this.teams[1].playerIds.indexOf(player);
    if (index !== -1) {
      this.teams[1].playerIds.splice(index, 1);
    }

    index = this.unselectedPlayers.indexOf(player);
    if (index !== -1) {
      this.unselectedPlayers.splice(index, 1);
    }
  }

  getPlayerById(id: string): Player {
    return this.players.find((player) => player.id === id);
  }
}

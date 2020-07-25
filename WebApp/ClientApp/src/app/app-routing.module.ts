import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { MatchComponent } from './components/match/match.component';
import { MatchListComponent } from './components/match-list/match-list.component';
import { PlayerListComponent } from './components/player-list/player-list.component';
import { PlayerComponent } from './components/player/player.component';
import { HomeComponent } from './components/home/home.component';
import { TeamListComponent } from './components/team-list/team-list.component';
import { TeamComponent } from './components/team/team.component';

const routes: Routes = [
  {
    path: '',
    // redirectTo: 'home',
    component: HomeComponent
  },
  {
    path: 'matches',
    component: MatchListComponent
  },
  {
    path: 'matches/:id',
    component: MatchComponent
  },
  {
    path: 'players',
    component: PlayerListComponent
  },
  {
    path: 'players/:id',
    component: PlayerComponent
  },
  {
    path: 'teams',
    component: TeamListComponent
  },
  {
    path: 'teams/:id',
    component: TeamComponent
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }

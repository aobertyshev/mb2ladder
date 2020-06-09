import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { MatchComponent } from './components/match/match.component';
import { MatchListComponent } from './components/match-list/match-list.component';
import { PlayerListComponent } from './components/player-list/player-list.component';
import { PlayerComponent } from './components/player/player.component';
import { HomeComponent } from './components/home/home.component';

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
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }

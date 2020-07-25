import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-match',
  templateUrl: './match.component.html',
  styleUrls: ['./match.component.scss'],
})
export class MatchComponent implements OnInit {
  matchId: number;

  constructor(private readonly activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.matchId = parseInt(this.activatedRoute.snapshot.paramMap.get('id'));
  }

}

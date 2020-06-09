import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-pug',
  templateUrl: './pug.component.html',
  styleUrls: ['./pug.component.scss'],
})
export class PugComponent implements OnInit {

  pugId: string;
  constructor(private readonly activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.pugId = this.activatedRoute.snapshot.paramMap.get('id');
  }

}

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './TrafficMain.component.html',
})
export class TrafficMainComponent implements OnInit {
  constructor(private route: ActivatedRoute) { }
  _path = "";
  ngOnInit(): void {
    this._path = this.route.snapshot["_routerState"].url;
  }
}

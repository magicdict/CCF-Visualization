import { Component, OnInit} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
declare var $: any;

@Component({
  selector: 'app-root',
  templateUrl: './TrafficMain.component.html',
})
export class TrafficMainComponent implements OnInit {
  constructor(private route: ActivatedRoute) { }
  _path = "";

  ngOnInit() {
    this._path = this.route.snapshot["_routerState"].url;
    $(document).ready(() => {
      const trees: any = $('[data-widget="tree"]');
      if (trees) {
        trees.tree();
      }
    });
  }
}

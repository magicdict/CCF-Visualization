import { Component, OnInit} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
declare var $: any;

@Component({
  templateUrl: './SecurityMain.component.html',
})
export class SecurityMainComponent implements OnInit {
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

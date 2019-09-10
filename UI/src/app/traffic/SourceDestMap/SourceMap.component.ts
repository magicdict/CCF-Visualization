import { Component, OnInit } from '@angular/core';
import { CommonFunction } from 'src/app/Common/common';
import { ActivatedRoute } from '@angular/router';
import { MapValue } from '../Model';
import { IMapStardard } from 'src/app/Common/chartOption';


@Component({
  templateUrl: './SourceMap.component.html',
})
export class SourceMapComponent implements OnInit {
  constructor(private route: ActivatedRoute) { }
  _commonFunction = CommonFunction;
  _map = CommonFunction.clone(IMapStardard);
  ngOnInit(): void {
    this.route.data
      .subscribe((xxx: { data: MapValue[] }) => {
        this._map.series[0].data = xxx.data;
        this._map.series[1].data = xxx.data.slice(0, 6);
        this._map.title.text = "";
     });
  }
}


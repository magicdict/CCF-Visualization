import { Component, OnInit } from '@angular/core';
import { CommonFunction } from 'src/app/Common/common';
import { ActivatedRoute } from '@angular/router';
import { MapValue } from '../Model';
import { IMapStardard, ILinesItem } from 'src/app/Common/chartOption';


@Component({
  templateUrl: './TraceMap.component.html',
})
export class TraceMapComponent implements OnInit {
  constructor(private route: ActivatedRoute) { }
  _commonFunction = CommonFunction;
  _map = {
    series: []
  };
  _title = "轨迹分析";
  ngOnInit(): void {
    this.route.data
      .subscribe((xxx: { data: [][] }) => {
        this._map['bmap'] = CommonFunction.clone(IMapStardard.bmap);
        let lines = CommonFunction.clone(ILinesItem);
        lines.data = xxx.data;
        this._map.series.push(lines);
      });
  }
}


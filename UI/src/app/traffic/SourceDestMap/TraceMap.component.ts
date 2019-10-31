import { Component, OnInit } from '@angular/core';
import { CommonFunction } from 'src/app/Common/common';
import { ActivatedRoute } from '@angular/router';
import { ITraceItem } from '../Model';
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
      .subscribe((xxx: { data: ITraceItem[] }) => {
        let bmapoption = CommonFunction.clone(IMapStardard.bmap);
        bmapoption.zoom = 13;
        this._map['bmap'] = bmapoption;
        let lines = CommonFunction.clone(ILinesItem);
        lines.data = xxx.data.map(this.convertTrace);
        this._map.series.push(lines);
      });
  }

  convertTrace(inItem: ITraceItem): ITraceItem {
    var color = inItem.lineStyle.width > 8 ? "#FF0080" : (inItem.lineStyle.width > 7 ? "#2E2EFE" : "#74DF00");
    var outItem = {
      coords: inItem.coords,
      lineStyle: {
        width: 2,
        color: color
      }
    }
    return outItem;
  }


}


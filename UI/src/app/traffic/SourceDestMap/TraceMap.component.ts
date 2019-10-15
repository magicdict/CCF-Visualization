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
        this._map['bmap'] = CommonFunction.clone(IMapStardard.bmap);
        let lines = CommonFunction.clone(ILinesItem);
        lines.data = xxx.data.map(this.convertTrace);
        this._map.series.push(lines);
      });
  }

  convertTrace(inItem: ITraceItem): ITraceItem {
    var color = inItem.lineStyle.width > 8 ? "red":"blue";
    var outItem = {
      coords: inItem.coords,
      lineStyle: {
        width: inItem.lineStyle.width - 5,
        color:color
      }
    }
    return outItem;
  }


}


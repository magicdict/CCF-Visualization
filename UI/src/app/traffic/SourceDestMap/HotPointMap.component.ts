import { Component, OnInit } from '@angular/core';
import { CommonFunction } from 'src/app/Common/common';
import { ActivatedRoute } from '@angular/router';
import { IPointAttr, MapValue } from '../Model';
import { IMapStardard } from 'src/app/Common/chartOption';


@Component({
  templateUrl: './SimpleMap.component.html',
})
export class HotPointMapComponent implements OnInit {
  constructor(private route: ActivatedRoute) { }
  _commonFunction = CommonFunction;
  _map = CommonFunction.clone(IMapStardard);
  _title = "";
  ngOnInit(): void {
    this._title = "热门地点分析";
    this.route.data
      .subscribe((xxx: { data: IPointAttr[] }) => {
        this._map.series[0].data = xxx.data.map(this.convertToMapValue);
        this._map.series[0].symbolSize = this.symbolSize;
        this._map.tooltip.formatter = this.tooltip;
      });
  }
  symbolSize(val: any) {
    return Math.log(val[2]);
  };
  tooltip(val: any) {
    return "作为出发地:" + val.data.value[3] + "<br />" +
      "作为目的地:" + val.data.value[4] + "<br />" +
      "运行时间:" + val.data.value[5] + "<br />" +
      "运行距离:" + val.data.value[6] + "<br />" +
      "等待时间:" + val.data.value[7];
  }
  convertToMapValue(x: IPointAttr) {
    var m = {
      name: "hotpoint",
      value: [x.lng,
      x.lat,
      x.StartCount + x.DestCount,
      x.StartCount,
      x.DestCount,
      x.NormalTime,
      x.Distance,
      x.WatiTime]
    }
    return m;
  }
}


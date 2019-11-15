import { Component, OnInit } from '@angular/core';
import { CommonFunction } from 'src/app/Common/common';
import { ActivatedRoute } from '@angular/router';
import { MapValue } from '../Model';
import { IMapStardard } from 'src/app/Common/chartOption';


@Component({
  templateUrl: './SimpleMap.component.html',
})
export class SimpleMapComponent implements OnInit {
  constructor(private route: ActivatedRoute) { }
  _commonFunction = CommonFunction;
  _map = CommonFunction.clone(IMapStardard);
  _title = "";
  ngOnInit(): void {

    switch (this.route.snapshot.routeConfig.path) {
      case "simplesourcemap":
        this._title = "出发地分析";
        break;
      case "simpledestmap":
        this._title = "目的地分析";
        break;
      case "destpointfromairport":
        this._title = "机场出发目的地";
        this._map.bmap.zoom = 13;
        break;
      case "startpointtoairport":
        this._title = "去机场出发地";
        this._map.bmap.zoom = 13;
        break;
      case "destpointfromtrain":
        this._title = "火车站出发目的地";
        this._map.bmap.zoom = 13;
        break;
      case "startpointtotrain":
        this._title = "去火车站出发地";
        this._map.bmap.zoom = 13;
        break;
      default:
        break;
    }


    this.route.data
      .subscribe((xxx: { data: MapValue[] }) => {
        xxx.data.sort((x, y) => { return y.value[2] - x.value[2] })
        this._map.series[0].data = xxx.data.slice(0, 2000);
        this._map.series[0].symbolSize = this.symbolSize;
        this._map.tooltip.formatter = this.tooltip;
        this._map.series[1].data = xxx.data.slice(0, 6);
        this._map.series[1].symbolSize = this.symbolSize;
        this._map.title.text = "";
      });
  }
  symbolSize(val: any) {
    return Math.sqrt(val[2] * 100) / 30;
  };
  tooltip(val: any) {
    return val.data.name + ":" + val.data.value[2];
  }
}


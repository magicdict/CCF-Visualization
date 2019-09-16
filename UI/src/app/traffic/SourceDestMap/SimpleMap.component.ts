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
  ngOnInit(): void {
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


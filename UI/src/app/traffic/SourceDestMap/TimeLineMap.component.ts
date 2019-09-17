import { Component, OnInit } from '@angular/core';
import { CommonFunction } from 'src/app/Common/common';
import { ActivatedRoute } from '@angular/router';
import { MapValue } from '../Model';
import { IMapStardard, ITimelineStardard } from 'src/app/Common/chartOption';

@Component({
  templateUrl: './TimeLineMap.component.html',
})
export class TimeLineMapComponent implements OnInit {
  constructor(private route: ActivatedRoute) { }
  _commonFunction = CommonFunction;
  _map_timeline = CommonFunction.clone(ITimelineStardard);
  _title = "";
  ngOnInit(): void {
    if (this.route.snapshot.routeConfig.path === "sourcemap") {
      this._title = "出发地分析";
    } else {
      this._title = "目的地分析";
    }
    this.route.data
      .subscribe((xxx: { data: MapValue[] }) => {
        xxx.data.sort((x, y) => { return y.value[2] - x.value[2] })
        this._map_timeline.baseOption.timeline.playInterval = 5000;
        this._map_timeline.baseOption['bmap'] = CommonFunction.clone(IMapStardard.bmap);
        this._map_timeline.baseOption.series.push(CommonFunction.clone(IMapStardard.series[0]));
        this._map_timeline.baseOption.tooltip.formatter = this.tooltip;
        this._map_timeline.baseOption.series[0].data = [];
        this._map_timeline.baseOption.series[0].symbolSize = this.symbolSize;
        this._map_timeline.baseOption.timeline.label.formatter = (x: number) => x.toString();
        for (let hour = 0; hour < 24; hour++) {
          this._map_timeline.baseOption.timeline.data.push(hour); //这里如果是toString则会发生错误...
          this._map_timeline.options.push(
            {
              title: {
                text: hour.toString() + "点"
              },
              series: [{ data: xxx.data.filter(x => x.hour === hour).map(x => { return { name: x.name, value: x.value } }) }]
            }
          )
        }
      });
  }

  symbolSize(val: any) {
    return Math.sqrt(val[2] * 100) / 10;
  };
  tooltip(val: any) {
    return val.data.name + ":" + val.data.value[2];
  }
}


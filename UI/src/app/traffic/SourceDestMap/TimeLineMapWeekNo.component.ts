import { Component, OnInit } from '@angular/core';
import { CommonFunction } from 'src/app/Common/common';
import { ActivatedRoute } from '@angular/router';
import { MapValue } from '../Model';
import { IMapStardard, ITimelineStardard } from 'src/app/Common/chartOption';

@Component({
  templateUrl: './TimeLineMap.component.html',
})
export class TimeLineMapWeekNoComponent implements OnInit {
  constructor(private route: ActivatedRoute) { }
  _commonFunction = CommonFunction;
  _map_timeline = CommonFunction.clone(ITimelineStardard);
  _title = "";
  ngOnInit(): void {
    if (this.route.snapshot.routeConfig.path === "weeklysourcemap") {
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
        this._map_timeline.baseOption.timeline.label.formatter = this.timelinelabel;

        let weeknos = xxx.data.map(item => item.weekno)
          .filter((value, index, self) => self.indexOf(value) === index);
        weeknos.sort();
        weeknos.pop();  //10-30数据不足，去掉
        weeknos.forEach(
          weekno => {
            //如果是数字的话，时间轴将按照数字的位置排列！！间隔也会按照数字间隔来排列！！
            var strWeekno = weekno.toString();
            strWeekno = strWeekno.substr(0, 4) + "-" + strWeekno.substr(4, 2) + "-" + strWeekno.substr(6, 2);
            this._map_timeline.baseOption.timeline.data.push(strWeekno);
            this._map_timeline.options.push(
              {
                title: {
                  text: strWeekno
                },
                series: [{ data: xxx.data.filter(x => x.weekno === weekno).map(x => { return { name: x.name, value: x.value } }) }]
              }
            )
          }
        );
      });
  }

  timelinelabel(s: any) {
    //这里由于输入的数据是 yyyy-MM-dd 的格式，则被转为日期，日期在JS里是数字，同时月份是0开始的
    return ((new Date(s)).getMonth() + 1) + "-" + (new Date(s)).getDate();
  }

  symbolSize(val: any) {
    return Math.sqrt(val[2] * 100) / 10;
  };
  tooltip(val: any) {
    return val.data.name + ":" + val.data.value[2];
  }
}


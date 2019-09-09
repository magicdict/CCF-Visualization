import { Component, OnInit } from '@angular/core';
import { CommonFunction } from 'src/app/Common/common';
import { I3DarStardard } from 'src/app/Common/chartOption';
import { ActivatedRoute } from '@angular/router';
import { ITimeAnaysis } from '../Model';


@Component({
  selector: 'app-root',
  templateUrl: './TimeAnalysis.component.html',
})
export class TimeAnalysisComponent implements OnInit {
  constructor(private route: ActivatedRoute) { }
  _timeanaysis: ITimeAnaysis;
  _traffic_3d = CommonFunction.clone(I3DarStardard);
  _commonFunction = CommonFunction;
  ngOnInit(): void {
    this.route.data
      .subscribe((xxx: { data: ITimeAnaysis }) => {
        this._timeanaysis = xxx.data;
        //对3D时间进行填充  
        this.Fill3DTime();

      });
  }

  Fill3DTime() {
    //X:具体时间，Y：日期，Z：流量
    var weekday = ["周一", "周二", "周三", "周四", "周五", "周六", "周日"];
    var time = [];
    this._timeanaysis.weekday_hour_orderCnt.forEach(
      element => {
        var t = element.Name.split("|")[1];
        if (time.indexOf(t) == -1) time.push(t);
      }
    );
    time = time.sort();
    this._traffic_3d.xAxis3D.data = time;
    this._traffic_3d.yAxis3D.data = weekday;
    //三维数组
    var data: any[] = [];
    this._timeanaysis.weekday_hour_orderCnt.forEach(element => {
      data.push([element.Name.split("|")[1], this.ConvertIntToWeekday(element.Name.split("|")[0]), element.Value]);
    });

    this._traffic_3d.series[0].data = data;
    this._traffic_3d.visualMap.max = 5000;
    this._traffic_3d.grid3D.boxWidth = 200;
    this._traffic_3d.grid3D.boxDepth = 80;
    this._traffic_3d.grid3D["height"] = 750;

  }


  ConvertIntToWeekday(weekday) {
    switch (weekday) {
      case "0": return "周日";
      case "1": return "周一";
      case "2": return "周二";
      case "3": return "周三";
      case "4": return "周四";
      case "5": return "周五";
      case "6": return "周六";
    }
  }

}

import { Component, OnInit } from '@angular/core';
import { CommonFunction } from 'src/app/Common/common';
import { I3DarStardard } from 'src/app/Common/chartOption';
import { ActivatedRoute } from '@angular/router';
import { NameValueSet, ITimeAnaysis } from '../Model';


@Component({
  selector: 'app-root',
  templateUrl: './TimeAnalysis.component.html',
})
export class TimeAnalysisComponent implements OnInit {
  constructor(private route: ActivatedRoute) { }
  _weekday_hour_orderCnt: NameValueSet[];
  _traffic_3d = CommonFunction.clone(I3DarStardard);
  _commonFunction = CommonFunction;
  _title = "";
  ngOnInit(): void {
    this.route.data
      .subscribe((xxx: { data: ITimeAnaysis }) => {

        switch (this.route.snapshot.routeConfig.path) {
          case "timeanalysis":
            this._title = "全体";
            this._weekday_hour_orderCnt = xxx.data.weekday_hour_orderCnt;
            break;
          case "timeanalysis_airport":
            this._title = "机场";
            this._weekday_hour_orderCnt = xxx.data.weekday_hour_orderCnt_airport;
            break;
          case "timeanalysis_longbus":
            this._title = "汽车站";
            this._weekday_hour_orderCnt = xxx.data.weekday_hour_orderCnt_longbus;
            break;
          case "timeanalysis_train":
            this._title = "火车站";
            this._weekday_hour_orderCnt = xxx.data.weekday_hour_orderCnt_train;
            break;
          case "timeanalysis_cbd":
            this._title = "商圈";
            this._weekday_hour_orderCnt = xxx.data.weekday_hour_orderCnt_cbd;
            break;
          case "timeanalysis_hospital":
            this._title = "医院";
            this._weekday_hour_orderCnt = xxx.data.weekday_hour_orderCnt_hospital;
            break;
          case "timeanalysis_school":
            this._title = "学校";
            this._weekday_hour_orderCnt = xxx.data.weekday_hour_orderCnt_school;
            break;
          case "timeanalysis_travel":
            this._title = "景点";
            this._weekday_hour_orderCnt = xxx.data.weekday_hour_orderCnt_travel;
            break;
          default:
            break;
        }

        //对3D时间进行填充  
        this.Fill3DTime();
      });
  }

  Fill3DTime() {
    //X:具体时间，Y：日期，Z：流量
    var weekday = ["周一", "周二", "周三", "周四", "周五", "周六", "周日"];
    var time = [];
    this._weekday_hour_orderCnt.forEach(
      element => {
        var t = element.Name.split("|")[1];
        if (time.indexOf(t) == -1) time.push(t);
      }
    );
    time = time.sort();
    this._traffic_3d.zAxis3D.name = "订单量";
    this._traffic_3d.xAxis3D.data = time;
    this._traffic_3d.yAxis3D.data = weekday;
    //三维数组
    var data: any[] = [];
    this._weekday_hour_orderCnt.forEach(element => {
      data.push([element.Name.split("|")[1], this.ConvertIntToWeekday(element.Name.split("|")[0]), element.Value]);
    });

    this._traffic_3d.series[0].data = data;
    this._traffic_3d.visualMap.max = 5000;
    let x = this._weekday_hour_orderCnt.map(x => x.Value);
    this._traffic_3d.visualMap.max = Math.max(...x);
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

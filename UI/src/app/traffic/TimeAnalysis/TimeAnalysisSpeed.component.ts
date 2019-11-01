import { Component, OnInit } from '@angular/core';
import { CommonFunction } from 'src/app/Common/common';
import { I3DarStardard } from 'src/app/Common/chartOption';
import { ActivatedRoute } from '@angular/router';
import { NameValueSet } from '../Model';
import { IDashBoard } from 'src/app/traffic/Model';


@Component({
  selector: 'app-root',
  templateUrl: './TimeAnalysisSpeed.component.html',
})
export class TimeAnalysisSPeedComponent implements OnInit {
  constructor(private route: ActivatedRoute) { }
  _weekday_hour_orderCnt: NameValueSet[];
  _traffic_3d = CommonFunction.clone(I3DarStardard);
  _commonFunction = CommonFunction;
  _title = "";
  ngOnInit(): void {
    this.route.data
      .subscribe((xxx: { data: IDashBoard }) => {
        switch (this.route.snapshot.routeConfig.path) {
          case "speedtimeanalysis":
            this._title = "全体";
            this._weekday_hour_orderCnt = xxx.data.weekday_hour_speed;
            break;
          case "speedtimeanalysis_airport":
            this._title = "机场";
            this._weekday_hour_orderCnt = xxx.data.weekday_hour_speed_airport;
            break;
          case "speedtimeanalysis_longbus":
            this._title = "汽车站";
            this._weekday_hour_orderCnt = xxx.data.weekday_hour_speed_longbus;
            break;
          case "speedtimeanalysis_train":
            this._title = "火车站";
            this._weekday_hour_orderCnt = xxx.data.weekday_hour_speed_train;
            break;
          case "speedtimeanalysis_cbd":
            this._title = "商圈";
            this._weekday_hour_orderCnt = xxx.data.weekday_hour_speed_cbd;
            break;
          case "speedtimeanalysis_hospital":
            this._title = "医院";
            this._weekday_hour_orderCnt = xxx.data.weekday_hour_speed_hospital;
            break;
          case "speedtimeanalysis_school":
            this._title = "学校";
            this._weekday_hour_orderCnt = xxx.data.weekday_hour_speed_school;
            break;
          case "speedtimeanalysis_travel":
            this._title = "景点";
            this._weekday_hour_orderCnt = xxx.data.weekday_hour_speed_travel;
            break;
          default:
            break;
        }
        //对3D时间进行填充  
        CommonFunction.Fill3DTime(this._weekday_hour_orderCnt, this._traffic_3d, "速度", 50);
      });
  }
}

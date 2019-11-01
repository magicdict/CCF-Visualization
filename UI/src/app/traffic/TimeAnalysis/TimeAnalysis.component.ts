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
        CommonFunction.Fill3DTime(this._weekday_hour_orderCnt, this._traffic_3d, "订单量");
      });
  }



}

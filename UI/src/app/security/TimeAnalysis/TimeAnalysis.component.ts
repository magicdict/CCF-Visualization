import { Component, OnInit } from '@angular/core';
import { CommonFunction } from 'src/app/Common/common';
import { IPolarStardard, PolarItem, I3DarStardard } from 'src/app/Common/chartOption';
import { ActivatedRoute } from '@angular/router';
import { ITimeAnaysis } from '../Model';


@Component({
  selector: 'app-root',
  templateUrl: './TimeAnalysis.component.html',
})
export class TimeAnalysisComponent implements OnInit {
  constructor(private route: ActivatedRoute) { }
  _timeanaysis: ITimeAnaysis;
  _protocols_rec_cnt = CommonFunction.clone(IPolarStardard);
  _protocols_traffic_cnt = CommonFunction.clone(IPolarStardard);
  _traffic_3d = CommonFunction.clone(I3DarStardard);
  _commonFunction = CommonFunction;
  ngOnInit(): void {
    this.route.data
      .subscribe((xxx: { data: ITimeAnaysis }) => {
        this._timeanaysis = xxx.data;
        this._protocols_rec_cnt.legend.data = [];
        this._protocols_rec_cnt.angleAxis.data = this._timeanaysis.hours_rec_cnt.map(x => x.Name);
        for (const key in this._timeanaysis.Protocols_Hours) {
          if (this._timeanaysis.Protocols_Hours.hasOwnProperty(key)) {
            this._protocols_rec_cnt.legend.data.push(key);
            const element = this._timeanaysis.Protocols_Hours[key];
            let protocols_connecttime = CommonFunction.clone(PolarItem);
            protocols_connecttime.name = key;
            protocols_connecttime.data = element.map(x => x.Value);
            this._protocols_rec_cnt.series.push(protocols_connecttime);
          }
        }

        this._protocols_traffic_cnt.legend.data = [];
        this._protocols_traffic_cnt.angleAxis.data = this._timeanaysis.hours_rec_cnt.map(x => x.Name);
        for (const key in this._timeanaysis.Protocols_Hours_Traffic) {
          if (this._timeanaysis.Protocols_Hours_Traffic.hasOwnProperty(key)) {
            this._protocols_traffic_cnt.legend.data.push(key);
            const element = this._timeanaysis.Protocols_Hours_Traffic[key];
            let protocols_connecttime = CommonFunction.clone(PolarItem);
            protocols_connecttime.name = key;
            protocols_connecttime.data = element.map(x => x.Value);
            this._protocols_traffic_cnt.series.push(protocols_connecttime);
          }
        }


        //对3D时间进行填充  
        this.Fill3DTime();

      });
  }

  Fill3DTime() {
    //X:具体时间，Y：日期，Z：流量

    var date = [];
    var time = [];
    this._timeanaysis.traffic_hours_everyday.forEach(
      element => {
        var d = element.Name.split("|")[0];
        var t = element.Name.split("|")[1];
        if (date.indexOf(d) == -1) date.push(d);
        if (time.indexOf(t) == -1) time.push(t);
      }
    );
    date = date.sort();
    time = time.sort();
    this._traffic_3d.xAxis3D.data = time;
    this._traffic_3d.yAxis3D.data = date;
    //三维数组
    var data: any[] = [];
    this._timeanaysis.traffic_hours_everyday.forEach(element => {
      data.push([element.Name.split("|")[1], element.Name.split("|")[0], element.Value]);
    });

    this._traffic_3d.series[0].data = data;
    let x = this._timeanaysis.traffic_hours_everyday.map(x=>x.Value);
    this._traffic_3d.visualMap.max = Math.max(...x);
  }

}

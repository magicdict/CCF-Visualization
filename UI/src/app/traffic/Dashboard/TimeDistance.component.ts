import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IDashBoard } from '../Model';
import { ILineStardard, LineItem, IPieStardard } from 'src/app/Common/chartOption';
import { CommonFunction } from 'src/app/Common/common';
 

@Component({
  templateUrl: './TimeDistance.component.html',
})
export class TimeDistanceComponent implements OnInit {
 
  constructor(private route: ActivatedRoute) { }
  _dashboard: IDashBoard;
  _commonFunction = CommonFunction;

  _weeklyDistance = CommonFunction.clone(ILineStardard); 
  _weeklySpeed = CommonFunction.clone(ILineStardard); 
  _Speed = CommonFunction.clone(ILineStardard); 
  _NormalTime = CommonFunction.clone(IPieStardard); 
  _Distance = CommonFunction.clone(IPieStardard); 
  _WaitTime = CommonFunction.clone(IPieStardard);
  _WaitTimeWeekly = CommonFunction.clone(ILineStardard);
  _Dsitence_KM  = CommonFunction.clone(ILineStardard); 


  ngOnInit(): void {
    this.route.data
      .subscribe((xxx: { data: IDashBoard }) => {
        this._dashboard = xxx.data;

        //等候时间
        this._WaitTime.legend.data = this._dashboard.WaitTime.map(x => x.Name);
        this._WaitTime.series[0].data = this._dashboard.WaitTime.map(x => { return { name: x.Name, value: x.Value } });

        this._WaitTimeWeekly.title.text = "";
        this._WaitTimeWeekly.xAxis.data = this._dashboard.weeklyinfos.map(x => x.Name);
        this._WaitTimeWeekly.legend.data = this._dashboard.WaitTime.map(x => x.Name);

        let WaitTimeWeekly_1 = CommonFunction.clone(LineItem);
        WaitTimeWeekly_1.type = "bar";
        WaitTimeWeekly_1["stack"] = "WaitTimeWeekly";
        WaitTimeWeekly_1.name = this._WaitTimeWeekly.legend.data[0];
        WaitTimeWeekly_1.data = this._dashboard.weeklyinfos.map(x => CommonFunction.roundvalue((x.Value.waittime_1) * 100 /
          (x.Value.waittime_1 + x.Value.waittime_2 + x.Value.waittime_3 + x.Value.waittime_4)));
        this._WaitTimeWeekly.series.push(WaitTimeWeekly_1);

        let WaitTimeWeekly_2 = CommonFunction.clone(LineItem);
        WaitTimeWeekly_2.type = "bar";
        WaitTimeWeekly_2["stack"] = "WaitTimeWeekly";
        WaitTimeWeekly_2.name = this._WaitTimeWeekly.legend.data[1];
        WaitTimeWeekly_2.data = this._dashboard.weeklyinfos.map(x => CommonFunction.roundvalue((x.Value.waittime_2) * 100 /
          (x.Value.waittime_1 + x.Value.waittime_2 + x.Value.waittime_3 + x.Value.waittime_4)));
        this._WaitTimeWeekly.series.push(WaitTimeWeekly_2);

        let WaitTimeWeekly_3 = CommonFunction.clone(LineItem);
        WaitTimeWeekly_3.type = "bar";
        WaitTimeWeekly_3["stack"] = "WaitTimeWeekly";
        WaitTimeWeekly_3.name = this._WaitTimeWeekly.legend.data[2];
        WaitTimeWeekly_3.data = this._dashboard.weeklyinfos.map(x => CommonFunction.roundvalue((x.Value.waittime_3) * 100 /
          (x.Value.waittime_1 + x.Value.waittime_2 + x.Value.waittime_3 + x.Value.waittime_4)));
        this._WaitTimeWeekly.series.push(WaitTimeWeekly_3);

        let WaitTimeWeekly_4 = CommonFunction.clone(LineItem);
        WaitTimeWeekly_4.type = "bar";
        WaitTimeWeekly_4["stack"] = "WaitTimeWeekly";
        WaitTimeWeekly_4.name = this._WaitTimeWeekly.legend.data[3];
        WaitTimeWeekly_4.data = this._dashboard.weeklyinfos.map(x => CommonFunction.roundvalue((x.Value.waittime_4) * 100 /
          (x.Value.waittime_1 + x.Value.waittime_2 + x.Value.waittime_3 + x.Value.waittime_4)));
        this._WaitTimeWeekly.series.push(WaitTimeWeekly_4);

        this._NormalTime.legend.data = this._dashboard.NormalTime.map(x => x.Name);
        this._NormalTime.series[0].data = this._dashboard.NormalTime.map(x => { return { name: x.Name, value: x.Value } });


        this._weeklyDistance.title.text = "";
        this._weeklyDistance.xAxis.data = this._dashboard.weeklyinfos.map(x => x.Name);
        let weeklydistance = CommonFunction.clone(LineItem);
        weeklydistance.name = "公里数";
        weeklydistance.data = this._dashboard.weeklyinfos.map(x => CommonFunction.roundvalue(x.Value.distance / x.Value.ordercnt));
        this._weeklyDistance.series.push(weeklydistance);
        this._Distance.legend.data = this._dashboard.Distance.map(x => x.Name);
        this._Distance.series[0].data = this._dashboard.Distance.map(x => { return { name: x.Name, value: x.Value } });


        this._Dsitence_KM.title.text = "";
        this._Dsitence_KM.xAxis.data = this._dashboard.distance_km.map(x => x.Name);
        let Dsitence_KM = CommonFunction.clone(LineItem);
        Dsitence_KM.name = "公里";
        Dsitence_KM.data = this._dashboard.distance_km.map(x=>x.Value);
        this._Dsitence_KM.series.push(Dsitence_KM);

        //订单时速

        this._weeklySpeed.title.text = "";
        this._weeklySpeed.xAxis.data = this._dashboard.weeklyinfos.map(x => x.Name);
        let weeklySpeed = CommonFunction.clone(LineItem);
        weeklySpeed.name = "速度";
        weeklySpeed.data = this._dashboard.weeklyinfos.map(x => CommonFunction.roundvalue(x.Value.distance * 60 / x.Value.normaltime));
        this._weeklySpeed.series.push(weeklySpeed);

        this._Speed.title.text = "";
        this._Speed.xAxis.data = this._dashboard.speed.map(x => x.Name);
        let Speed = CommonFunction.clone(LineItem);
        Speed.name = "速度";
        Speed.data = this._dashboard.speed.map(x=>x.Value);
        this._Speed.series.push(Speed);


    
      });
  }
}

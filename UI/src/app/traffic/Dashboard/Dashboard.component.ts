import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IDashBoard } from '../Model';
import { ILineStardard, LineItem, IPieStardard } from 'src/app/Common/chartOption';
import { CommonFunction } from 'src/app/Common/common';


@Component({
  templateUrl: './Dashboard.component.html',
})
export class DashboardComponent implements OnInit {

  constructor(private route: ActivatedRoute) { }
  _dashboard: IDashBoard;
  _commonFunction = CommonFunction;

  _weeklyOrdercnt = CommonFunction.clone(ILineStardard);
  _travellerCnt = CommonFunction.clone(ILineStardard);
  _weeklyDistance = CommonFunction.clone(ILineStardard);
  _weeklySpeed = CommonFunction.clone(ILineStardard);
  _weeklyTypes = CommonFunction.clone(ILineStardard);
  _weeklyTransport = CommonFunction.clone(ILineStardard);

  //饼图
  _NormalTime = CommonFunction.clone(IPieStardard);
  _WaitTime = CommonFunction.clone(IPieStardard);
  _Distance = CommonFunction.clone(IPieStardard);
  _product_ids = CommonFunction.clone(IPieStardard);
  _countys = CommonFunction.clone(IPieStardard);
  _order_type = CommonFunction.clone(IPieStardard);
  _traffic_types = CommonFunction.clone(IPieStardard);
  _product_1levels = CommonFunction.clone(IPieStardard);
  _starting_pois = CommonFunction.clone(IPieStardard);
  _dest_pois = CommonFunction.clone(IPieStardard);

  //堆叠图
  _WaitTimeWeekly = CommonFunction.clone(ILineStardard);
  _DistanceWeekly = CommonFunction.clone(ILineStardard);

  ngOnInit(): void {
    this.route.data
      .subscribe((xxx: { data: IDashBoard }) => {
        this._dashboard = xxx.data;
        this._weeklyOrdercnt.title.text = "";
        this._weeklyOrdercnt.xAxis.data = this._dashboard.weeklyinfos.map(x => x.Name);
        let weeklyOrdercnt = CommonFunction.clone(LineItem);
        weeklyOrdercnt.name = "周订单数";
        weeklyOrdercnt.data = this._dashboard.weeklyinfos.map(x => x.Value.ordercnt);
        this._weeklyOrdercnt.series.push(weeklyOrdercnt);

        this._travellerCnt.title.text = "";
        this._travellerCnt.xAxis.data = this._dashboard.TravellerCnt.map(x => x.Name);
        let travellerCnt = CommonFunction.clone(LineItem);
        travellerCnt.name = "境内外过夜旅客数";
        travellerCnt.data = this._dashboard.TravellerCnt.map(x => x.Value);
        this._travellerCnt.series.push(travellerCnt);

        this._weeklySpeed.title.text = "";
        this._weeklySpeed.xAxis.data = this._dashboard.weeklyinfos.map(x => x.Name);
        let weeklySpeed = CommonFunction.clone(LineItem);
        weeklySpeed.name = "速度";
        weeklySpeed.data = this._dashboard.weeklyinfos.map(x => CommonFunction.roundvalue(x.Value.distance * 60 / x.Value.normaltime));
        this._weeklySpeed.series.push(weeklySpeed);

        this._weeklyDistance.title.text = "";
        this._weeklyDistance.xAxis.data = this._dashboard.weeklyinfos.map(x => x.Name);
        let weeklydistance = CommonFunction.clone(LineItem);
        weeklydistance.name = "公里数";
        weeklydistance.data = this._dashboard.weeklyinfos.map(x => CommonFunction.roundvalue(x.Value.distance / x.Value.ordercnt));
        this._weeklyDistance.series.push(weeklydistance);        


        this._weeklyTypes.title.text = "";
        this._weeklyTypes.xAxis.data = this._dashboard.weeklyinfos.map(x => x.Name);
        let weeklyPremier = CommonFunction.clone(LineItem);
        weeklyPremier.name = "专车数";
        weeklyPremier.data = this._dashboard.weeklyinfos.map(x => CommonFunction.roundvalue(x.Value.premier * 100 / x.Value.ordercnt));
        this._weeklyTypes.series.push(weeklyPremier);

        let weeklyReserve = CommonFunction.clone(LineItem);
        weeklyReserve.name = "预约数";
        weeklyReserve.data = this._dashboard.weeklyinfos.map(x => CommonFunction.roundvalue(x.Value.reserve * 100 / x.Value.ordercnt));
        this._weeklyTypes.series.push(weeklyReserve);

        let weeklyPickup = CommonFunction.clone(LineItem);
        weeklyPickup.name = "接机送机数";
        weeklyPickup.data = this._dashboard.weeklyinfos.map(x => CommonFunction.roundvalue(x.Value.pickup * 100 / x.Value.ordercnt));
        this._weeklyTypes.series.push(weeklyPickup);
        this._weeklyTypes.legend.data = ["专车数", "预约数", "接机送机数"]

        this._weeklyTransport.title.text = "";
        this._weeklyTransport.xAxis.data = this._dashboard.weeklyinfos.map(x => x.Name);
        
        let weeklyAirport = CommonFunction.clone(LineItem);
        weeklyAirport.name = "机场";
        weeklyAirport.data = this._dashboard.weeklyinfos.map(x => CommonFunction.roundvalue((x.Value.airport) * 100 / x.Value.ordercnt));
        this._weeklyTransport.series.push(weeklyAirport);
        
        let weeklyTrain = CommonFunction.clone(LineItem);
        weeklyTrain.name = "火车站";
        weeklyTrain.data = this._dashboard.weeklyinfos.map(x => CommonFunction.roundvalue((x.Value.train) * 100 / x.Value.ordercnt));
        this._weeklyTransport.series.push(weeklyTrain);

        let weeklyLongbus = CommonFunction.clone(LineItem);
        weeklyLongbus.name = "汽车站";
        weeklyLongbus.data = this._dashboard.weeklyinfos.map(x => CommonFunction.roundvalue((x.Value.longbus) * 100 / x.Value.ordercnt));
        this._weeklyTransport.series.push(weeklyLongbus);

        let weeklyschool = CommonFunction.clone(LineItem);
        weeklyschool.name = "学校";
        weeklyschool.data = this._dashboard.weeklyinfos.map(x => CommonFunction.roundvalue((x.Value.school) * 100 / x.Value.ordercnt));
        this._weeklyTransport.series.push(weeklyschool);

        let weeklyhospital = CommonFunction.clone(LineItem);
        weeklyhospital.name = "医院";
        weeklyhospital.data = this._dashboard.weeklyinfos.map(x => CommonFunction.roundvalue((x.Value.hospital) * 100 / x.Value.ordercnt));
        this._weeklyTransport.series.push(weeklyhospital);
        
        this._weeklyTransport.legend.data = ["机场", "火车站", "汽车站","学校","医院"]

        this._NormalTime.legend.data = this._dashboard.NormalTime.map(x => x.Name);
        this._NormalTime.series[0].data = this._dashboard.NormalTime.map(x => { return { name: x.Name, value: x.Value } });

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


        this._Distance.legend.data = this._dashboard.Distance.map(x => x.Name);
        this._Distance.series[0].data = this._dashboard.Distance.map(x => { return { name: x.Name, value: x.Value } });

        this._product_ids.legend.data = this._dashboard.product_ids.map(x => x.Name);
        this._product_ids.series[0].data = this._dashboard.product_ids.map(x => { return { name: x.Name, value: x.Value } });

        this._order_type.legend.data = this._dashboard.order_type.map(x => x.Name);
        this._order_type.series[0].data = this._dashboard.order_type.map(x => { return { name: x.Name, value: x.Value } });

        this._traffic_types.legend.data = this._dashboard.traffic_types.map(x => x.Name);
        this._traffic_types.series[0].data = this._dashboard.traffic_types.map(x => { return { name: x.Name, value: x.Value } });

        this._product_1levels.legend.data = this._dashboard.product_1levels.map(x => x.Name);
        this._product_1levels.series[0].data = this._dashboard.product_1levels.map(x => { return { name: x.Name, value: x.Value } });

        this._countys.legend.data = this._dashboard.countys.map(x => x.Name);
        this._countys.series[0].data = this._dashboard.countys.map(x => { return { name: x.Name, value: x.Value } });

        this._starting_pois.legend.data = this._dashboard.starting_pois.map(x => x.Name);
        this._starting_pois.series[0].data = this._dashboard.starting_pois.map(x => { return { name: x.Name, value: x.Value } });

        this._dest_pois.legend.data = this._dashboard.dest_pois.map(x => x.Name);
        this._dest_pois.series[0].data = this._dashboard.dest_pois.map(x => { return { name: x.Name, value: x.Value } });

      });
  }
}

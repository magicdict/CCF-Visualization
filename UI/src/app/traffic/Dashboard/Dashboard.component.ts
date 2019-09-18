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
  _weeklyDistance = CommonFunction.clone(ILineStardard);
  _travellerCnt = CommonFunction.clone(ILineStardard);
  //饼图
  _Time = CommonFunction.clone(IPieStardard);
  _Distance = CommonFunction.clone(IPieStardard);
  _product_ids = CommonFunction.clone(IPieStardard);
  _countys = CommonFunction.clone(IPieStardard);
  _order_type = CommonFunction.clone(IPieStardard);
  _traffic_types = CommonFunction.clone(IPieStardard);
  _product_1levels = CommonFunction.clone(IPieStardard);
  _starting_pois = CommonFunction.clone(IPieStardard);
  _dest_pois = CommonFunction.clone(IPieStardard);

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

        this._weeklyDistance.title.text = "";
        this._weeklyDistance.xAxis.data = this._dashboard.weeklyinfos.map(x => x.Name);
        let weeklydistance = CommonFunction.clone(LineItem);
        weeklydistance.name = "周公里数";
        weeklydistance.data = this._dashboard.weeklyinfos.map(x => CommonFunction.roundvalue(x.Value.distance / x.Value.ordercnt));
        this._weeklyDistance.series.push(weeklydistance);

        this._Time.legend.data = this._dashboard.Time.map(x => x.Name);
        this._Time.series[0].data = this._dashboard.Time.map(x => { return { name: x.Name, value: x.Value } });
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

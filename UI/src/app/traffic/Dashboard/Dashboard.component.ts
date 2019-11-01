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
  _weeklyTypes = CommonFunction.clone(ILineStardard);
  _weeklyTransport = CommonFunction.clone(ILineStardard);
  _weeklyPOI = CommonFunction.clone(ILineStardard);

  //饼图
  _product_ids = CommonFunction.clone(IPieStardard);
  _countys = CommonFunction.clone(IPieStardard);
  _order_type = CommonFunction.clone(IPieStardard);
  _traffic_types = CommonFunction.clone(IPieStardard);
  _product_1levels = CommonFunction.clone(IPieStardard);
  _starting_pois = CommonFunction.clone(IPieStardard);
  _dest_pois = CommonFunction.clone(IPieStardard);
  _starting_transport = CommonFunction.clone(IPieStardard);
  _dest_transport = CommonFunction.clone(IPieStardard);


  ngOnInit(): void {
    this.route.data
      .subscribe((xxx: { data: IDashBoard }) => {
        this._dashboard = xxx.data;
        this._weeklyOrdercnt.title.text = "";
        this._weeklyOrdercnt.xAxis.data = this._dashboard.weeklyinfos.map(x => x.Name);
        let weeklyOrdercnt = CommonFunction.clone(LineItem);
        weeklyOrdercnt.name = "周订单数";
        weeklyOrdercnt.data = this._dashboard.weeklyinfos.map(x => x.Value.ordercnt);
        
        weeklyOrdercnt['markPoint'] = {
          data: [
            { type: 'max', name: '最大值' },
            { type: 'min', name: '最小值' }
          ]
        },
        weeklyOrdercnt['markLine'] = {
          data: [
            { type: 'average', name: '平均值' }
          ]
        },

        this._weeklyOrdercnt.series.push(weeklyOrdercnt);

        this._travellerCnt.title.text = "";
        this._travellerCnt.xAxis.data = this._dashboard.TravellerCnt.map(x => x.Name);
        let travellerCnt = CommonFunction.clone(LineItem);
        travellerCnt.type = "bar";
        travellerCnt.name = "境内外过夜旅客数";
        travellerCnt.data = this._dashboard.TravellerCnt.map(x => x.Value);
        this._travellerCnt.series.push(travellerCnt);



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

        this._weeklyTransport.legend.data = ["机场", "火车站", "汽车站"]

        this._weeklyPOI.title.text = "";
        this._weeklyPOI.xAxis.data = this._dashboard.weeklyinfos.map(x => x.Name);

        let weeklyschool = CommonFunction.clone(LineItem);
        weeklyschool.name = "学校";
        weeklyschool.data = this._dashboard.weeklyinfos.map(x => CommonFunction.roundvalue((x.Value.school) * 100 / x.Value.ordercnt));
        this._weeklyPOI.series.push(weeklyschool);

        let weeklyhospital = CommonFunction.clone(LineItem);
        weeklyhospital.name = "医院";
        weeklyhospital.data = this._dashboard.weeklyinfos.map(x => CommonFunction.roundvalue((x.Value.hospital) * 100 / x.Value.ordercnt));
        this._weeklyPOI.series.push(weeklyhospital);

        let weeklytravel = CommonFunction.clone(LineItem);
        weeklytravel.name = "景点";
        weeklytravel.data = this._dashboard.weeklyinfos.map(x => CommonFunction.roundvalue((x.Value.travel) * 100 / x.Value.ordercnt));
        this._weeklyPOI.series.push(weeklytravel);
        this._weeklyPOI.legend.data = ["学校", "医院", "景点"];





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

        this._starting_pois.legend.data = this._weeklyPOI.legend.data;
        this._starting_pois.series[0].data = this._dashboard.starting_pois
          .filter(x => this._weeklyPOI.legend.data.indexOf(x.Name) != -1).map(x => { return { name: x.Name, value: x.Value } });

        this._dest_pois.legend.data = this._weeklyPOI.legend.data;
        this._dest_pois.series[0].data = this._dashboard.dest_pois
          .filter(x => this._weeklyPOI.legend.data.indexOf(x.Name) != -1).map(x => { return { name: x.Name, value: x.Value } });

        this._starting_transport.legend.data = this._weeklyTransport.legend.data;
        this._starting_transport.series[0].data = this._dashboard.starting_pois
          .filter(x => this._weeklyTransport.legend.data.indexOf(x.Name) != -1).map(x => { return { name: x.Name, value: x.Value } });

        this._dest_transport.legend.data = this._weeklyTransport.legend.data;
        this._dest_transport.series[0].data = this._dashboard.dest_pois
          .filter(x => this._weeklyTransport.legend.data.indexOf(x.Name) != -1).map(x => { return { name: x.Name, value: x.Value } });
      });
  }
}

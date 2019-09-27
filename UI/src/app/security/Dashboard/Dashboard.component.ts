import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IDashBoard } from '../Model';
import { IPieStardard, ILineStardard, LineItem } from 'src/app/Common/chartOption';
import { CommonFunction } from 'src/app/Common/common';


@Component({
  selector: 'app-root',
  templateUrl: './Dashboard.component.html',
})
export class DashboardComponent implements OnInit {

  constructor(private route: ActivatedRoute) { }
  _dashboard: IDashBoard;
  _protocols = CommonFunction.clone(IPieStardard);
  _ports = CommonFunction.clone(IPieStardard);

  _traffic_hours = CommonFunction.clone(ILineStardard);
  _access_hours = CommonFunction.clone(ILineStardard);
  _commonFunction = CommonFunction;

  ngOnInit(): void {
    this.route.data
      .subscribe((xxx: { data: IDashBoard }) => {
        this._dashboard = xxx.data;
        this._protocols.legend.data = this._dashboard.protocols.map(x => x.Name);
        this._protocols.series[0].data = this._dashboard.protocols.map(x => { return { name: x.Name, value: x.Value } });

        this._ports.legend.data = this._dashboard.ports.map(x => x.Name);
        this._ports.series[0].data = this._dashboard.ports.map(x => { return { name: x.Name, value: x.Value } });
 
        this._traffic_hours.title.text = "";
        this._traffic_hours.legend.data = ["最近3日", "昨天", "今天"];
        this._traffic_hours.xAxis.data = this._dashboard.traffic_hours_last3days.map(x => x.Name);
        let last3days = CommonFunction.clone(LineItem);
        last3days.name = "最近3日";
        last3days.data = this._dashboard.traffic_hours_last3days.map(x => x.Value);
        this._traffic_hours.series.push(last3days);

        let last1days = CommonFunction.clone(LineItem);
        last1days.name = "昨天";
        last1days.data = this._dashboard.traffic_hours_last1days.map(x => x.Value);
        this._traffic_hours.series.push(last1days);

        let today = CommonFunction.clone(LineItem);
        today.name = "今天";
        today.data = this._dashboard.traffic_hours_today.map(x => x.Value);
        this._traffic_hours.series.push(today);



        this._access_hours.title.text = "";
        this._access_hours.legend.data = ["最近3日", "昨天", "今天"];
        this._access_hours.xAxis.data = this._dashboard.access_hours_last3days.map(x => x.Name);

        let access_last3days = CommonFunction.clone(LineItem);
        access_last3days.name = "最近3日";
        access_last3days.data = this._dashboard.access_hours_last3days.map(x => x.Value);
        this._access_hours.series.push(access_last3days);

        let access_last1days = CommonFunction.clone(LineItem);
        access_last1days.name = "昨天";
        access_last1days.data = this._dashboard.access_hours_last1days.map(x => x.Value);
        this._access_hours.series.push(access_last1days);

        let access_today = CommonFunction.clone(LineItem);
        access_today.name = "今天";
        access_today.data = this._dashboard.access_hours_today.map(x => x.Value);
        this._access_hours.series.push(access_today);

      });
  }
}

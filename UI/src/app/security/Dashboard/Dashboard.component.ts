import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IDashBoard } from '../Model';
import { IPieStardard, IPolarStardard, PolarItem, ILineStardard, LineItem } from 'src/app/Common/chartOption';
import { CommonFunction } from 'src/app/Common/common';


@Component({
  selector: 'app-root',
  templateUrl: './Dashboard.component.html',
})
export class DashboardComponent implements OnInit {

  constructor(private route: ActivatedRoute) { }
  _dashboard: IDashBoard;
  _protocols = CommonFunction.clone(IPieStardard);
  _traffic_hours = CommonFunction.clone(ILineStardard)
  _commonFunction = CommonFunction;
  //TOP10

 

  ngOnInit(): void {
    this.route.data
      .subscribe((xxx: { data: IDashBoard }) => {
        this._dashboard = xxx.data;
        this._protocols.legend.data = this._dashboard.Protocols.map(x => x.Name);
        this._protocols.series[0].data = this._dashboard.Protocols.map(x => { return { name: x.Name, value: x.Value } });
        this._traffic_hours.title.text = "";
        this._traffic_hours.legend.data = ["最近3日","昨天","今天"];
        this._traffic_hours.xAxis.data = this._dashboard.traffic_hours_last3days.map(x=>x.Name);
        let last3days = CommonFunction.clone(LineItem);
        last3days.name = "最近3日";
        last3days.data = this._dashboard.traffic_hours_last3days.map(x=>x.Value);  
        this._traffic_hours.series.push(last3days);

        let last1days = CommonFunction.clone(LineItem);
        last1days.name = "昨天";
        last1days.data = this._dashboard.traffic_hours_last1days.map(x=>x.Value);  
        this._traffic_hours.series.push(last1days);

        let today = CommonFunction.clone(LineItem);
        today.name = "今天";
        today.data = this._dashboard.traffic_hours_today.map(x=>x.Value);  
        this._traffic_hours.series.push(today);
      });
    }
}

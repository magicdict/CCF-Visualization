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
  _traffic_hours = CommonFunction.clone(ILineStardard);
  _source_Top10 = CommonFunction.clone(ILineStardard);
  _dist_Top10 = CommonFunction.clone(ILineStardard);
  _source_dist_Top10 = CommonFunction.clone(ILineStardard);
  _commonFunction = CommonFunction;

  ngOnInit(): void {
    this.route.data
      .subscribe((xxx: { data: IDashBoard }) => {
        this._dashboard = xxx.data;
        this._protocols.legend.data = this._dashboard.Protocols.map(x => x.Name);
        this._protocols.series[0].data = this._dashboard.Protocols.map(x => { return { name: x.Name, value: x.Value } });

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

        this._source_Top10.xAxis.data = this._dashboard.source.slice(0,10).map(x => x.Name);
        let sourceitem = CommonFunction.clone(LineItem);
        sourceitem.name = "源头TOP10";
        sourceitem.data = this._dashboard.source.slice(0,10).map(x => x.Value);
        sourceitem.type = "bar";
        this._source_Top10.series.push(sourceitem);

        this._dist_Top10.xAxis.data = this._dashboard.dist.slice(0,10).map(x => x.Name);
        let distitem = CommonFunction.clone(LineItem);
        distitem.name = "目标TOP10";
        distitem.data = this._dashboard.dist.slice(0,10).map(x => x.Value);
        distitem.type = "bar";
        this._dist_Top10.series.push(distitem);

        this._source_dist_Top10.xAxis.data = this._dashboard.source_dist.slice(0,10).map(x => x.Name);
        let source_distitem = CommonFunction.clone(LineItem);
        source_distitem.name = "源头目标TOP10";
        source_distitem.data = this._dashboard.source_dist.slice(0,10).map(x => x.Value);
        source_distitem.type = "bar";
        this._source_dist_Top10.series.push(source_distitem);

      });
  }
}

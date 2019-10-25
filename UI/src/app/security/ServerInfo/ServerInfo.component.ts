import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IServerInfo, IDashBoard, NameValueSet } from '../Model';
import { CommonFunction } from 'src/app/Common/common';
import { ILineStardard, LineItem } from 'src/app/Common/chartOption';


@Component({
  templateUrl: './ServerInfo.component.html',
})
export class ServerInfoComponent implements OnInit {
  constructor(private route: ActivatedRoute) { }
  _dashboard: IDashBoard;
  _source_Top5 = CommonFunction.clone(ILineStardard);
  _dist_Top5 = CommonFunction.clone(ILineStardard);
  _source_dist_Top5 = CommonFunction.clone(ILineStardard);
  _commonFunction = CommonFunction;

  ngOnInit(): void {
    this.route.data
      .subscribe((xxx: { data: IDashBoard }) => {
        this._dashboard = xxx.data;
        this._source_Top5.xAxis.data = this._dashboard.source.slice(0, 20).map(x => x.Name);
        let sourceitem = CommonFunction.clone(LineItem);
        sourceitem.name = "源头TOP20";
        sourceitem.data = this._dashboard.source.slice(0, 20).map(x => x.Value);
        sourceitem.type = "bar";
        this._source_Top5.series.push(sourceitem);

        this._dist_Top5.xAxis.data = this._dashboard.dist.slice(0, 20).map(x => x.Name);
        let distitem = CommonFunction.clone(LineItem);
        distitem.name = "目标TOP20";
        distitem.data = this._dashboard.dist.slice(0, 20).map(x => x.Value);
        distitem.type = "bar";
        this._dist_Top5.series.push(distitem);

        this._source_dist_Top5.xAxis.data = this._dashboard.source_dist.slice(0, 20).map(this.source_dist_xAxis);
        //this._source_dist_Top5.xAxis["axisLabel"] = { interval: 0 };
        let source_distitem = CommonFunction.clone(LineItem);
        source_distitem.name = "源头目标TOP20";
        source_distitem.data = this._dashboard.source_dist.slice(0, 20).map(x => x.Value);
        source_distitem.type = "bar";
        this._source_dist_Top5.series.push(source_distitem);
      });
  }

  source_dist_xAxis(value: NameValueSet): string {
    var name = value.Name;
    var source = name.split("->")[0];
    var dist = name.split("->")[1];
    return source + "\n" + dist;
  }
}

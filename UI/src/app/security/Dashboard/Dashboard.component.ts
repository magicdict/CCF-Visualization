import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IDashBoard } from '../Model';
import { IStardardPie, IStardardPolar, PolarItem } from 'src/app/Common/chartOption';
import { CommonFunction } from 'src/app/Common/common';


@Component({
  selector: 'app-root',
  templateUrl: './Dashboard.component.html',
})
export class DashboardComponent implements OnInit {

  boxstyle_Col4 = { 'width': '400px', 'height': '400px' };
  chartstyle_Col4 = { 'width': '350px', 'height': '350px' };

  boxstyle_Col8 = { 'width': '800px', 'height': '400px' };
  chartstyle_Col8 = { 'width': '750px', 'height': '350px' };

  constructor(private route: ActivatedRoute) { }
  _dashboard: IDashBoard;
  _protocols = CommonFunction.clone(IStardardPie);
  _connectTime = CommonFunction.clone(IStardardPolar);
  _protocols_connectTime = CommonFunction.clone(IStardardPolar);

  ngOnInit(): void {
    this.route.data
      .subscribe((xxx: { data: IDashBoard }) => {
        this._dashboard = xxx.data;
        this._protocols.legend.data = this._dashboard.Protocols.map(x => x.Name);
        this._protocols.series[0].data = this._dashboard.Protocols.map(x => { return { name: x.Name, value: x.Value } });
        

        this._connectTime.legend.data = ["通信次数"];
        this._connectTime.angleAxis.data = this._dashboard.Hours.map(x => x.Name);
        let connecttime = CommonFunction.clone(PolarItem);
        connecttime.data = this._dashboard.Hours.map(x => x.Value);
        this._connectTime.series.push(connecttime);

        this._protocols_connectTime.legend.data = [];
        this._protocols_connectTime.angleAxis.data = this._dashboard.Hours.map(x => x.Name);
        for (const key in this._dashboard.Protocols_Hours) {
          if (this._dashboard.Protocols_Hours.hasOwnProperty(key)) {
            this._protocols_connectTime.legend.data.push(key);  
            const element = this._dashboard.Protocols_Hours[key];
            let protocols_connecttime = CommonFunction.clone(PolarItem);
            protocols_connecttime.name = key;
            protocols_connecttime.data = element.map(x => x.Value);
            this._protocols_connectTime.series.push(protocols_connecttime);
          }
        }

        console.log(this._protocols_connectTime);

      });
  }
}

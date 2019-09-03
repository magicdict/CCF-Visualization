import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IDashBoard } from '../Model';
import { IStardardPie } from 'src/app/Common/chartOption';
import { CommonFunction } from 'src/app/Common/common';


@Component({
  selector: 'app-root',
  templateUrl: './Dashboard.component.html',
})
export class DashboardComponent implements OnInit {

  boxstyle_Col = { 'width' : '400px' , 'height' : '400px' };
  boxstyle_Col2 = { 'width' : '800px' , 'height' : '400px' };
  chartstyle = { 'width' : '350px' , 'height' : '350px' };

  constructor(private route: ActivatedRoute) { }
  _dashboard: IDashBoard;
  _protocols = CommonFunction.clone(IStardardPie);
  ngOnInit(): void {
    this.route.data
      .subscribe((xxx: { data: IDashBoard }) => {
        this._dashboard = xxx.data;
        this._protocols.legend.data = this._dashboard.Protocols.map(x => x.Name);
        this._protocols.series[0].data = this._dashboard.Protocols.map(x => { return { name: x.Name, value: x.Value } });
        console.log(this._protocols);
      });
  }
}

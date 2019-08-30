import { Component, OnInit } from '@angular/core';
import { CommonFunction } from './common';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  constructor(private cf: CommonFunction) {

  }

  option = {
    xAxis: {
      type: 'category',
      data: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun']
    },
    yAxis: {
      type: 'value'
    },
    series: [{
      data: [820, 932, 901, 934, 1290, 1330, 1320],
      type: 'bar'
    }]
  };

  ngOnInit(): void {
    //HTTP Test
    this.cf.httpRequestGet<{ name: string, count: number }[]>("GetProtocols").then(
      r => {
        this.option.xAxis.data = r.map(x => x.name).splice(0, 10);
        this.option.series[0].data = r.map(x => x.count / 10000).splice(0, 10);
      }
    );
  }
  title = 'CCF大数据与计算智能大赛';
}

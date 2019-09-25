import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IDashBoard } from '../Model';
import { ISankeyStardard } from 'src/app/Common/chartOption';
import { CommonFunction } from 'src/app/Common/common';


@Component({
  templateUrl: './TrafficSankey.component.html',
})
export class TrafficSankeyComponent implements OnInit {

  constructor(private route: ActivatedRoute) { }
  _dashboard: IDashBoard;
  _sankey = CommonFunction.clone(ISankeyStardard);
  _commonFunction = CommonFunction;

  ngOnInit(): void {
    this.route.data
      .subscribe((xxx: { data: IDashBoard }) => {
        this._dashboard = xxx.data;

        this._sankey.title.text = "";
        let sourcelist = xxx.data.source_dist.map(item => item.Name.split("->")[0]).filter((value, index, self) => self.indexOf(value) === index);
        let distlist = xxx.data.source_dist.map(item => item.Name.split("->")[1]).filter((value, index, self) => self.indexOf(value) === index);

        let total = CommonFunction.clone(sourcelist);
        total.push(...distlist);
        total = total.filter((value, index, self) => self.indexOf(value) === index);
        //去掉同时为源头和目标的地址，防止循环
        total.forEach(
          e => {
            if (!(sourcelist.indexOf(e) != -1 && distlist.indexOf(e) != -1)) {
              this._sankey.series.data.push({ 'name': e });
            } else {
              console.log("同时为源头和目标的IP地址：" + e);
            }
          }
        )
        xxx.data.source_dist.forEach(
          e => {
            this._sankey.series.links.push({ 'source': e.Name.split("->")[0], 'target': e.Name.split("->")[1], 'value': e.Value });
          }
        );
      });
  }
}

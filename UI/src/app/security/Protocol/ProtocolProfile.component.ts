import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IServerInfo, IDashBoard, NameValueSet, IProfile } from '../Model';
import { CommonFunction } from 'src/app/Common/common';
import { ILineStardard, LineItem, IPieStardard } from 'src/app/Common/chartOption';


@Component({
  templateUrl: './ProtocolProfile.component.html',
})
export class ProtocolProfileComponent implements OnInit {
  constructor(private route: ActivatedRoute) { }
  _profile: IProfile;
  _ports = CommonFunction.clone(IPieStardard);
  _commonFunction = CommonFunction;
  _dist_Top5 = CommonFunction.clone(ILineStardard);
  _source_Top5 = CommonFunction.clone(ILineStardard);
  _source_dist_Top5 = CommonFunction.clone(ILineStardard);
  _protocolname = "";
  ngOnInit(): void {
    this.route.data
      .subscribe((xxx: { data: IProfile }) => {
        this._profile = xxx.data;
        this._protocolname = xxx.data.Name;
        this._ports.legend.data = this._profile.Ports.map(x => x.Name);
        this._ports.series[0].data = this._profile.Ports.map(x => { return { name: x.Name, value: x.Value } });

        this._dist_Top5.xAxis.data = this._profile.DistIps.slice(0, 5).map(x => x.Name);
        let distitem = CommonFunction.clone(LineItem);
        distitem.name = "目标TOP5";
        distitem.data = this._profile.DistIps.slice(0, 5).map(x => x.Value);
        distitem.type = "bar";
        this._dist_Top5.series.push(distitem);

        this._source_Top5.xAxis.data = this._profile.SourceIps.slice(0, 5).map(x => x.Name);
        let sourceitem = CommonFunction.clone(LineItem);
        sourceitem.name = "源头TOP5";
        sourceitem.data = this._profile.DistIps.slice(0, 5).map(x => x.Value);
        sourceitem.type = "bar";
        this._source_Top5.series.push(sourceitem);

        
        this._source_dist_Top5.xAxis.data = this._profile.Source_dist.slice(0, 5).map(this.source_dist_xAxis);
        this._source_dist_Top5.xAxis["axisLabel"] = { interval: 0 };
        let source_dist_item = CommonFunction.clone(LineItem);
        source_dist_item.name = "源头目标TOP5";
        source_dist_item.data = this._profile.Source_dist.slice(0, 5).map(x => x.Value);
        source_dist_item.type = "bar";
        this._source_dist_Top5.series.push(source_dist_item);


        this._profile.Top100HostInfo.forEach(
          el=>{
            let opt = CommonFunction.clone(IPieStardard);
            opt.series[0].center  =  ['50%', '50%'],
            opt.series[0].data = el.DistProtocols.map(x => { return { name: x.Name, value: x.Value } });
            opt.toolbox.show = false;
            el.option = opt;
          }
        );

      });
  }

  source_dist_xAxis(value: NameValueSet): string {
    var name = value.Name;
    var source = name.split("->")[0];
    var dist = name.split("->")[1];
    return source + "\n" + dist;
  }

}

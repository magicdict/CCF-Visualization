import { Component, OnInit } from '@angular/core';
import { CommonFunction } from 'src/app/Common/common';
import { ActivatedRoute } from '@angular/router';
import { IMapStardard } from 'src/app/Common/chartOption';


@Component({
  templateUrl: './SimpleMap.component.html',
})
export class KMeansMapComponent implements OnInit {
  constructor(private route: ActivatedRoute) { }
  _commonFunction = CommonFunction;
  _map = CommonFunction.clone(IMapStardard);
  _title = "";
  ngOnInit(): void {
    if (this.route.snapshot.routeConfig.path === "startkmeans") {
      this._title = "出发地 KMeans";
      this._map.series[0].data = [
        {name: "hotpoint",value: [110.3413402,20.02278797]},
        {name: "hotpoint",value: [110.3398491,20.01997581]},
        {name: "hotpoint",value: [110.4095,19.96735]},
        {name: "hotpoint",value: [110.3494042,20.01479375]},
        {name: "hotpoint",value: [110.3282778,20.01018889]},
        {name: "hotpoint",value: [110.342842,20.02159789]},
        {name: "hotpoint",value: [110.3472593,20.02269268]},
        {name: "hotpoint",value: [110.3461429,20.02018571]},
        {name: "hotpoint",value: [110.3400277,20.02255235]},
        {name: "hotpoint",value: [110.3432122,20.02247958]}
      ]
    } else {
      this._title = "目的地  KMeans";
      this._map.series[0].data = [
        {name: "hotpoint",value: [110.3392115,20.02169749]},
        {name: "hotpoint",value: [110.355,20.0217]},
        {name: "hotpoint",value: [110.3361636,20.02511818]},
        {name: "hotpoint",value: [110.3449688,20.02185469]},
        {name: "hotpoint",value: [110.3394469,20.02323526]},
        {name: "hotpoint",value: [110.40965,19.9663]},
        {name: "hotpoint",value: [110.340966,20.02142628]},
        {name: "hotpoint",value: [110.3383,20.00515]},
        {name: "hotpoint",value: [110.3454434,20.02120769]},
        {name: "hotpoint",value: [110.3379885,20.01564231]}
      ]
    }
  }
}


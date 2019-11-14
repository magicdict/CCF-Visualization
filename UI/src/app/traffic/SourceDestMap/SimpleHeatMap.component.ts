import { Component, OnInit } from '@angular/core';
import { CommonFunction } from 'src/app/Common/common';
import { ActivatedRoute } from '@angular/router';
import { IHeatBaiduMapStardard } from 'src/app/Common/chartOption';
import { IRelationship } from '../Model';


@Component({
    templateUrl: './SimpleMap.component.html',
})
export class SimpleHeatMapComponent implements OnInit {
    constructor(private route: ActivatedRoute) { }
    _commonFunction = CommonFunction;
    _map = CommonFunction.clone(IHeatBaiduMapStardard);
    _title = "";
    ngOnInit(): void {
        this.route.data
            .subscribe((xxx: { data: IRelationship[] }) => {

                console.log(this.route.snapshot.routeConfig.path)
                if (this.route.snapshot.routeConfig.path.endsWith("pagerank")) {
                    this._map.bmap.zoom = 10
                    this._map.visualMap.max = Math.max(...xxx.data.map(x => x.PageRank * 10000));
                    this._map.series[0].data = xxx.data.map(x => { return [x.coord[0], x.coord[1], x.PageRank * 100000] })
                    //排序
                    let sorted = xxx.data.sort((x, y) => { return y.PageRank - x.PageRank }).slice(0, 10);
                    this._map.series[1].data = sorted.map(x => { return { "name": "PR", value: [x.coord[0], x.coord[1], x.PageRank] } });
                    this._map.series[1].symbolSize = this.symbolSizeForPR;
                }
                if (this.route.snapshot.routeConfig.path.endsWith("betweenness")) {
                    this._map.bmap.zoom = 10
                    this._map.visualMap.max = Math.max(...xxx.data.map(x => x.Betweenness / 1000));
                    this._map.series[0].data = xxx.data.map(x => { return [x.coord[0], x.coord[1], x.Betweenness] })
                    //排序
                    let sorted = xxx.data.sort((x, y) => { return y.Betweenness - x.Betweenness }).slice(0, 10);
                    this._map.series[1].data = sorted.map(x => { return { "name": "BW", value: [x.coord[0], x.coord[1], x.Betweenness] } });
                    this._map.series[1].symbolSize = this.symbolSizeForBW;
                }
                if (this.route.snapshot.routeConfig.path.endsWith("enc")) {
                    this._map.bmap.zoom = 10
                    this._map.visualMap.max = Math.max(...xxx.data.map(x => x.ENC / 1000));
                    this._map.series[0].data = xxx.data.map(x => { return [x.coord[0], x.coord[1], x.ENC] })
                    //排序
                    let sorted = xxx.data.sort((x, y) => { return y.ENC - x.ENC }).slice(0, 10);
                    this._map.series[1].data = sorted.map(x => { return { "name": "ENC", value: [x.coord[0], x.coord[1], x.ENC] } });
                    this._map.series[1].symbolSize = this.symbolSizeForENC;
                }
            });
    }

    symbolSizeForPR(val: any) {
        return val[2] * 1000;
    };
    symbolSizeForBW(val: any) {
        return val[2] / 2000;
    };
    symbolSizeForENC(val: any) {
        return val[2] / 50;
    };
}


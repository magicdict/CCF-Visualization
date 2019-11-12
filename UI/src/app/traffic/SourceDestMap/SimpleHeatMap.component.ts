import { Component, OnInit } from '@angular/core';
import { CommonFunction } from 'src/app/Common/common';
import { ActivatedRoute } from '@angular/router';
import { IHeatMapStardard } from 'src/app/Common/chartOption';
import { IRelationship } from '../Model';


@Component({
    templateUrl: './SimpleMap.component.html',
})
export class SimpleHeatMapComponent implements OnInit {
    constructor(private route: ActivatedRoute) { }
    _commonFunction = CommonFunction;
    _map = CommonFunction.clone(IHeatMapStardard);
    _title = "";
    ngOnInit(): void {
        this.route.data
            .subscribe((xxx: { data: IRelationship[] }) => {

                console.log(this.route.snapshot.routeConfig.path ) 
                if (this.route.snapshot.routeConfig.path.endsWith("pagerank")){
                    this._map.bmap.zoom = 10
                    this._map.visualMap.max = Math.max(...xxx.data.map(x=>x.PageRank * 10000));
                    this._map.series[0].data = xxx.data.map(x => { return [x.coord[0], x.coord[1], x.PageRank * 100000] })
                }    
                if (this.route.snapshot.routeConfig.path.endsWith("betweenness")){
                    this._map.bmap.zoom = 10
                    this._map.visualMap.max = Math.max(...xxx.data.map(x=>x.PageRank * 10000));
                    this._map.series[0].data = xxx.data.map(x => { return [x.coord[0], x.coord[1], x.Betweenness * 100000] })
                }    
            });
    }
}


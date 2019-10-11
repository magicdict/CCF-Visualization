import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { gexf } from 'echarts/extension/dataTool';
import { CommonFunction } from 'src/app/Common/common';
import { IGephiStardard } from 'src/app/Common/chartOption';


@Component({
    templateUrl: './TrafficGephi.component.html',
})
export class TrafficGephiComponent implements OnInit {

    constructor(private route: ActivatedRoute) { }
    _gephi = CommonFunction.clone(IGephiStardard);
    _commonFunction = CommonFunction;

    ngOnInit(): void {
        this.route.data
            .subscribe((xxx: { data: any }) => {
                var graph = gexf.parse(xxx.data);
                var categories = [];
                graph.nodes.forEach(function (node) {
                    node.itemStyle = null;
                    node.value = node.symbolSize;
                });
                this._gephi.series[0].categories = categories;
                this._gephi.series[0].data = graph.nodes;
                this._gephi.series[0].links = graph.links;
            });
    }
}

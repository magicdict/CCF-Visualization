import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { gexf  } from 'echarts/extension/dataTool';
import { CommonFunction } from 'src/app/Common/common';


@Component({
  templateUrl: './TrafficGephi.component.html',
}) 
export class TrafficGephiComponent implements OnInit {

  constructor(private route: ActivatedRoute) { }
  _gephi:any;
  _commonFunction = CommonFunction;

  ngOnInit(): void {
    this.route.data
      .subscribe((xxx: { data: any }) => {
        var graph = gexf.parse(xxx.data);
        var categories = [];
        /* for (var i = 0; i < 9; i++) {
            categories[i] = {
                name: '类目' + i
            };
        } */
        graph.nodes.forEach(function (node) {
            node.itemStyle = null;
            node.value = node.symbolSize;
            node.symbolSize /= 1.5;
            node.label = {
                normal: {
                    show: node.symbolSize > 30
                }
            };
            //node.category = node.attributes.modularity_class;
        });
        this._gephi = {
            title: {
                text: 'Les Miserables',
                subtext: 'Default layout',
                top: 'bottom',
                left: 'right'
            },
            tooltip: {},
            legend: [{
                // selectedMode: 'single',
                data: categories.map(function (a) {
                    return a.name;
                })
            }],
            animationDuration: 1500,
            animationEasingUpdate: 'quinticInOut',
            series : [
                {
                    name: 'Les Miserables',
                    type: 'graph',
                    layout: 'none',
                    data: graph.nodes,
                    links: graph.links,
                    categories: categories,
                    roam: true,
                    focusNodeAdjacency: true,
                    itemStyle: {
                        normal: {
                            borderColor: '#fff',
                            borderWidth: 1,
                            shadowBlur: 10,
                            shadowColor: 'rgba(0, 0, 0, 0.3)'
                        }
                    },
                    label: {
                        position: 'right',
                        formatter: '{b}'
                    },
                    lineStyle: {
                        color: 'source',
                        curveness: 0.3
                    },
                    emphasis: {
                        lineStyle: {
                            width: 10
                        }
                    }
                }
            ]
        };
      });
  }
}

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonFunction } from 'src/app/Common/common';
import { ITopologyStardard } from 'src/app/Common/chartOption';


@Component({
    templateUrl: './Topology.component.html',
})
export class TopologyComponent implements OnInit {

    constructor(private route: ActivatedRoute) { }
    _topology = CommonFunction.clone(ITopologyStardard);
    _commonFunction = CommonFunction;


    //Test Start
    nodes = [{
        x: '1',
        y: '12',
        name: 'DNS',
        img: 'pc.png',
    },
    {
        x: '1',
        y: '3',
        name: '爬虫数据',
        img: 'console.png',
    },
    {
        x: '3',
        y: '12',
        name: '防火墙',
        img: 'firewall.png',
    },
    {
        x: '3',
        y: '8',
        name: 'DPI用户面',
        img: 'server.png',
    },
    {
        x: '3',
        y: '5',
        name: '场景特征',
        img: 'pc.png',
    },
    {
        x: '3',
        y: '1',
        name: '终端特征',
        img: 'pc.png',
    },
    {
        x: '5',
        y: '10',
        name: '增强特征',
        img: 'pc.png',
    },
    {
        x: '7',
        y: '12',
        name: 'HTTP',
        img: 'pc.png',
    },
    {
        x: '7',
        y: '8',
        name: '通用',
        img: 'pc.png',
    },
    {
        x: '9',
        y: '10',
        name: '用户中间表',
        img: 'pc.png',
    },
    {
        x: '9',
        y: '6',
        name: '网元信息表',
        img: 'pc.png',
    },
    {
        x: '11',
        y: '8',
        name: '质差网元',
        img: 'pc.png',
    },
    ];
    links = [{
        source: 'DNS',
        target: '防火墙',
        name: '访问'
    },
    {
        source: '爬虫数据',
        target: '场景特征',
        name: '访问'
    },
    {
        source: '爬虫数据',
        target: '终端特征',
        name: '访问'
    },
    {
        source: '防火墙',
        target: '增强特征',
        name: '访问'
    },
    {
        source: 'DPI用户面',
        target: '增强特征',
        name: '访问'
    },
    {
        source: '场景特征',
        target: '增强特征',
        name: '访问'
    },
    {
        source: '终端特征',
        target: '增强特征',
        name: '访问'
    },
    {
        source: '增强特征',
        target: 'HTTP',
        name: '访问'
    },
    {
        source: '增强特征',
        target: '通用',
        name: '访问'
    },
    {
        source: 'HTTP',
        target: '用户中间表',
        name: '访问'
    },
    {
        source: '通用',
        target: '用户中间表',
        name: '访问'
    },
    {
        source: '用户中间表',
        target: '质差网元',
        name: '访问'
    },
    {
        source: '网元信息表',
        target: '质差网元',
        name: '访问'
    }
    ];
    charts = {
        nodes: [],
        links: [],
        linesData: []
    }
    //Test End

    ngOnInit(): void {
        this.route.data
            .subscribe((xxx: { data: any }) => {
                this.init();
                this._topology.series[0].data = this.charts.nodes;
                this._topology.series[0].links = this.charts.links;    
                this._topology.series[1].data = this.charts.linesData;
            });
    }

    init() {
        let dataMap = new Map()
        for (var j = 0; j < this.nodes.length; j++) {
            var x = parseInt(this.nodes[j].x)
            var y = parseInt(this.nodes[j].y)
            var node = {
                name: this.nodes[j].name,
                value: [x, y],
                symbolSize: 50,
                //alarm: this.nodes[j].alarm,
                symbol: 'image:///assets/security/' + this.nodes[j].img,
                itemStyle: {
                    normal: {
                        color: '#12b5d0',
                    }
                }
            }
            dataMap.set(this.nodes[j].name, [x, y])
            this.charts.nodes.push(node)
        }
        for (var i = 0; i < this.links.length; i++) {
            var link = {
                source: this.links[i].source,
                target: this.links[i].target,
                label: {
                    normal: {
                        show: true,
                        formatter: this.links[i].name
                    }
                },
                lineStyle: {
                    normal: {
                        color: '#12b5d0'
                    }
                }
            }

            this.charts.links.push(link)
            // 组装动态移动的效果数据
            var lines = [{
                coord: dataMap.get(this.links[i].source)
            }, {
                coord: dataMap.get(this.links[i].target)
            }]
            this.charts.linesData.push(lines)
        }
    }

}

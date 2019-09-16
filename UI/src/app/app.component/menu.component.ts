import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './menu.component.html',
})
export class MenuComponent {
  data = [
    { name: '海门', value: 300 },
    { name: '鄂尔多斯', value: 120 },
    { name: '招远', value: 150 },
    { name: '舟山', value: 180 }
  ];
  data2 = [
    { name: '海门', value: 60 },
    { name: '鄂尔多斯', value: 133 },
    { name: '招远', value: 250 },
    { name: '舟山', value: 5 }
  ];
  data3 = [
    { name: '海门', value: 250 },
    { name: '鄂尔多斯', value: 22 },
    { name: '招远', value: 100 },
    { name: '舟山', value: 92 }
  ];

  geoCoordMap = {
    '海门': [121.15, 31.89],
    '鄂尔多斯': [109.781327, 39.608266],
    '招远': [120.38, 37.35],
    '舟山': [122.207216, 29.985295]
  };
  convertData(data) {
    var res = [];
    for (var i = 0; i < data.length; i++) {
      var geoCoord = this.geoCoordMap[data[i].name];
      if (geoCoord) {
        res.push({
          name: data[i].name,
          value: geoCoord.concat(data[i].value)
        });
      }
    }
    return res;
  };
  options = {
    baseOption: {
      timeline: {
        show: true,
        autoPlay: true,
        playInterval: 1000,
        data: ['2002-01-01', '2003-01-01', '2004-01-01']
      },
      bmap: {
        center: [104.114129, 37.550339],
        zoom: 5,
        roam: false
      },
      toolbox: {
        'show': true,
        'feature': {
          'saveAsImage': {},
        }
      },
      title: [],
      series: [{
        name: 'pm2.5',
        type: 'scatter',
        coordinateSystem: 'bmap',
        symbol: 'pin',
        symbolSize: 100,
        label: {
          normal: {
            formatter: function (d) {
              return d.name + "\n" + d.value[2] + 'μg/m³';
            },
            textStyle: {
              fontSize: 14
            },
            show: true
          }
        }
      }]
    },
    options: [
      {
        series: [{
          data: this.convertData(this.data)
        }]
      },
      {
        series: [{
          data: this.convertData(this.data2)
        }]
      },
      {
        series: [{
          data: this.convertData(this.data3)
        }]
      }
    ]
  }
}

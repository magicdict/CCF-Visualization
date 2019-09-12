

export const IPieStardard = {
  title: {
    text: '',
  },
  tooltip: {
    trigger: 'item',
    formatter: "{a} <br/>{b} : {c} ({d}%)"
  },
  toolbox: {
    'show': true,
    'feature': {
      'saveAsImage': {},
    }
  },
  legend: {
    //orient: 'vertical',
    //left: 'left',
    data: []
  },
  series: [
    {
      name: '',
      type: 'pie',
      radius: '55%',
      center: ['50%', '60%'],
      data: [],
      itemStyle: {
        emphasis: {
          shadowBlur: 10,
          shadowOffsetX: 0,
          shadowColor: 'rgba(0, 0, 0, 0.5)'
        }
      }
    }
  ]
};

export const IPolarStardard = {
  angleAxis: {
    type: 'category',
    data: [],   //整体数据要设定一下，不然数据将越过坐标范围
    z: 10,
    interval: 50
  },
  title: {
    text: ""
  },
  toolbox: {
    'show': true,
    'feature': {
      'saveAsImage': {},
    }
  },
  tooltip: {
    trigger: 'axis',
    axisPointer: {
      type: 'cross'
    }
  },
  radiusAxis: {
  },
  polar: {
    radius: '70%'
  },
  series: [],
  legend: {
    //show: true,
    //top: 30,
    data: []
  }
};


export const PolarItem = {
  type: 'bar',
  data: [],
  coordinateSystem: 'polar',
  name: '',
  stack: 'stackname'
}



export const ILineStardard = {
  title: {
    text: ''
  },
  tooltip: {
    trigger: 'axis'
  },
  legend: {
    data: []
  },
  grid: {
    left: '3%',
    right: '4%',
    bottom: '3%',
    containLabel: true
  },
  toolbox: {
    feature: {
      saveAsImage: {}
    }
  },
  xAxis: {
    type: 'category',
    boundaryGap: false,
    data: []
  },
  yAxis: {
    type: 'value'
  },
  series: [
  ]
};


export const LineItem = {
  name: '',
  type: 'line',
  data: []
};


export const IBarStardard = {
  xAxis: {
    type: 'category',
    data: []
  },
  yAxis: {
    type: 'value'
  },
  series: [{
    data: [],
    type: 'bar'
  }]
};


export const I3DarStardard = {
  visualMap: {
    max: 300,
    show: false,
    inRange: {
      color: ['#313695', '#4575b4', '#74add1', '#abd9e9',
        '#e0f3f8', '#ffffbf', '#fee090', '#fdae61',
        '#f46d43', '#d73027', '#a50026']
    }
  },
  xAxis3D: {
    type: 'category',
    data: [],
    name: '时间'
  },
  yAxis3D: {
    type: 'category',
    data: [],
    name: '日期'
  },
  zAxis3D: {
    type: 'value',
    name: '流量'
  },
  grid3D: {
    boxWidth: 200,
    boxDepth: 80,
    viewControl: {
      projection: 'orthographic',
    },
    light: {
      main: {
        intensity: 1.2
      },
      ambient: {
        intensity: 0.3
      }
    }
  },
  series: [{
    type: 'bar3D',
    data: [],
    shading: 'color',
    label: {
      formatter: '{c}'
    },
    itemStyle: {
      opacity: 0.4
    },
  }]
};


export const IMapStardard = {
  title: {
    text: '',
    left: 'center'
  },
  tooltip: {
    trigger: 'item',
    formatter:null,
  },
  bmap: {
    center: [110.3373, 20.0303],
    zoom: 15,
    roam: true,
    mapStyle: {
      styleJson: [{
        'featureType': 'water',
        'elementType': 'all',
        'stylers': {
          'color': '#d1d1d1'
        }
      }, {
        'featureType': 'land',
        'elementType': 'all',
        'stylers': {
          'color': '#f3f3f3'
        }
      }, {
        'featureType': 'railway',
        'elementType': 'all',
        'stylers': {
          'visibility': 'off'
        }
      }, {
        'featureType': 'highway',
        'elementType': 'all',
        'stylers': {
          'color': '#fdfdfd'
        }
      }, {
        'featureType': 'highway',
        'elementType': 'labels',
        'stylers': {
          'visibility': 'off'
        }
      }, {
        'featureType': 'arterial',
        'elementType': 'geometry',
        'stylers': {
          'color': '#fefefe'
        }
      }, {
        'featureType': 'arterial',
        'elementType': 'geometry.fill',
        'stylers': {
          'color': '#fefefe'
        }
      }, {
        'featureType': 'poi',
        'elementType': 'all',
        'stylers': {
          'visibility': 'on'
        }
      }, {
        'featureType': 'green',
        'elementType': 'all',
        'stylers': {
          'visibility': 'off'
        }
      }, {
        'featureType': 'subway',
        'elementType': 'all',
        'stylers': {
          'visibility': 'off'
        }
      }, {
        'featureType': 'manmade',
        'elementType': 'all',
        'stylers': {
          'color': '#d1d1d1'
        }
      }, {
        'featureType': 'local',
        'elementType': 'all',
        'stylers': {
          'color': '#d1d1d1'
        }
      }, {
        'featureType': 'arterial',
        'elementType': 'labels',
        'stylers': {
          'visibility': 'off'
        }
      }, {
        'featureType': 'boundary',
        'elementType': 'all',
        'stylers': {
          'color': '#fefefe'
        }
      }, {
        'featureType': 'building',
        'elementType': 'all',
        'stylers': {
          'color': '#d1d1d1'
        }
      }, {
        'featureType': 'label',
        'elementType': 'labels.text.fill',
        'stylers': {
          'color': '#999999'
        }
      }]
    }
  },
  series: [
    {
      name: '订单数',
      type: 'scatter',
      coordinateSystem: 'bmap',
      data: [],
      label: {
        normal: {
          formatter: '{b}',
          position: 'right',
          show: false
        },
        emphasis: {
          show: true
        }
      },
      itemStyle: {
        normal: {
          color: 'purple'
        }
      }
    },
    {
      name: 'Top 5',
      type: 'effectScatter',
      coordinateSystem: 'bmap',
      data: [],
      symbolSize: function (val) {
        return val[1]/10;
      },
      showEffectOn: 'render',
      rippleEffect: {
        brushType: 'stroke'
      },
      hoverAnimation: true,
      label: {
        normal: {
          formatter: '{b}',
          position: 'right',
          show: true
        }
      },
      itemStyle: {
        normal: {
          color: 'purple',
          shadowBlur: 10,
          shadowColor: '#333'
        }
      },
      zlevel: 1
    }
  ]
};

export const ICalendarStardard = {
  tooltip: {
    formatter: null
  },
  visualMap: {
    show: false,
    min: 0,
    max: 300,
    calculable: true,
    seriesIndex: [2],
    orient: 'horizontal',
    left: 'center',
    bottom: 20,
    inRange: {
      color: ['#e0ffff', '#006edd'],
      opacity: 0.3
    },
    controller: {
      inRange: {
        opacity: 0.5
      }
    }
  },

  calendar: [{
    left: 'center',
    top: 'middle',
    cellSize: [65, 65],
    yearLabel: { show: false },
    orient: 'vertical',
    dayLabel: {
      firstDay: 1,
      nameMap: 'cn'
    },
    monthLabel: {
      show: false
    },
    range: []
  }],

  series: []
};

export const ICalendarItem_scatter = {
  type: 'scatter',
  coordinateSystem: 'calendar',
  symbolSize: 1,
  label: {
    normal: {
      show: true,
      formatter: null,
      textStyle: {
        color: '#000'
      }
    }
  },
  data: []
};

export const ICalendarItem_heatmap =  {
  name: '降雨量',
  type: 'heatmap',
  coordinateSystem: 'calendar',
  data: []
};


export const IStardardPie = {
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

export const IStardardPolar = {
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
        text: '折线图堆叠'
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
  }
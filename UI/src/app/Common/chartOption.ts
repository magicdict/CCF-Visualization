

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
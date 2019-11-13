import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IDiaryinfo, IWeather } from '../Model';
import { ICalendarStardard, ICalendarItem_scatter, ICalendarItem_heatmap, Rich_Weathy } from 'src/app/Common/chartOption';
import { CommonFunction } from 'src/app/Common/common';


@Component({
  templateUrl: './Calendar.component.html',
})
export class CalendarComponent implements OnInit {

  constructor(private route: ActivatedRoute) { }
  _dashboard: IDiaryinfo[];
  _commonFunction = CommonFunction;
  _calendar5 = CommonFunction.clone(ICalendarStardard);
  _calendar6 = CommonFunction.clone(ICalendarStardard);
  _calendar7 = CommonFunction.clone(ICalendarStardard);
  _calendar8 = CommonFunction.clone(ICalendarStardard);
  _calendar9 = CommonFunction.clone(ICalendarStardard);
  _calendar10 = CommonFunction.clone(ICalendarStardard);


  symbol(val: any) {
    let strDate: string = val.data[0];
    let weather: IWeather = val.data[4];
    let isWorkday: boolean = val.data[3];
    strDate = (isWorkday ? "{bluebold|" : "{redbold|") + strDate.substr(5) + "}";
    let weatherImg1 = CommonFunction.getImage(weather.Description.replace(" ", "").split("/")[0]);
    let weatherImg2 = CommonFunction.getImage(weather.Description.replace(" ", "").split("/")[1]);
    return [
      strDate,"",
      weatherImg1 + weather.Tempera.replace(" / ", "/").split("/")[0] + "        ",
      "        " + weatherImg2 + weather.Tempera.replace(" / ", "/").split("/")[1],"",
      "{blackbold|" + val.data[2] + "}"
    ].join('\n');
  }

  _title = "全体";

  ngOnInit(): void {
    this.route.data
      .subscribe((xxx: { data: IDiaryinfo[] }) => {
        this._dashboard = xxx.data;
        this._calendar5.calendar[0].range = ['2017-05'];
        this._calendar5.visualMap.seriesIndex = [1];
        //this._calendar5.visualMap.inRange = null;
        let data = xxx.data.map(x => [x.Name, 1, x.Value.holiday, x.Value.isWorkday, x.Value.weather]);
        let item = CommonFunction.clone(ICalendarItem_scatter);
        item.label.normal.formatter = this.symbol;
        item.label.normal['rich'] = CommonFunction.clone(Rich_Weathy);
        item.data = data;
        this._calendar5.series.push(item);
        let heatmapitem = CommonFunction.clone(ICalendarItem_heatmap);

        switch (this.route.snapshot.routeConfig.path) {
          case "calendar":
            this._title = "全体";
            heatmapitem.data = xxx.data.map(x => [x.Name, x.Value.ordercnt]);
            this._calendar5.series.push(heatmapitem);
            this._calendar5.visualMap.max = 120000;
            this._calendar5.visualMap.min = 50000;
            break;
          case "calendar_airport":
            this._title = "机场";
            heatmapitem.data = xxx.data.map(x => [x.Name, x.Value.airport]);
            this._calendar5.series.push(heatmapitem);
            this._calendar5.visualMap.max = 10000;
            this._calendar5.visualMap.min = 2000;
            break;
          case "calendar_longbus":
            this._title = "汽车站";
            heatmapitem.data = xxx.data.map(x => [x.Name, x.Value.longbus]);
            this._calendar5.series.push(heatmapitem);
            this._calendar5.visualMap.max = 500;
            this._calendar5.visualMap.min = 2000;
            break;
          case "calendar_train":
            this._title = "火车站";
            heatmapitem.data = xxx.data.map(x => [x.Name, x.Value.train]);
            this._calendar5.series.push(heatmapitem);
            this._calendar5.visualMap.max = 10000;
            this._calendar5.visualMap.min = 2000;
            break;
          case "calendar_cbd":
            this._title = "商圈";
            heatmapitem.data = xxx.data.map(x => [x.Name, x.Value.cbd]);
            this._calendar5.series.push(heatmapitem);
            this._calendar5.visualMap.max = 10000;
            this._calendar5.visualMap.min = 2000;
            break;
          case "calendar_hospital":
            this._title = "医院";
            heatmapitem.data = xxx.data.map(x => [x.Name, x.Value.hospital]);
            this._calendar5.series.push(heatmapitem);
            this._calendar5.visualMap.max = 1500;
            this._calendar5.visualMap.min = 200;
            break;
          case "calendar_school":
            this._title = "学校";
            heatmapitem.data = xxx.data.map(x => [x.Name, x.Value.school]);
            this._calendar5.series.push(heatmapitem);
            this._calendar5.visualMap.max = 2500;
            this._calendar5.visualMap.min = 1000;
            break;
          case "calendar_travel":
            this._title = "景点";
            heatmapitem.data = xxx.data.map(x => [x.Name, x.Value.travel]);
            this._calendar5.series.push(heatmapitem);
            this._calendar5.visualMap.max = 2000;
            this._calendar5.visualMap.min = 1000;
            break;
          default:
            break;
        }



        this._calendar6 = CommonFunction.clone(this._calendar5);
        this._calendar6.calendar[0].range = ['2017-06'];
        this._calendar6.series[0].label.normal.formatter = this.symbol;  //方法无法克隆

        this._calendar7 = CommonFunction.clone(this._calendar5);
        this._calendar7.calendar[0].range = ['2017-07'];
        this._calendar7.series[0].label.normal.formatter = this.symbol;

        this._calendar8 = CommonFunction.clone(this._calendar5);
        this._calendar8.calendar[0].range = ['2017-08'];
        this._calendar8.series[0].label.normal.formatter = this.symbol;

        this._calendar9 = CommonFunction.clone(this._calendar5);
        this._calendar9.calendar[0].range = ['2017-09'];
        this._calendar9.series[0].label.normal.formatter = this.symbol;

        this._calendar10 = CommonFunction.clone(this._calendar5);
        this._calendar10.calendar[0].range = ['2017-10'];
        this._calendar10.series[0].label.normal.formatter = this.symbol;


      });
  }
}

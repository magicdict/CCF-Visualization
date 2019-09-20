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
    strDate = strDate.substr(5);
    let weather: IWeather = val.data[4];
    let isWorkday: boolean = val.data[3];
    let workday = isWorkday ? "" : "休息日";
    let weatherImg1 = CommonFunction.getImage(weather.Description.replace(" ", "").split("/")[0]);
    let weatherImg2 = CommonFunction.getImage(weather.Description.replace(" ", "").split("/")[1]);
    return [
      strDate,
      weatherImg1 + weather.Tempera.replace(" / ", "/").split("/")[0],
      weatherImg2 + weather.Tempera.replace(" / ", "/").split("/")[1],
      val.data[2] + " " + workday
    ].join('\n');
  }



  ngOnInit(): void {
    this.route.data
      .subscribe((xxx: { data: IDiaryinfo[] }) => {
        this._dashboard = xxx.data;
        this._calendar5.calendar[0].range = ['2017-05'];
        this._calendar5.visualMap.max = 120000;
        this._calendar5.visualMap.min = 50000;
        this._calendar5.visualMap.seriesIndex = [1];
        this._calendar5.visualMap.inRange = null;
        let data = xxx.data.map(x => [x.Name, 1, x.Value.holiday, x.Value.isWorkday, x.Value.weather]);
        let item = CommonFunction.clone(ICalendarItem_scatter);
        item.label.normal.formatter = this.symbol;
        item.label.normal['rich'] = CommonFunction.clone(Rich_Weathy);
        item.data = data;
        this._calendar5.series.push(item);
        let heatmapitem = CommonFunction.clone(ICalendarItem_heatmap);
        heatmapitem.data = xxx.data.map(x => [x.Name, x.Value.ordercnt]);
        this._calendar5.series.push(heatmapitem);

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

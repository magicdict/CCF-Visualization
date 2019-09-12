import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IDiaryinfo, IWeather } from '../Model';
import { ICalendarStardard, ICalendarItem_scatter, ICalendarItem_heatmap } from 'src/app/Common/chartOption';
import { CommonFunction } from 'src/app/Common/common';


@Component({
  templateUrl: './Calendar.component.html',
})
export class CalendarComponent implements OnInit {

  constructor(private route: ActivatedRoute) { }
  _dashboard: IDiaryinfo[];
  _commonFunction = CommonFunction;
  _calendar56 = CommonFunction.clone(ICalendarStardard);
  _calendar78 = CommonFunction.clone(ICalendarStardard);
  _calendar910 = CommonFunction.clone(ICalendarStardard);

  symbolSizeCnt(val: any) {
    let x: string = val.data[0];
    x = x.substr(5) + val.data[2];
    let weather: IWeather = val.data[4];
    x = x + "\n" + weather.Description.replace(" ", "").split("/")[0] + weather.Tempera.replace(" / ", "/").split("/")[0];
    x = x + "\n" + weather.Description.replace(" ", "").split("/")[1] + weather.Tempera.replace(" / ", "/").split("/")[1];
    let isWorkday: boolean = val.data[3];
    if (!isWorkday) {
      x = x + "\n" + "休息日";
    }
    console.log(x);
    return x;
  }

  ngOnInit(): void {
    this.route.data
      .subscribe((xxx: { data: IDiaryinfo[] }) => {
        this._dashboard = xxx.data;
        this._calendar56.calendar[0].range = ['2017-05-01', '2017-06-30'];
        this._calendar56.visualMap.max = 120000;
        this._calendar56.visualMap.min = 50000;

        this._calendar56.visualMap.seriesIndex = [1];
        this._calendar56.visualMap.inRange = null;
        let data = xxx.data.map(x => [x.Name, 1, x.Value.holiday, x.Value.isWorkday, x.Value.weather]);

        let item = CommonFunction.clone(ICalendarItem_scatter);
        item.label.normal.formatter = this.symbolSizeCnt;
        item.data = data;
        this._calendar56.series.push(item);

        let heatmapitem = CommonFunction.clone(ICalendarItem_heatmap);
        heatmapitem.data = xxx.data.map(x => [x.Name, x.Value.ordercnt]);
        this._calendar56.series.push(heatmapitem);

        this._calendar78 = CommonFunction.clone(this._calendar56);
        this._calendar78.series = [];
        this._calendar78.series.push(item);
        this._calendar78.series.push(heatmapitem);
        this._calendar78.calendar[0].range = ['2017-07-01', '2017-08-31'];


        this._calendar910 = CommonFunction.clone(this._calendar56);
        this._calendar910.series = [];
        this._calendar910.series.push(item);
        this._calendar910.series.push(heatmapitem);
        this._calendar910.calendar[0].range = ['2017-09-01', '2017-10-31'];

      });
  }
}

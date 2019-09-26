import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IServerInfo } from '../Model';
import { ITreeStardard } from 'src/app/Common/chartOption';
import { CommonFunction } from 'src/app/Common/common';


@Component({
  templateUrl: './IpSegment.component.html',
})
export class IpSegmentComponent implements OnInit {
  constructor(private route: ActivatedRoute) { }
  _tree = CommonFunction.clone(ITreeStardard);
  _commonFunction = CommonFunction;

  ngOnInit(): void {
    this.route.data
      .subscribe((xxx: { data: any }) => {
        this._tree.series[0].data.push(xxx.data);
      });
  }
}

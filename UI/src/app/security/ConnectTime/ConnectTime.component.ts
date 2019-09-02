import { Component } from '@angular/core';
import { CommonFunction } from 'src/app/Common/common';

@Component({
  selector: 'app-root',
  templateUrl: './ConnectTime.component.html',
})
export class ConnectTimeComponent {
  title = '企业网络资产及安全事件分析与可视化';  
  constructor(public commonFunction: CommonFunction) {

  }
  GetData(){
    this.commonFunction.httpRequestGet("GetProtocols");
  }

}
 
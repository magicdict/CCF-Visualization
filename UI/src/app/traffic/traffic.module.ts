import { NgModule } from '@angular/core';
import { TrafficRoutingModule } from './traffic-routing.module';
import { TimeAnaysisResolver, SourceMapResolver, DestMapResolver, DashBoardResolver } from './resolver.service';
import { CommonFunction } from '../Common/common';
import { HttpClientModule } from '@angular/common/http';
import { MyCommonModule } from '../Common/MyCommon.module';

import { TrafficMainComponent } from './TrafficMain.component';
import { TimeAnalysisComponent } from './TimeAnalysis/TimeAnalysis.component';
import { NgxEchartsModule } from 'ngx-echarts';
import { DestMapComponent } from './SourceDestMap/DestMap.component';
import { SourceMapComponent } from './SourceDestMap/SourceMap.component';

@NgModule({
  declarations: [
    TrafficMainComponent,
    SourceMapComponent,
    DestMapComponent,
    TimeAnalysisComponent
  ],
  imports: [
    TrafficRoutingModule,
    HttpClientModule,
    MyCommonModule,
    NgxEchartsModule
  ],
  providers: [
    CommonFunction,
    DashBoardResolver,
    TimeAnaysisResolver,
    SourceMapResolver,
    DestMapResolver,
  ],
  bootstrap: [TrafficMainComponent]
})
export class TrafficModule { }

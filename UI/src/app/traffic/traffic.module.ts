import { NgModule } from '@angular/core';
import { TrafficRoutingModule } from './traffic-routing.module';
import { TimeAnaysisResolver, SourceMapResolver, DestMapResolver, DashBoardResolver, CalendarResolver } from './resolver.service';
import { CommonFunction } from '../Common/common';
import { HttpClientModule } from '@angular/common/http';
import { MyCommonModule } from '../Common/MyCommon.module';

import { TrafficMainComponent } from './TrafficMain.component';
import { TimeAnalysisComponent } from './TimeAnalysis/TimeAnalysis.component';
import { NgxEchartsModule } from 'ngx-echarts';
import { DestMapComponent } from './SourceDestMap/DestMap.component';
import { SourceMapComponent } from './SourceDestMap/SourceMap.component';
import { DashboardComponent } from './Dashboard/Dashboard.component';
import { CalendarComponent } from './Calendar/Calendar.component';

@NgModule({
  declarations: [
    TrafficMainComponent,
    DashboardComponent,
    SourceMapComponent,
    DestMapComponent,
    TimeAnalysisComponent,
    CalendarComponent
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
    CalendarResolver
  ],
  bootstrap: [TrafficMainComponent]
})
export class TrafficModule { }

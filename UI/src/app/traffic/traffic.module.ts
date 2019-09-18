import { NgModule } from '@angular/core';
import { TrafficRoutingModule } from './traffic-routing.module';
import { TimeAnaysisResolver, SourceMapResolver, DestMapResolver, DashBoardResolver, 
  CalendarResolver, SimpleDestMapResolver, SimpleSourceMapResolver, TraceResolver, SourceMapWeeKnoResolver, DestMapWeeKnoResolver } from './resolver.service';
import { CommonFunction } from '../Common/common';
import { HttpClientModule } from '@angular/common/http';
import { MyCommonModule } from '../Common/MyCommon.module';

import { TrafficMainComponent } from './TrafficMain.component';
import { TimeAnalysisComponent } from './TimeAnalysis/TimeAnalysis.component';
import { NgxEchartsModule } from 'ngx-echarts';

import { TimeLineMapComponent } from './SourceDestMap/TimeLineMap.component';
import { DashboardComponent } from './Dashboard/Dashboard.component';
import { CalendarComponent } from './Calendar/Calendar.component';
import { SimpleMapComponent } from './SourceDestMap/SimpleMap.component';
import { TraceMapComponent } from './SourceDestMap/TraceMap.component';
import { TimeLineMapWeekNoComponent } from './SourceDestMap/TimeLineMapWeekNo.component';

@NgModule({
  declarations: [
    TrafficMainComponent,
    DashboardComponent,
    TimeLineMapComponent,
    TimeLineMapWeekNoComponent,
    SimpleMapComponent,
    TraceMapComponent,
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
    SimpleSourceMapResolver,
    SimpleDestMapResolver,
    SourceMapResolver,
    DestMapResolver,
    SourceMapWeeKnoResolver,
    DestMapWeeKnoResolver,
    CalendarResolver,
    TraceResolver
  ],
  bootstrap: [TrafficMainComponent]
})
export class TrafficModule { }

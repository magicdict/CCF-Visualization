import { NgModule } from '@angular/core';
import { TrafficRoutingModule } from './traffic-routing.module';
import { TimeAnaysisResolver, SourceMapResolver, DestMapResolver, DashBoardResolver, 
  CalendarResolver, SimpleDestMapResolver, SimpleSourceMapResolver, TraceResolver, SourceMapWeeKnoResolver, DestMapWeeKnoResolver, HotPointMapResolver, HeatMapResolver, AirportMapResolver } from './resolver.service';
import { CommonFunction } from '../Common/common';
import { HttpClientModule } from '@angular/common/http';
import { MyCommonModule } from '../Common/MyCommon.module';

import { TrafficMainComponent } from './TrafficMain/TrafficMain.component';
import { TimeAnalysisComponent } from './TimeAnalysis/TimeAnalysis.component';
import { NgxEchartsModule } from 'ngx-echarts';

import { TimeLineMapComponent } from './SourceDestMap/TimeLineMap.component';
import { DashboardComponent } from './Dashboard/Dashboard.component';
import { CalendarComponent } from './Calendar/Calendar.component';
import { SimpleMapComponent } from './SourceDestMap/SimpleMap.component';
import { TraceMapComponent } from './SourceDestMap/TraceMap.component';
import { TimeLineMapWeekNoComponent } from './SourceDestMap/TimeLineMapWeekNo.component';

import { TabViewModule } from 'primeng/tabview';
import { HotPointMapComponent } from './SourceDestMap/HotPointMap.component';
import { TimeDistanceComponent } from './Dashboard/TimeDistance.component';
import { TimeAnalysisSPeedComponent } from './TimeAnalysis/TimeAnalysisSpeed.component';
import { KMeansMapComponent } from './SourceDestMap/KMeansMap.component';
import { SimpleHeatMapComponent } from './SourceDestMap/SimpleHeatMap.component';
import { ArimaComponent } from './ML/Arima.component';
import { CCComponent } from './ML/CC.component';



@NgModule({
  declarations: [
    TrafficMainComponent,
    DashboardComponent,
    TimeDistanceComponent,
    TimeLineMapComponent,
    TimeLineMapWeekNoComponent,
    HotPointMapComponent,
    SimpleMapComponent,
    KMeansMapComponent,
    TraceMapComponent,
    TimeAnalysisComponent,
    CalendarComponent,
    TimeAnalysisSPeedComponent,
    SimpleHeatMapComponent,
    ArimaComponent,
    CCComponent
  ],
  imports: [
    TrafficRoutingModule,
    HttpClientModule,
    MyCommonModule,
    NgxEchartsModule,
    TabViewModule,
  ],
  providers: [
    CommonFunction,
    DashBoardResolver,
    TimeAnaysisResolver,
    HotPointMapResolver,
    SimpleSourceMapResolver,
    SimpleDestMapResolver,
    SourceMapResolver,
    DestMapResolver,
    SourceMapWeeKnoResolver,
    DestMapWeeKnoResolver,
    CalendarResolver,
    TraceResolver,
    HeatMapResolver,
    AirportMapResolver
  ],
  bootstrap: [TrafficMainComponent]
})
export class TrafficModule { }

import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TrafficMainComponent } from './TrafficMain/TrafficMain.component';
import { TimeAnalysisComponent } from './TimeAnalysis/TimeAnalysis.component';
import { TimeLineMapComponent } from './SourceDestMap/TimeLineMap.component';
import {
  SourceMapResolver, DestMapResolver, TimeAnaysisResolver, DashBoardResolver,
  CalendarResolver, SimpleSourceMapResolver, SimpleDestMapResolver, TraceResolver, SourceMapWeeKnoResolver, DestMapWeeKnoResolver, HotPointMapResolver
} from './resolver.service';
import { DashboardComponent } from './Dashboard/Dashboard.component';
import { CalendarComponent } from './Calendar/Calendar.component';
import { SimpleMapComponent } from './SourceDestMap/SimpleMap.component';
import { TraceMapComponent } from './SourceDestMap/TraceMap.component';
import { TimeLineMapWeekNoComponent } from './SourceDestMap/TimeLineMapWeekNo.component';
import { HotPointMapComponent } from './SourceDestMap/HotPointMap.component';
import { TimeDistanceComponent } from './Dashboard/TimeDistance.component';
import { TimeAnalysisSPeedComponent } from './TimeAnalysis/TimeAnalysisSpeed.component';


const routes: Routes = [
  {
    path: 'traffic', component: TrafficMainComponent,
    children: [
      { path: 'dashboard', component: DashboardComponent, resolve: { data: DashBoardResolver } },
      { path: 'dashboard_timedistance', component: TimeDistanceComponent, resolve: { data: DashBoardResolver } },

      { path: 'speedtimeanalysis', component: TimeAnalysisSPeedComponent, resolve: { data: DashBoardResolver } },
      { path: 'speedtimeanalysis_airport', component: TimeAnalysisSPeedComponent, resolve: { data: DashBoardResolver } },
      { path: 'speedtimeanalysis_train', component: TimeAnalysisSPeedComponent, resolve: { data: DashBoardResolver } },
      { path: 'speedtimeanalysis_longbus', component: TimeAnalysisSPeedComponent, resolve: { data: DashBoardResolver } },
      { path: 'speedtimeanalysis_cbd', component: TimeAnalysisSPeedComponent, resolve: { data: DashBoardResolver } },
      { path: 'speedtimeanalysis_hospital', component: TimeAnalysisSPeedComponent, resolve: { data: DashBoardResolver } },
      { path: 'speedtimeanalysis_school', component: TimeAnalysisSPeedComponent, resolve: { data: DashBoardResolver } },
      { path: 'speedtimeanalysis_travel', component: TimeAnalysisSPeedComponent, resolve: { data: DashBoardResolver } },

      { path: 'calendar', component: CalendarComponent, resolve: { data: CalendarResolver } },
      { path: 'calendar_airport', component: CalendarComponent, resolve: { data: CalendarResolver } },
      { path: 'calendar_train', component: CalendarComponent, resolve: { data: CalendarResolver } },
      { path: 'calendar_longbus', component: CalendarComponent, resolve: { data: CalendarResolver } },
      { path: 'calendar_cbd', component: CalendarComponent, resolve: { data: CalendarResolver } },
      { path: 'calendar_hospital', component: CalendarComponent, resolve: { data: CalendarResolver } },
      { path: 'calendar_school', component: CalendarComponent, resolve: { data: CalendarResolver } },
      { path: 'calendar_travel', component: CalendarComponent, resolve: { data: CalendarResolver } },


      { path: 'timeanalysis', component: TimeAnalysisComponent, resolve: { data: TimeAnaysisResolver } },
      { path: 'timeanalysis_airport', component: TimeAnalysisComponent, resolve: { data: TimeAnaysisResolver } },
      { path: 'timeanalysis_train', component: TimeAnalysisComponent, resolve: { data: TimeAnaysisResolver } },
      { path: 'timeanalysis_longbus', component: TimeAnalysisComponent, resolve: { data: TimeAnaysisResolver } },
      { path: 'timeanalysis_cbd', component: TimeAnalysisComponent, resolve: { data: TimeAnaysisResolver } },
      { path: 'timeanalysis_hospital', component: TimeAnalysisComponent, resolve: { data: TimeAnaysisResolver } },
      { path: 'timeanalysis_school', component: TimeAnalysisComponent, resolve: { data: TimeAnaysisResolver } },
      { path: 'timeanalysis_travel', component: TimeAnalysisComponent, resolve: { data: TimeAnaysisResolver } },

      { path: 'hotpoint', component: HotPointMapComponent, resolve: { data: HotPointMapResolver } },
      { path: 'simplesourcemap', component: SimpleMapComponent, resolve: { data: SimpleSourceMapResolver } },
      { path: 'simpledestmap', component: SimpleMapComponent, resolve: { data: SimpleDestMapResolver } },
      { path: 'sourcemap', component: TimeLineMapComponent, resolve: { data: SourceMapResolver } },
      { path: 'destmap', component: TimeLineMapComponent, resolve: { data: DestMapResolver } },
      { path: 'weeklysourcemap', component: TimeLineMapWeekNoComponent, resolve: { data: SourceMapWeeKnoResolver } },
      { path: 'weeklydestmap', component: TimeLineMapWeekNoComponent, resolve: { data: DestMapWeeKnoResolver } },
      { path: 'trace', component: TraceMapComponent, resolve: { data: TraceResolver } },
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TrafficRoutingModule { }

import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TrafficMainComponent } from './TrafficMain/TrafficMain.component';
import { TimeAnalysisComponent } from './TimeAnalysis/TimeAnalysis.component';
import { TimeLineMapComponent } from './SourceDestMap/TimeLineMap.component';
import { SourceMapResolver, DestMapResolver, TimeAnaysisResolver, DashBoardResolver, 
  CalendarResolver, SimpleSourceMapResolver, SimpleDestMapResolver, TraceResolver, SourceMapWeeKnoResolver, DestMapWeeKnoResolver, LongWaitMapResolver } from './resolver.service';
import { DashboardComponent } from './Dashboard/Dashboard.component';
import { CalendarComponent } from './Calendar/Calendar.component';
import { SimpleMapComponent } from './SourceDestMap/SimpleMap.component';
import { TraceMapComponent } from './SourceDestMap/TraceMap.component';
import { TimeLineMapWeekNoComponent } from './SourceDestMap/TimeLineMapWeekNo.component';


const routes: Routes = [
  {
    path: 'traffic', component: TrafficMainComponent,
    children: [
      { path: 'dashboard', component: DashboardComponent, resolve: { data: DashBoardResolver } },
      { path: 'timeanalysis', component: TimeAnalysisComponent, resolve: { data: TimeAnaysisResolver } },
      { path: 'longwait', component: SimpleMapComponent, resolve: { data: LongWaitMapResolver } },
      { path: 'simplesourcemap', component: SimpleMapComponent, resolve: { data: SimpleSourceMapResolver } },
      { path: 'simpledestmap', component: SimpleMapComponent , resolve: { data: SimpleDestMapResolver } },
      { path: 'sourcemap', component: TimeLineMapComponent, resolve: { data: SourceMapResolver } },
      { path: 'destmap', component: TimeLineMapComponent , resolve: { data: DestMapResolver } },
      { path: 'weeklysourcemap', component: TimeLineMapWeekNoComponent, resolve: { data: SourceMapWeeKnoResolver } },
      { path: 'weeklydestmap', component: TimeLineMapWeekNoComponent, resolve: { data: DestMapWeeKnoResolver } },
      { path: 'trace', component: TraceMapComponent, resolve: { data: TraceResolver }  },
      { path: 'calendar', component: CalendarComponent , resolve: { data: CalendarResolver } },
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TrafficRoutingModule { }

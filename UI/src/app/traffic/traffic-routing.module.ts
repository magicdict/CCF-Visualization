import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TrafficMainComponent } from './TrafficMain.component';
import { TimeAnalysisComponent } from './TimeAnalysis/TimeAnalysis.component';
import { TimeLineMapComponent } from './SourceDestMap/TimeLineMap.component';
import { SourceMapResolver, DestMapResolver, TimeAnaysisResolver, DashBoardResolver, CalendarResolver } from './resolver.service';
import { DashboardComponent } from './Dashboard/Dashboard.component';
import { CalendarComponent } from './Calendar/Calendar.component';


const routes: Routes = [
  {
    path: 'traffic', component: TrafficMainComponent,
    children: [
      { path: 'dashboard', component: DashboardComponent, resolve: { data: DashBoardResolver } },
      { path: 'timeanalysis', component: TimeAnalysisComponent, resolve: { data: TimeAnaysisResolver } },
      { path: 'sourcemap', component: TimeLineMapComponent, resolve: { data: SourceMapResolver } },
      { path: 'destmap', component: TimeLineMapComponent , resolve: { data: DestMapResolver } },
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

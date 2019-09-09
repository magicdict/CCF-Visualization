import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TrafficMainComponent } from './TrafficMain.component';
import { TimeAnalysisComponent } from './TimeAnalysis/TimeAnalysis.component';
import { TimeAnaysisResolver } from './resolver.service';

const routes: Routes = [
  {
    path: 'traffic', component: TrafficMainComponent,
    children: [
      { path: 'timeanalysis', component: TimeAnalysisComponent, resolve: { data: TimeAnaysisResolver } },
      { path: '', redirectTo: 'timeanalysis', pathMatch: 'full' }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TrafficRoutingModule { }

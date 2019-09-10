import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TrafficMainComponent } from './TrafficMain.component';
import { TimeAnalysisComponent } from './TimeAnalysis/TimeAnalysis.component';
import { DestMapComponent } from './SourceDestMap/DestMap.component';
import { SourceMapComponent } from './SourceDestMap/SourceMap.component';
import { SourceMapResolver, DestMapResolver, TimeAnaysisResolver } from './resolver.service';


const routes: Routes = [
  {
    path: 'traffic', component: TrafficMainComponent,
    children: [
      { path: 'timeanalysis', component: TimeAnalysisComponent, resolve: { data: TimeAnaysisResolver } },
      { path: 'sourcemap', component: SourceMapComponent, resolve: { data: SourceMapResolver } },
      { path: 'destmap', component: DestMapComponent , resolve: { data: DestMapResolver } },
      { path: '', redirectTo: 'timeanalysis', pathMatch: 'full' }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TrafficRoutingModule { }

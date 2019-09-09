import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SecurityMainComponent } from './SecurityMain.component';
import { TimeAnaysisResolver, DashboardResolver } from './resolver.service';
import { DashboardComponent } from './Dashboard/Dashboard.component';
import { TimeAnalysisComponent } from './TimeAnalysis/TimeAnalysis.component';


const routes: Routes = [
  {
    path: 'security', component: SecurityMainComponent, children: [
      { path: 'dashboard', component: DashboardComponent, resolve: { data: DashboardResolver } },
      { path: 'timeanalysis', component: TimeAnalysisComponent, resolve: { data: TimeAnaysisResolver } },
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SecurityRoutingModule { }

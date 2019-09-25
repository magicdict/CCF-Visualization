import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SecurityMainComponent } from './SecurityMain.component';
import { TimeAnaysisResolver, DashboardResolver, ServerInfoResolver } from './resolver.service';
import { DashboardComponent } from './Dashboard/Dashboard.component';
import { TimeAnalysisComponent } from './TimeAnalysis/TimeAnalysis.component';
import { ServerInfoComponent } from './ServerInfo/ServerInfocomponent';
import { TrafficSankeyComponent } from './TrafficSankey/TrafficSankey.component';


const routes: Routes = [
  {
    path: 'security', component: SecurityMainComponent, children: [
      { path: 'dashboard', component: DashboardComponent, resolve: { data: DashboardResolver } },
      { path: 'timeanalysis', component: TimeAnalysisComponent, resolve: { data: TimeAnaysisResolver } },
      { path: 'sankey', component: TrafficSankeyComponent, resolve: { data: DashboardResolver } },
      { path: 'serverinfo', component: ServerInfoComponent, resolve: { data: ServerInfoResolver } },
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SecurityRoutingModule { }

import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SecurityMainComponent } from './SecurityMain.component';
import { TimeAnaysisResolver, DashboardResolver, ServerInfoResolver, SourceIpSegResolver, DistIpSegResolver } from './resolver.service';
import { DashboardComponent } from './Dashboard/Dashboard.component';
import { TimeAnalysisComponent } from './TimeAnalysis/TimeAnalysis.component';
import { ServerInfoComponent } from './ServerInfo/ServerInfo.component';
import { TrafficSankeyComponent } from './TrafficSankey/TrafficSankey.component';
import { IpSegmentComponent } from './IpSegment/IpSegment.component';


const routes: Routes = [
  {
    path: 'security', component: SecurityMainComponent, children: [
      { path: 'dashboard', component: DashboardComponent, resolve: { data: DashboardResolver } },
      { path: 'serverinfo', component: ServerInfoComponent, resolve: { data: DashboardResolver } },
      { path: 'timeanalysis', component: TimeAnalysisComponent, resolve: { data: TimeAnaysisResolver } },
      { path: 'sankey', component: TrafficSankeyComponent, resolve: { data: DashboardResolver } },
      { path: 'sourceipseg', component: IpSegmentComponent, resolve: { data: SourceIpSegResolver } },
      { path: 'distipseg', component: IpSegmentComponent, resolve: { data: DistIpSegResolver } },
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SecurityRoutingModule { }

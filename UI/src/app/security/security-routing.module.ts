import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ConnectTimeComponent } from './ConnectTime/ConnectTime.component';
import { SecurityMainComponent } from './SecurityMain.component';
import { DashboardResolver } from './resolver.service';
import { DashboardComponent } from './Dashboard/Dashboard.component';
const routes: Routes = [
  {
    path: 'security', component: SecurityMainComponent, children: [
      { path: 'dashboard', component: DashboardComponent, resolve: { data: DashboardResolver } },
      { path: 'connectTime', component: ConnectTimeComponent, resolve: { data: DashboardResolver } },
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SecurityRoutingModule { }

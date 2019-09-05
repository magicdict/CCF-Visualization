import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TrafficMainComponent } from './TrafficMain.component';
import { DashboardResolver } from './resolver.service';
import { DashboardComponent } from './Dashboard/Dashboard.component';
const routes: Routes = [
  {
    path: 'traffic', component: TrafficMainComponent, children: [
      { path: 'dashboard', component: DashboardComponent, resolve: { data: DashboardResolver } },
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TrafficRoutingModule { }

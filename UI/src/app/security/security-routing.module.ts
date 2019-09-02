import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ConnectTimeComponent } from './ConnectTime/ConnectTime.component';
import { SecurityMainComponent } from './SecurityMain.component';
import { ConnectTimeResolver } from './resolver.service';
const routes: Routes = [
  {
    path: 'security', component: SecurityMainComponent, children: [
      { path: 'connectTime', component: ConnectTimeComponent, resolve: { data: ConnectTimeResolver } },
      { path: '', redirectTo: 'connectTime', pathMatch: 'full' }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SecurityRoutingModule { }

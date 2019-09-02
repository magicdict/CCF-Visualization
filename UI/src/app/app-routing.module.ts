import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SecurityMainComponent } from './security/SecurityMain.component';

const routes: Routes = [
  { path: 'security', component: SecurityMainComponent },
  { path: 'traffic', component: SecurityMainComponent },
  { path: '', redirectTo: 'security', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

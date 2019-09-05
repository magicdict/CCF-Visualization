import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component/app.component';
import { MenuComponent } from './app.component/menu.component';

const routes: Routes = [
  {
    path: '', component: AppComponent, children: [
      { path: "menu", component: MenuComponent },
      { path: "", redirectTo: "menu", pathMatch: "full" }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

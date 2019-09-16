import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component/app.component';

import { SecurityModule } from './security/security.module';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { CommonFunction } from './Common/common';
import { TrafficModule } from './traffic/traffic.module';
import { MenuComponent } from './app.component/menu.component';


@NgModule({
  declarations: [
    AppComponent,
    MenuComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CommonModule,
    HttpClientModule,
    SecurityModule,
    TrafficModule,
  ],
  providers: [CommonFunction],
  bootstrap: [AppComponent]
})
export class AppModule { }

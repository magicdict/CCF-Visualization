import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component/app.component';

import { HttpClientModule } from "@angular/common/http"
import { CommonFunction } from './common';
import { NgxEchartsModule } from 'ngx-echarts';
import { SecurityMainComponent } from './security/SecurityMain.component';

@NgModule({
  declarations: [
    AppComponent,
    SecurityMainComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NgxEchartsModule,
  ],
  providers: [CommonFunction],
  bootstrap: [AppComponent]
})
export class AppModule { }

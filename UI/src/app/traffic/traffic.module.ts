import { NgModule } from '@angular/core';
import { TrafficMainComponent } from './TrafficMain.component';
import { TrafficRoutingModule } from './traffic-routing.module';
import { TimeAnaysisResolver } from './resolver.service';
import { CommonFunction } from '../Common/common';
import { HttpClientModule } from '@angular/common/http';

import { MyCommonModule } from '../Common/MyCommon.module';
import { TimeAnalysisComponent } from './TimeAnalysis/TimeAnalysis.component';

@NgModule({
  declarations: [
    TrafficMainComponent,

    TimeAnalysisComponent
  ],
  imports: [
    TrafficRoutingModule,
    HttpClientModule,
    MyCommonModule
  ],
  providers: [
    CommonFunction,
    TimeAnaysisResolver
  ],
  bootstrap: [TrafficMainComponent]
})
export class TrafficModule { }

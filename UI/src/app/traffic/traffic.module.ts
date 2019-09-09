import { NgModule } from '@angular/core';
import { TrafficRoutingModule } from './traffic-routing.module';
import { TimeAnaysisResolver } from './resolver.service';
import { CommonFunction } from '../Common/common';
import { HttpClientModule } from '@angular/common/http';
import { MyCommonModule } from '../Common/MyCommon.module';

import { TrafficMainComponent } from './TrafficMain.component';
import { TimeAnalysisComponent } from './TimeAnalysis/TimeAnalysis.component';
import { SourceDestMapComponent } from './SourceDestMap/SourceDestMap.component';
import { NgxEchartsModule } from 'ngx-echarts';
@NgModule({
  declarations: [
    TrafficMainComponent,
    SourceDestMapComponent,
    TimeAnalysisComponent
  ],
  imports: [
    TrafficRoutingModule,
    HttpClientModule,
    MyCommonModule,
    NgxEchartsModule
  ],
  providers: [
    CommonFunction,
    TimeAnaysisResolver
  ],
  bootstrap: [TrafficMainComponent]
})
export class TrafficModule { }

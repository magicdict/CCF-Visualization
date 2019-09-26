import { NgModule } from '@angular/core';
import { SecurityMainComponent } from './SecurityMain.component';
import { SecurityRoutingModule } from './security-routing.module';
import { DashboardResolver, TimeAnaysisResolver, ServerInfoResolver, SourceIpSegResolver, DistIpSegResolver } from './resolver.service';
import { CommonFunction } from '../Common/common';
import { HttpClientModule } from '@angular/common/http';
import { DashboardComponent } from './Dashboard/Dashboard.component';
import { MyCommonModule } from '../Common/MyCommon.module';
import { TimeAnalysisComponent } from './TimeAnalysis/TimeAnalysis.component';
import { ServerInfoComponent } from './ServerInfo/ServerInfo.component';
import { TrafficSankeyComponent } from './TrafficSankey/TrafficSankey.component';
import { IpSegmentComponent } from './IpSegment/IpSegment.component';

@NgModule({
  declarations: [
    SecurityMainComponent,
    TimeAnalysisComponent,
    DashboardComponent,
    TrafficSankeyComponent,
    ServerInfoComponent,
    IpSegmentComponent
  ],
  imports: [
    SecurityRoutingModule,
    HttpClientModule,
    MyCommonModule
  ],
  providers: [
    CommonFunction,
    DashboardResolver,
    TimeAnaysisResolver,
    ServerInfoResolver,
    SourceIpSegResolver,
    DistIpSegResolver
  ],
  bootstrap: [SecurityMainComponent]
})
export class SecurityModule { }

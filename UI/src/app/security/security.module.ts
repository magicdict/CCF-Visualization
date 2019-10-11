import { NgModule } from '@angular/core';
import { SecurityMainComponent } from './SecurityMain.component';
import { SecurityRoutingModule } from './security-routing.module';
import { DashboardResolver, TimeAnaysisResolver, ServerInfoResolver, SourceIpSegResolver, DistIpSegResolver, ProfileResolver, GephiResolver, GephiOpenOrdResolver } from './resolver.service';
import { CommonFunction } from '../Common/common';
import { HttpClientModule } from '@angular/common/http';
import { DashboardComponent } from './Dashboard/Dashboard.component';
import { MyCommonModule } from '../Common/MyCommon.module';
import { TimeAnalysisComponent } from './TimeAnalysis/TimeAnalysis.component';
import { ServerInfoComponent } from './ServerInfo/ServerInfo.component';
import { TrafficSankeyComponent } from './TrafficSankey/TrafficSankey.component';
import { IpSegmentComponent } from './IpSegment/IpSegment.component';
import { ProtocolProfileComponent } from './Protocol/ProtocolProfile.component';
import { CommonModule } from '@angular/common';
import { TrafficGephiComponent } from './TrafficSankey/TrafficGephi.component';
import { TopologyComponent } from './TrafficSankey/Topology.component';

@NgModule({
  declarations: [
    SecurityMainComponent,
    TimeAnalysisComponent,
    DashboardComponent,
    TrafficSankeyComponent,
    TrafficGephiComponent,
    TopologyComponent,
    ServerInfoComponent,
    IpSegmentComponent,
    ProtocolProfileComponent
  ],
  imports: [
    SecurityRoutingModule, 
    HttpClientModule,
    MyCommonModule,
    CommonModule
  ],
  providers: [
    CommonFunction,
    DashboardResolver,
    TimeAnaysisResolver,
    ServerInfoResolver,
    SourceIpSegResolver,
    DistIpSegResolver,
    ProfileResolver,
    GephiResolver,
    GephiOpenOrdResolver
  ],
  bootstrap: [SecurityMainComponent]
})
export class SecurityModule { }

import { NgModule } from '@angular/core';
import { TrafficMainComponent } from './TrafficMain.component';
import { TrafficRoutingModule } from './traffic-routing.module';
import { DashboardResolver } from './resolver.service';
import { CommonFunction } from '../Common/common';
import { HttpClientModule } from '@angular/common/http';
import { DashboardComponent } from './Dashboard/Dashboard.component';
import { MyCommonModule } from '../Common/MyCommon.module';

@NgModule({
  declarations: [
    TrafficMainComponent,
    DashboardComponent
  ],
  imports: [
    TrafficRoutingModule,
    HttpClientModule,
    MyCommonModule
  ],
  providers: [
    CommonFunction,
    DashboardResolver
  ],
  bootstrap: [TrafficMainComponent]
})
export class TrafficModule { }

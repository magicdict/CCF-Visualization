import { NgModule } from '@angular/core';
import { SecurityMainComponent } from './SecurityMain.component';
import { ConnectTimeComponent } from './ConnectTime/ConnectTime.component';
import { SecurityRoutingModule } from './security-routing.module';
import { DashboardResolver } from './resolver.service';
import { CommonFunction } from '../Common/common';
import { HttpClientModule } from '@angular/common/http';
import { DashboardComponent } from './Dashboard/Dashboard.component';
import { MyCommonModule } from '../Common/MyCommon.module';

@NgModule({
  declarations: [
    SecurityMainComponent,
    ConnectTimeComponent,
    DashboardComponent
  ],
  imports: [
    SecurityRoutingModule,
    HttpClientModule,
    MyCommonModule
  ],
  providers: [
    CommonFunction,
    DashboardResolver
  ],
  bootstrap: [SecurityMainComponent]
})
export class SecurityModule { }

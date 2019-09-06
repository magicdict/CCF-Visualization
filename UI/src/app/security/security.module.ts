import { NgModule } from '@angular/core';
import { SecurityMainComponent } from './SecurityMain.component';
import { SecurityRoutingModule } from './security-routing.module';
import { DashboardResolver, TimeAnaysisResolver } from './resolver.service';
import { CommonFunction } from '../Common/common';
import { HttpClientModule } from '@angular/common/http';
import { DashboardComponent } from './Dashboard/Dashboard.component';
import { MyCommonModule } from '../Common/MyCommon.module';
import { TimeAnalysisComponent } from './TimeAnalysis/TimeAnalysis.component';

@NgModule({
  declarations: [
    SecurityMainComponent,
    TimeAnalysisComponent,
    DashboardComponent
  ],
  imports: [
    SecurityRoutingModule,
    HttpClientModule,
    MyCommonModule
  ],
  providers: [
    CommonFunction,
    DashboardResolver,
    TimeAnaysisResolver
  ],
  bootstrap: [SecurityMainComponent]
})
export class SecurityModule { }

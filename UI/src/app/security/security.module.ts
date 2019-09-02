import { NgModule } from '@angular/core';
import { SecurityMainComponent } from './SecurityMain.component';
import { ConnectTimeComponent } from './ConnectTime/ConnectTime.component';
import { SecurityRoutingModule } from './security-routing.module';
import { ConnectTimeResolver } from './resolver.service';
import { CommonFunction } from '../Common/common';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '../Common/common.module';

@NgModule({
  declarations: [
    SecurityMainComponent,
    ConnectTimeComponent
  ],
  imports: [
    SecurityRoutingModule,
    HttpClientModule,
    CommonModule
  ],
  providers: [
    CommonFunction,
    ConnectTimeResolver
  ],
  bootstrap: [SecurityMainComponent]
})
export class SecurityModule { }

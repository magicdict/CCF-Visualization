import { NgModule } from '@angular/core';
import { HttpClientModule } from "@angular/common/http"
import { NgxEchartsModule } from 'ngx-echarts';
import { ChartComponent } from './chart.component';
import { CommonFunction } from './common';

@NgModule({
  declarations: [
    ChartComponent
  ],
  imports: [
    NgxEchartsModule,
    HttpClientModule,
  ],
  providers: [CommonFunction],
  bootstrap: []
})
export class CommonModule { }

import { Component, Input, Output } from '@angular/core';
import { EventEmitter } from 'events';

@Component({
    selector: 'chart',
    templateUrl: './chart.component.html',
})
export class ChartComponent {
    @Input() title = '图表名称';
    @Input() footer = "";
    @Input() options = null;
    @Output() ChartInit = new EventEmitter();
    public ChartInited(e:any){
        this.ChartInit.emit(e); 
    }
} 

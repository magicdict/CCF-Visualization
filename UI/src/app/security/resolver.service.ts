import { Injectable } from '@angular/core';
import { Resolve, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { CommonFunction } from '../Common/common';

@Injectable()
export class ConnectTimeResolver implements Resolve<any> {
    constructor(public commonFunction: CommonFunction) {

    }
    resolve(_: ActivatedRouteSnapshot, _state: RouterStateSnapshot): any | Observable<any> | Promise<any> {
        return this.commonFunction.httpRequestGet<any>("GetProtocols");
    }
}
import { Injectable } from '@angular/core';
import { Resolve, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { CommonFunction } from '../Common/common';
import { IDashBoard, ITimeAnaysis, IServerInfo } from './Model';

@Injectable()
export class DashboardResolver implements Resolve<IDashBoard> {
    constructor(public commonFunction: CommonFunction) {

    }
    resolve(_: ActivatedRouteSnapshot, _state: RouterStateSnapshot): IDashBoard | Observable<IDashBoard> | Promise<IDashBoard> {
        return this.commonFunction.httpRequestGet<IDashBoard>("Security/GetDashBoard");
    }
}

@Injectable()
export class TimeAnaysisResolver implements Resolve<ITimeAnaysis> {
    constructor(public commonFunction: CommonFunction) {

    }
    resolve(_: ActivatedRouteSnapshot, _state: RouterStateSnapshot): ITimeAnaysis | Observable<ITimeAnaysis> | Promise<ITimeAnaysis> {
        return this.commonFunction.httpRequestGet<ITimeAnaysis>("Security/GetTimeAnalysis");
    }
}

@Injectable()
export class ServerInfoResolver implements Resolve<IServerInfo[]> {
    constructor(public commonFunction: CommonFunction) {

    }
    resolve(_: ActivatedRouteSnapshot, _state: RouterStateSnapshot): IServerInfo[] | Observable<IServerInfo[]> | Promise<IServerInfo[]> {
        return this.commonFunction.httpRequestGetFromAsset<IServerInfo[]>("security/json/server_info.json");
    }
}
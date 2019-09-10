import { Injectable } from '@angular/core';
import { Resolve, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { CommonFunction } from '../Common/common';
import { ITimeAnaysis, MapValue, IDashBoard } from './Model';


@Injectable()
export class DashBoardResolver implements Resolve<IDashBoard> {
    constructor(public commonFunction: CommonFunction) {

    }
    resolve(_: ActivatedRouteSnapshot, _state: RouterStateSnapshot): IDashBoard | Observable<IDashBoard> | Promise<IDashBoard> {
        return this.commonFunction.httpRequestGet<IDashBoard>("Traffic/GetDashboard");
    }
}

@Injectable()
export class TimeAnaysisResolver implements Resolve<ITimeAnaysis> {
    constructor(public commonFunction: CommonFunction) {

    }
    resolve(_: ActivatedRouteSnapshot, _state: RouterStateSnapshot): ITimeAnaysis | Observable<ITimeAnaysis> | Promise<ITimeAnaysis> {
        return this.commonFunction.httpRequestGet<ITimeAnaysis>("Traffic/GetTimeAnalysis");
    }
}

@Injectable()
export class SourceMapResolver implements Resolve<MapValue[]> {
    constructor(public commonFunction: CommonFunction) {

    }
    resolve(_: ActivatedRouteSnapshot, _state: RouterStateSnapshot): MapValue[] | Observable<MapValue[]> | Promise<MapValue[]> {
        return this.commonFunction.httpRequestGetFromAsset<MapValue[]>("json/startlocs_PointSize.json");
    }
}

@Injectable()
export class DestMapResolver implements Resolve<MapValue[]> {
    constructor(public commonFunction: CommonFunction) {

    }
    resolve(_: ActivatedRouteSnapshot, _state: RouterStateSnapshot): MapValue[] | Observable<MapValue[]> | Promise<MapValue[]> {
        return this.commonFunction.httpRequestGetFromAsset<MapValue[]>("json/destlocs_PointSize.json");
    }
}
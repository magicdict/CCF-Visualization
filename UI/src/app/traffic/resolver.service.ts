import { Injectable } from '@angular/core';
import { Resolve, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { CommonFunction } from '../Common/common';
import { ITimeAnaysis, MapValue, IDashBoard, IDiaryinfo, IPointAttr, IRelationship } from './Model';


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
export class SimpleSourceMapResolver implements Resolve<MapValue[]> {
    constructor(public commonFunction: CommonFunction) {

    }
    resolve(_: ActivatedRouteSnapshot, _state: RouterStateSnapshot): MapValue[] | Observable<MapValue[]> | Promise<MapValue[]> {
        return this.commonFunction.httpRequestGetFromAsset<MapValue[]>("traffic/json/startlocs_PointSize.json");
    }
}

@Injectable()
export class HotPointMapResolver implements Resolve<IPointAttr[]> {
    constructor(public commonFunction: CommonFunction) {

    }
    resolve(_: ActivatedRouteSnapshot, _state: RouterStateSnapshot): IPointAttr[] | Observable<IPointAttr[]> | Promise<IPointAttr[]> {
        return this.commonFunction.httpRequestGetFromAsset<IPointAttr[]>("traffic/json/PointAttr.json");
    }
}

@Injectable()
export class SimpleDestMapResolver implements Resolve<MapValue[]> {
    constructor(public commonFunction: CommonFunction) {

    }
    resolve(_: ActivatedRouteSnapshot, _state: RouterStateSnapshot): MapValue[] | Observable<MapValue[]> | Promise<MapValue[]> {
        return this.commonFunction.httpRequestGetFromAsset<MapValue[]>("traffic/json/destlocs_PointSize.json");
    }
}

@Injectable()
export class SourceMapResolver implements Resolve<MapValue[]> {
    constructor(public commonFunction: CommonFunction) {

    }
    resolve(_: ActivatedRouteSnapshot, _state: RouterStateSnapshot): MapValue[] | Observable<MapValue[]> | Promise<MapValue[]> {
        return this.commonFunction.httpRequestGetFromAsset<MapValue[]>("traffic/json/startlocs_24h_PointSize.json");
    }
}

@Injectable()
export class DestMapResolver implements Resolve<MapValue[]> {
    constructor(public commonFunction: CommonFunction) {

    }
    resolve(_: ActivatedRouteSnapshot, _state: RouterStateSnapshot): MapValue[] | Observable<MapValue[]> | Promise<MapValue[]> {
        return this.commonFunction.httpRequestGetFromAsset<MapValue[]>("traffic/json/destlocs_24h_PointSize.json");
    }
}

@Injectable()
export class SourceMapWeeKnoResolver implements Resolve<MapValue[]> {
    constructor(public commonFunction: CommonFunction) {

    }
    resolve(_: ActivatedRouteSnapshot, _state: RouterStateSnapshot): MapValue[] | Observable<MapValue[]> | Promise<MapValue[]> {
        return this.commonFunction.httpRequestGetFromAsset<MapValue[]>("traffic/json/startlocs_weekno_PointSize.json");
    }
}

@Injectable()
export class DestMapWeeKnoResolver implements Resolve<MapValue[]> {
    constructor(public commonFunction: CommonFunction) {

    }
    resolve(_: ActivatedRouteSnapshot, _state: RouterStateSnapshot): MapValue[] | Observable<MapValue[]> | Promise<MapValue[]> {
        return this.commonFunction.httpRequestGetFromAsset<MapValue[]>("traffic/json/destlocs_weekno_PointSize.json");
    }
}


@Injectable()
export class TraceResolver implements Resolve<any> {
    constructor(public commonFunction: CommonFunction) {

    }
    resolve(_: ActivatedRouteSnapshot, _state: RouterStateSnapshot): any | Observable<any> | Promise<any> {
        return this.commonFunction.httpRequestGetFromAsset<any>("traffic/json/trace.json");
    }
}

@Injectable()
export class CalendarResolver implements Resolve<IDiaryinfo[]> {
    constructor(public commonFunction: CommonFunction) {

    }
    resolve(_: ActivatedRouteSnapshot, _state: RouterStateSnapshot): IDiaryinfo[] | Observable<IDiaryinfo[]> | Promise<IDiaryinfo[]> {
        return this.commonFunction.httpRequestGet<IDiaryinfo[]>("Traffic/GetDiaryinfos");
    }
}

export class HeatMapResolver implements Resolve<IRelationship[]> {
    constructor(public commonFunction: CommonFunction) {

    }
    resolve(_: ActivatedRouteSnapshot, _state: RouterStateSnapshot): IRelationship[] | Observable<IRelationship[]> | Promise<IRelationship[]> {
        if (_state.url === "/traffic/startpagerank") {
            return this.commonFunction.httpRequestGetFromAsset<IRelationship[]>("traffic/json/start_betweenness.json");
        } else {
            return this.commonFunction.httpRequestGetFromAsset<IRelationship[]>("traffic/json/dest_betweenness.json");
        }
    }
}

export class AirportMapResolver implements Resolve<MapValue[]> {
    constructor(public commonFunction: CommonFunction) {

    }
    resolve(_: ActivatedRouteSnapshot, _state: RouterStateSnapshot): MapValue[] | Observable<MapValue[]> | Promise<MapValue[]> {
        switch (_state.url) {
            case "/traffic/destpointfromairport":
                return this.commonFunction.httpRequestGetFromAsset<MapValue[]>("traffic/json/DestPoint4AirportStart_PointSize.json");
            case "/traffic/startpointtoairport":
                return this.commonFunction.httpRequestGetFromAsset<MapValue[]>("traffic/json/StartPoint4AirportDest_PointSize.json");
            case "/traffic/destpointfromtrain":
                return this.commonFunction.httpRequestGetFromAsset<MapValue[]>("traffic/json/DestPoint4TrainStart_PointSize.json");
            case "/traffic/startpointtotrain":
                return this.commonFunction.httpRequestGetFromAsset<MapValue[]>("traffic/json/StartPoint4TrainDest_PointSize.json");
            default:
                break;
        }
    }
}

import { Injectable } from '@angular/core';
import { Resolve, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { CommonFunction } from '../Common/common';
import { IDashBoard, ITimeAnaysis, IServerInfo, IProfile } from './Model';

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



@Injectable()
export class SourceIpSegResolver implements Resolve<any> {
    constructor(public commonFunction: CommonFunction) {

    }
    resolve(_: ActivatedRouteSnapshot, _state: RouterStateSnapshot): any | Observable<any> | Promise<any> {
        return this.commonFunction.httpRequestGetFromAsset<any>("security/json/sourceip_tree.json");
    }
}

@Injectable()
export class DistIpSegResolver implements Resolve<any> {
    constructor(public commonFunction: CommonFunction) {

    }
    resolve(_: ActivatedRouteSnapshot, _state: RouterStateSnapshot): any | Observable<any> | Promise<any> {
        return this.commonFunction.httpRequestGetFromAsset<any>("security/json/distip_tree.json");
    }
}


@Injectable()
export class ProfileResolver implements Resolve<IProfile> {
    constructor(public commonFunction: CommonFunction) {

    }
    resolve(route: ActivatedRouteSnapshot, _state: RouterStateSnapshot): IProfile | Observable<IProfile> | Promise<IProfile> {
        let protocol = route.paramMap.get('protocol');
        return this.commonFunction.httpRequestGetFromAsset<IProfile>("security/json/" + protocol + ".json");
    }
}


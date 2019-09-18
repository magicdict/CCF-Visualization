export interface NameValueSet {
    Name: string,
    Value: number
}

export interface IDashBoard {
    TotalOrderCnt: number;
    TotalFee: number;
    AvgFeePerOrder: number;
    TotalDistanceKm: number;
    AvgDistanceKmPerOrder: number;
    FeePerKm: number;
    TotalDayCnt: number;
    AvgOrderCntEveryDay: number;
    AvgFeeEveryDay: number;
    AvgDistanceKmEveryDay: number;
    weeklyinfos: {
        Name: string, Value: { ordercnt: number, fee: number, distance: number }
    }[],
    TravellerCnt:NameValueSet[],
    product_ids: NameValueSet[],
    order_type: NameValueSet[],
    traffic_types: NameValueSet[],
    countys:NameValueSet[],
    product_1levels: NameValueSet[],
    Time: NameValueSet[],
    Distance: NameValueSet[],
    starting_poi:NameValueSet[],
    starting_pois:NameValueSet[],
    dest_pois:NameValueSet[],
}

export interface IDiaryinfo {
    Name: string,
    Value: {
        weather: IWeather;
        ordercnt: number;
        distance: number;
        fee: number;
        holiday: number;
        isWorkday: boolean;
        Weekno: string;
    }
}

export interface IWeather {
    Description: string,
    Tempera: string,
    Wind: string
}

export interface ITimeAnaysis {
    weekday_hour_orderCnt: NameValueSet[],
}

export interface MapValue {
    hour:number,
    name: string,
    value: number[],
    weekno:number
}
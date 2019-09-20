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
        Name: string, Value: {
            ordercnt: number,
            fee: number,
            normaltime:number,
            distance: number,
            premier: number,
            reserve: number,
            pickup: number,
            airport: number,
            train: number,
            longbus: number,
            waittime_1: number,
            waittime_2: number,
            waittime_3: number,
            waittime_4: number,
            distance_1: number,
            distance_2: number,
            distance_3: number,
            distance_4: number,
        }
    }[],
    TravellerCnt: NameValueSet[],
    product_ids: NameValueSet[],
    order_type: NameValueSet[],
    traffic_types: NameValueSet[],
    countys: NameValueSet[],
    product_1levels: NameValueSet[],
    NormalTime: NameValueSet[],
    Distance: NameValueSet[],
    starting_poi: NameValueSet[],
    starting_pois: NameValueSet[],
    dest_pois: NameValueSet[],
    WaitTime: NameValueSet[]
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
    hour: number,
    name: string,
    value: number[],
    weekno: number
}
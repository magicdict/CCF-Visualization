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
    product_ids:NameValueSet[],
    order_type:NameValueSet[],
    traffic_types:NameValueSet[],
    product_1levels:NameValueSet[]
}

export interface ITimeAnaysis {
    weekday_hour_orderCnt: NameValueSet[],
}

export interface MapValue {
    name: string,
    value: number[]
}
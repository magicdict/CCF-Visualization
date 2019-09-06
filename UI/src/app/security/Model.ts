export interface IDashBoard {
    RecordCnt: number,
    ProtocolCnt: number,

    SourceDistIpCnt: number,
    SourceIpCnt: number,
    DistIpCnt: number,

    downlink_length: number,
    uplink_length: number,

    SourceSegmentCnt: number,
    SourceTotalLanCnt: number,
    DestSegmentCnt: number,
    DestTotalLanCnt: number,

    Protocols: NameValueSet[],

    traffic_hours_today: NameValueSet[],
    traffic_hours_last1days: NameValueSet[],
    traffic_hours_last3days: NameValueSet[],
}

export interface ITimeAnaysis{
    hours_rec_cnt: NameValueSet[],
    Protocols_Hours: { [key: string]: NameValueSet[] },
    Protocols_Hours_Traffic: { [key: string]: NameValueSet[] },
    traffic_hours_everyday:NameValueSet[],
}

export interface NameValueSet {
    Name: string,
    Value: number
}
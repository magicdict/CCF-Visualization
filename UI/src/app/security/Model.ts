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

    protocols: NameValueSet[],
    ports: NameValueSet[],

    traffic_hours_today: NameValueSet[],
    traffic_hours_last1days: NameValueSet[],
    traffic_hours_last3days: NameValueSet[],

    access_hours_today: NameValueSet[],
    access_hours_last1days: NameValueSet[],
    access_hours_last3days: NameValueSet[],

    source: NameValueSet[],
    dist: NameValueSet[],
    source_dist: NameValueSet[],
}

export interface ITimeAnaysis {
    Protocols_Hours: { [key: string]: NameValueSet[] },
    Protocols_Hours_Traffic: { [key: string]: NameValueSet[] },
    traffic_hours_everyday: NameValueSet[],
}

export interface NameValueSet {
    Name: string,
    Value: number
}

export interface IServerInfo {
    server_ip: string,
    access_count: number,
    client_count: number,
    downlink_length: number,
    uplink_length: number,
    start_time: Date,
    end_time: Date,
    active_time: number
}

export interface IProfile {
    Name: string,
    Ports: NameValueSet[],
    DistIps: NameValueSet[],
    SourceIps: NameValueSet[],
    Source_dist: NameValueSet[],
    HostCnt: number,
    Top100HostInfo: IHostInfo[]
}

export interface IHostInfo {
    Ip:string,
    SourceCnt:number,
    DistCnt:number,
    DistProtocolCnt:number,
    DistRat:number,
    SourceProtocols:NameValueSet[],
    DistProtocols:NameValueSet[],
    ProtocolRate:number,
    option:any
}
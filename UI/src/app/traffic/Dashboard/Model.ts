export interface IDashBoard {
    RecordCnt:number,
    ProtocolCnt:number,
    
    SourceDistIpCnt:number,
    SourceIpCnt:number,
    DistIpCnt:number,

    downlink_length:number,
    uplink_length:number,

    SourceSegmentCnt:number,
    SourceTotalLanCnt:number,
    DestSegmentCnt:number,
    DestTotalLanCnt:number,

    Protocols: NameValueSet[],
    Hours: NameValueSet[],
    Protocols_Hours: { [key: string]: NameValueSet[] }
}

export interface NameValueSet {
    Name: string,
    Value: number
}
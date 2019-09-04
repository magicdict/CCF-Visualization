export interface IDashBoard {
    Protocols: NameValueSet[],
    Hours: NameValueSet[],
    Protocols_Hours: { [key: string]: NameValueSet[] }
}

export interface NameValueSet {
    Name: string,
    Value: number
}
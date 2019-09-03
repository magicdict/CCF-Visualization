export interface IDashBoard {
    Protocols:NameValueSet[],
    Hours:NameValueSet[]
}

export interface NameValueSet {
    Name: string,
    Value: number
}
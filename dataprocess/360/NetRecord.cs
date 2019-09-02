
using System;
using System.Globalization;

public class NetRecord
{
    public DateTime record_time { get; set; }

    public string source_ip { get; set; }
    public string destination_ip { get; set; }
    public string protocol { get; set; }
    public string destination_port { get; set; }
    public Int64 uplink_length { get; set; }
    public Int64 downlink_length { get; set; }

    public NetRecord(string Rawdata)
    {
        //{"record_time": "2019-04-13 17:41:34.246", "source_ip": "10.59.223.71", "destination_ip": "10.59.223.31", "protocol": "http", "destination_port": "80", "uplink_length": 278, "downlink_length": 353}	
        Rawdata = Rawdata.TrimStart('{').TrimEnd('\t').TrimEnd('}');
        var Info = Rawdata.Split(",");
        record_time = DateTime.ParseExact(Info[0].Split(": ")[1].Replace("\"", string.Empty).Trim(),"yyyy-MM-dd HH:mm:ss.fff",CultureInfo.CurrentCulture, DateTimeStyles.None);
        source_ip = Info[1].Split(": ")[1].Replace("\"", string.Empty).Trim();
        destination_ip = Info[2].Split(": ")[1].Replace("\"", string.Empty).Trim();
        protocol = Info[3].Split(": ")[1].Replace("\"", string.Empty).Trim();
        destination_port = Info[4].Split(": ")[1].Replace("\"", string.Empty).Trim();
        uplink_length = Int64.Parse(Info[5].Split(": ")[1].Replace("\"", string.Empty));
        downlink_length = Int64.Parse(Info[6].Split(": ")[1].Replace("\"", string.Empty));
    }


}
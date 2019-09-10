
using System;
using System.Globalization;

public class NetRecord
{
    public DateTime record_time { get; set; }
    public IPAddress source_ip { get; set; }
    public IPAddress destination_ip { get; set; }
    public string protocol { get; set; }
    public string destination_port { get; set; }
    public Int64 uplink_length { get; set; }
    public Int64 downlink_length { get; set; }
    public NetRecord(string Rawdata)
    {
        //{"record_time": "2019-04-13 17:41:34.246", "source_ip": "10.59.223.71", "destination_ip": "10.59.223.31", "protocol": "http", "destination_port": "80", "uplink_length": 278, "downlink_length": 353}	
        Rawdata = Rawdata.TrimStart('{').TrimEnd('\t').TrimEnd('}');
        var Info = Rawdata.Split(",");
        record_time = DateTime.ParseExact(Info[0].Split(": ")[1].Replace("\"", string.Empty).Trim(), "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.CurrentCulture, DateTimeStyles.None);
        source_ip = new IPAddress(Info[1].Split(": ")[1].Replace("\"", string.Empty).Trim());
        destination_ip = new IPAddress(Info[2].Split(": ")[1].Replace("\"", string.Empty).Trim());
        protocol = Info[3].Split(": ")[1].Replace("\"", string.Empty).Trim();
        destination_port = Info[4].Split(": ")[1].Replace("\"", string.Empty).Trim();
        uplink_length = Int64.Parse(Info[5].Split(": ")[1].Replace("\"", string.Empty));
        downlink_length = Int64.Parse(Info[6].Split(": ")[1].Replace("\"", string.Empty));
    }
}

public class IPAddress
{
    public string RawIp;
    public IPAddress(string Ip)
    {
        RawIp = Ip;
    }
    public bool IsIpV4
    {
        get
        {
            return RawIp.Split(".").Length == 4;
        }
    }
    public bool IsIpV6
    {
        get
        {
            return RawIp.Split(":").Length == 8;
        }
    }

    public bool IsLAN
    {
        get
        {
            return IsKindAIp || IsKindBIp || IsKindCIp;
        }
    }

    public string Segment
    {
        get
        {
            return RawIp.Split(".")[0] + "." + RawIp.Split(".")[1] + "." + RawIp.Split(".")[2];
        }
    }

    /// <summary>
    /// A类地址：10.0.0.0--10.255.255.255
    /// </summary>
    /// <value></value>
    public bool IsKindAIp
    {
        get
        {
            if (IsIpV6) return false;
            return RawIp.Split(".")[0] == "10";
        }
    }

    /// <summary>
    /// B类地址：172.16.0.0--172.31.255.255 
    /// </summary>
    /// <value></value>
    public bool IsKindBIp
    {
        get
        {
            if (IsIpV6) return false;
            if (RawIp.Split(".")[0] != "172") return false;
            if (int.Parse(RawIp.Split(".")[1]) >= 16 && int.Parse(RawIp.Split(".")[1]) <= 31) return true;
            return false;
        }
    }

    /// <summary>
    /// C类地址：192.168.0.0--192.168.255.255
    /// </summary>
    /// <value></value>
    public bool IsKindCIp
    {
        get
        {
            if (IsIpV6) return false;
            return RawIp.StartsWith("192.168.");
        }
    }

    public bool IsDHCPBlockIp
    {
        get
        {
            if (IsIpV6) return false;
            return RawIp.StartsWith("169.254.");
        }
    }

}

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
        //通过端口对协议进行调整
        if (protocol == "unknown")
        {
            switch (destination_port)
            {
                case "1": protocol = "tcpmux"; break;
                case "5": protocol = "rje"; break;
                case "7": protocol = "echo"; break;
                case "9": protocol = "discard"; break;
                case "11": protocol = "systat"; break;
                case "13": protocol = "daytime"; break;
                case "17": protocol = "qotd"; break;
                case "18": protocol = "msp"; break;
                case "19": protocol = "chargen"; break;
                case "20": protocol = "ftp-data"; break;
                case "21": protocol = "ftp_control"; break; //和题目保持一致
                case "22": protocol = "ssh"; break;
                case "23": protocol = "telnet"; break;
                case "25": protocol = "smtp"; break;
                case "37": protocol = "time"; break;
                case "39": protocol = "rlp"; break;
                case "42": protocol = "nameserver"; break;
                case "43": protocol = "nicname"; break;
                case "49": protocol = "tacacs"; break;
                case "50": protocol = "re-mail-ck"; break;
                case "53": protocol = "domain"; break;
                case "63": protocol = "whois++"; break;
                case "67": protocol = "bootps"; break;
                case "68": protocol = "bootpc"; break;
                case "69": protocol = "tftp"; break;
                case "70": protocol = "gopher"; break;
                case "71": protocol = "netrjs-1"; break;
                case "72": protocol = "netrjs-2"; break;
                case "73": protocol = "netrjs-3"; break;
                case "74": protocol = "netrjs-4"; break;
                case "79": protocol = "finger"; break;
                case "80": protocol = "http"; break;
                case "88": protocol = "kerberos"; break;
                case "95": protocol = "supdup"; break;
                case "101": protocol = "hostname"; break;
                case "102": protocol = "iso-tsap"; break;
                case "105": protocol = "csnet-ns"; break;
                case "107": protocol = "rtelnet"; break;
                case "109": protocol = "pop2"; break;
                case "110": protocol = "pop3"; break;
                case "111": protocol = "sunrpc"; break;
                case "113": protocol = "auth"; break;
                case "115": protocol = "sftp"; break;
                case "117": protocol = "uucp-path"; break;
                case "119": protocol = "nntp"; break;
                case "123": protocol = "ntp"; break;
                case "137": protocol = "netbios-ns"; break;
                case "138": protocol = "netbios-dgm"; break;
                case "139": protocol = "netbios-ssn"; break;
                case "143": protocol = "imap"; break;
                case "161": protocol = "snmp"; break;
                case "162": protocol = "snmptrap"; break;
                case "163": protocol = "cmip-man"; break;
                case "164": protocol = "cmip-agent"; break;
                case "174": protocol = "mailq"; break;
                case "177": protocol = "xdmcp"; break;
                case "178": protocol = "nextstep"; break;
                case "179": protocol = "bgp"; break;
                case "191": protocol = "prospero"; break;
                case "194": protocol = "irc"; break;
                case "199": protocol = "smux"; break;
                case "201": protocol = "at-rtmp"; break;
                case "202": protocol = "at-nbp"; break;
                case "204": protocol = "at-echo"; break;
                case "206": protocol = "at-zis"; break;
                case "209": protocol = "qmtp"; break;
                case "210": protocol = "z39.50"; break;
                case "213": protocol = "ipx"; break;
                case "220": protocol = "imap3"; break;
                case "245": protocol = "link"; break;
                case "347": protocol = "fatserv"; break;
                case "363": protocol = "rsvp_tunnel"; break;
                case "369": protocol = "rpc2portmap"; break;
                case "370": protocol = "codaauth2"; break;
                case "372": protocol = "ulistproc"; break;
                case "389": protocol = "ldap"; break;
                case "427": protocol = "svrloc"; break;
                case "434": protocol = "mobileip-agent"; break;
                case "435": protocol = "mobilip-mn"; break;
                case "443": protocol = "https"; break;
                case "444": protocol = "snpp"; break;
                case "445": protocol = "microsoft-ds"; break;
                case "464": protocol = "kpasswd"; break;
                case "468": protocol = "photuris"; break;
                case "487": protocol = "saft"; break;
                case "488": protocol = "gss-http"; break;
                case "496": protocol = "pim-rp-disc"; break;
                case "500": protocol = "isakmp"; break;
                case "535": protocol = "iiop"; break;
                case "538": protocol = "gdomap"; break;
                case "546": protocol = "dhcpv6-client"; break;
                case "547": protocol = "dhcpv6-server"; break;
                case "554": protocol = "rtsp"; break;
                case "563": protocol = "nntps"; break;
                case "565": protocol = "whoami"; break;
                case "587": protocol = "submission"; break;
                case "610": protocol = "npmp-local"; break;
                case "611": protocol = "npmp-gui"; break;
                case "612": protocol = "hmmp-ind"; break;
                case "631": protocol = "ipp"; break;
                case "636": protocol = "ldaps"; break;
                case "674": protocol = "acap"; break;
                case "694": protocol = "ha-cluster"; break;
                case "749": protocol = "kerberos-adm"; break;
                case "750": protocol = "kerberos-iv"; break;
                case "765": protocol = "webster"; break;
                case "767": protocol = "phonebook"; break;
                case "873": protocol = "rsync"; break;
                case "992": protocol = "telnets"; break;
                case "993": protocol = "imaps"; break;
                case "994": protocol = "ircs"; break;
                case "995": protocol = "pop3s"; break;
                case "1883": protocol = "mqtt"; break;
                case "2181": protocol = "zookeeper"; break;
                case "5223": protocol = "apns"; break;
                case "8080": protocol = "http_proxy"; break;
                case "8082": protocol = "http"; break;
                case "8360": protocol = "http_proxy"; break;
                default:
                    break;
            }
        }
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


    public string SegmentRoot
    {
        get
        {
            return RawIp.Split(".")[0];
        }
    }

    public string SegmentParent
    {
        get
        {
            return RawIp.Split(".")[0] + "." + RawIp.Split(".")[1];
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
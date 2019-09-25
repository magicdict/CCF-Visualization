using System.Collections.Generic;
using System.IO;

public static class SecurityDataSet
{
    /// <summary>
    /// 不同协议通信次数
    /// </summary>
    /// <returns></returns>
    public static List<NameValueSet<int>> Protocols = new List<NameValueSet<int>>();
    /// <summary>
    /// 单纯通讯次数的按时统计
    /// </summary>
    /// <returns></returns>
    public static List<NameValueSet<int>> hours_rec_cnt = new List<NameValueSet<int>>();
    /// <summary>
    /// 15分钟单位协议别次数统计
    /// </summary>
    /// <returns></returns>
    public static List<NameValueSet<int>> Protocols_Hours = new List<NameValueSet<int>>();

    public static List<NameValueSet<int>> Protocols_Hours_Traffic = new List<NameValueSet<int>>();

    public static List<NameValueSet<long>> BasicInfo = new List<NameValueSet<long>>();
    public static List<NameValueSet<int>> traffic_hours_today = new List<NameValueSet<int>>();
    public static List<NameValueSet<int>> traffic_hours_last1days = new List<NameValueSet<int>>();
    public static List<NameValueSet<int>> traffic_hours_last3days = new List<NameValueSet<int>>();

    public static List<NameValueSet<int>> traffic_hours_everyday = new List<NameValueSet<int>>();

    public static void LoadData()
    {
        //协议统计的加载
        var Folder = @"F:\CCF-Visualization\dataprocess\AfterProcess\企业网络资产及安全事件分析与可视化\";
        //var Folder = @"/root/HelloChinaApi/AfterProcess/企业网络资产及安全事件分析与可视化/";


        var sr = new StreamReader(Folder + "protocols_hours_traffic.csv");
        while (!sr.EndOfStream)
        {
            var info = sr.ReadLine().Split(",");
            Protocols_Hours_Traffic.Add(new NameValueSet<int>() { Name = info[0] + "|" + info[1], Value = (int)double.Parse(info[2]) });
        }
        sr.Close();

        sr = new StreamReader(Folder + "protocols_hours.csv");
        while (!sr.EndOfStream)
        {
            var info = sr.ReadLine().Split(",");
            Protocols_Hours.Add(new NameValueSet<int>() { Name = info[0] + "|" + info[1], Value = int.Parse(info[2]) });
        }
        sr.Close();

        sr = new StreamReader(Folder + "traffic_hours_everyday.csv");
        while (!sr.EndOfStream)
        {
            var info = sr.ReadLine().Split(",");
            traffic_hours_everyday.Add(new NameValueSet<int>() { Name = info[0], Value = (int)double.Parse(info[1]) });
        }
        sr.Close();

        sr = new StreamReader(Folder + "basic_info.csv");
        while (!sr.EndOfStream)
        {
            var info = sr.ReadLine().Split(",");
            switch (info[0])
            {
                case nameof(SecurityDashBoard.downlink_length):
                    BasicInfo.Add(new NameValueSet<long>() { Name = info[0], Value = (long)double.Parse(info[1]) });
                    break;
                case nameof(SecurityDashBoard.uplink_length):
                    BasicInfo.Add(new NameValueSet<long>() { Name = info[0], Value = (long)double.Parse(info[1]) });
                    break;
                case nameof(Protocols):
                    for (int i = 1; i < info.Length - 1; i += 2)
                    {
                        Protocols.Add(new NameValueSet<int>() { Name = info[i], Value = int.Parse(info[i + 1]) });
                    }
                    break;
                case nameof(hours_rec_cnt):
                    for (int i = 1; i < info.Length - 1; i += 2)
                    {
                        hours_rec_cnt.Add(new NameValueSet<int>() { Name = info[i], Value = int.Parse(info[i + 1]) });
                    }
                    break;
                case nameof(traffic_hours_last1days):
                    for (int i = 1; i < info.Length - 1; i += 2)
                    {
                        traffic_hours_last1days.Add(new NameValueSet<int>() { Name = info[i], Value = (int)double.Parse(info[i + 1]) });
                    }
                    break;
                case nameof(traffic_hours_last3days):
                    for (int i = 1; i < info.Length - 1; i += 2)
                    {
                        traffic_hours_last3days.Add(new NameValueSet<int>() { Name = info[i], Value = (int)double.Parse(info[i + 1]) });
                    }
                    break;
                case nameof(traffic_hours_today):
                    for (int i = 1; i < info.Length - 1; i += 2)
                    {
                        traffic_hours_today.Add(new NameValueSet<int>() { Name = info[i], Value = (int)double.Parse(info[i + 1]) });
                    }
                    break;
                default:
                    if (info.Length == 2) BasicInfo.Add(new NameValueSet<long>() { Name = info[0], Value = long.Parse(info[1]) });
                    break;
            }
        }
        sr.Close();
    }
}


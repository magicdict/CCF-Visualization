using System.Collections.Generic;
using System.IO;

public static class SecurityDataSet
{
    public static List<NameValueSet<int>> Protocols_Hours = new List<NameValueSet<int>>();
    public static List<NameValueSet<int>> Protocols_Hours_Traffic = new List<NameValueSet<int>>();
    public static List<NameValueSet<int>> traffic_hours_everyday = new List<NameValueSet<int>>();

    //public static string Folder = @"F:\CCF-Visualization\dataprocess\AfterProcess\企业网络资产及安全事件分析与可视化\";
    public static string Folder = @"/root/HelloChinaApi/AfterProcess/企业网络资产及安全事件分析与可视化/";

    public static void LoadData()
    {
        //协议统计的加载
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
    }
}


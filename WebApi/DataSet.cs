using System.Collections.Generic;
using System.IO;

public static class DataSet
{
    public static List<NameValueSet<int>> Protocols = new List<NameValueSet<int>>();


    public static void LoadSecurityData()
    {
        //协议统计的加载
        var Folder = @"F:\CCF-Visualization\AfterProcess\企业网络资产及安全事件分析与可视化\";
        var sr = new StreamReader(Folder + "protocols.csv");
        while (!sr.EndOfStream)
        {
            var info = sr.ReadLine().Split(",");
            Protocols.Add(new NameValueSet<int>() { name = info[0], count = int.Parse(info[1]) });
        }
    }
}

public class NameValueSet<T>
{
    public string name { get; set; }

    public T count { get; set; }
}
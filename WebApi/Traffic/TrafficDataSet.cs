using System.Collections.Generic;
using System.IO;

public static class TrafficDataSet
{
    /// <summary>
    /// 不同协议通信次数
    /// </summary>
    /// <returns></returns>
    public static List<NameValueSet<int>> weekday_hour_orderCnt = new List<NameValueSet<int>>();

    public static void LoadData()
    {
        //协议统计的加载
        var Folder = @"F:\CCF-Visualization\dataprocess\AfterProcess\海口市-交通流量时空演变特征可视分析\";
        //var Folder = @"/root/HelloChinaApi/AfterProcess/海口市-交通流量时空演变特征可视分析/";

        var sr = new StreamReader(Folder + "weekday_hour_orderCnt.csv");
        while (!sr.EndOfStream)
        {
            var info = sr.ReadLine().Split(",");
            weekday_hour_orderCnt.Add(new NameValueSet<int>() { Name = info[0], Value = int.Parse(info[1]) });
        }
        sr.Close();

    }
}


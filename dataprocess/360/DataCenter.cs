using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public static class DataCenterFor360
{

    public const string RawDataFolder = @"F:\CCF-Visualization\RawData\企业网络资产及安全事件分析与可视化";

    public const string AfterProcessFolder = @"F:\CCF-Visualization\AfterProcess\企业网络资产及安全事件分析与可视化\";

    public const string EDAFile = @"F:\CCF-Visualization\AfterProcess\企业网络资产及安全事件分析与可视化\EDA.log";


    public static List<NetRecord> records = new List<NetRecord>();


    /// <summary>
    /// 加载数据
    /// </summary>
    public static void Load(int MaxRecord = -1)
    {
        var cnt = 0;
        foreach (var filename in Directory.GetFiles(RawDataFolder))
        {
            if (filename.EndsWith(".md")) continue; //跳过说明文件
            var sr = new StreamReader(filename);
            while (!sr.EndOfStream)
            {
                records.Add(new NetRecord(sr.ReadLine()));
                cnt++;
                if (cnt == MaxRecord) break;    //内存限制
            }
            sr.Close();
            if (cnt == MaxRecord) break;        //内存限制
        }
        Console.WriteLine("Total Record Count:" + records.Count);
    }

    /// <summary>
    /// EDA
    /// </summary>
    public static void EDA()
    {
        var TotalCnt = records.Count();

        //协议统计
        var protocols = records.GroupBy(x => x.protocol).Select(x => (protocol: x.Key, count: x.Count())).ToList();
        protocols.Sort((x, y) => { return y.count.CompareTo(x.count); });
        var sw = new StreamWriter(EDAFile);

        sw.WriteLine("#协议统计");
        var sw_csv = new StreamWriter(AfterProcessFolder + "protocols.csv");
        foreach (var item in protocols)
        {
            sw.WriteLine(item.protocol + ":" + item.count + "(" + Math.Round((float)item.count * 100 / TotalCnt, 2) + "%)");
            sw_csv.WriteLine(item.protocol + "," + item.count);
        }
        sw_csv.Close();
        sw.WriteLine();

        //日期统计
        sw.WriteLine("#日期统计");
        var dates = records.GroupBy(x => x.record_time.Substring(0, 10)).Select(x => (date: x.Key, count: x.Count())).ToList();
        dates.Sort((x, y) => { return x.date.CompareTo(y.date); });
        foreach (var item in dates)
        {
            sw.WriteLine(item.date + ":" + item.count + "(" + Math.Round((float)item.count * 100 / TotalCnt, 2) + "%)");
        }
        sw.WriteLine();

        //源头和目标统计
        sw.WriteLine("#源头和目标统计");
        var source_dist = records.GroupBy(x => x.source_ip + "->" + x.destination_ip).Select(x => (name: x.Key, count: x.Count())).ToList();
        source_dist.Sort((x, y) => { return y.count.CompareTo(x.count); });

        sw_csv = new StreamWriter(AfterProcessFolder + "source_dist.csv");
        foreach (var item in source_dist)
        {
            sw.WriteLine(item.name + ":" + item.count + "(" + Math.Round((float)item.count * 100 / TotalCnt, 2) + "%)");
            sw_csv.WriteLine(item.name + "," + item.count);
        }
        sw_csv.Close();
        sw.WriteLine();

        //源头统计
        sw.WriteLine("#源头统计");
        var source = records.GroupBy(x => x.source_ip).Select(x => (name: x.Key, count: x.Count())).ToList();
        source.Sort((x, y) => { return y.count.CompareTo(x.count); });

        sw_csv = new StreamWriter(AfterProcessFolder + "source.csv");
        foreach (var item in source)
        {
            sw.WriteLine(item.name + ":" + item.count + "(" + Math.Round((float)item.count * 100 / TotalCnt, 2) + "%)");
            sw_csv.WriteLine(item.name + "," + item.count);
        }
        sw_csv.Close();
        sw.WriteLine();

        //目标统计
        sw.WriteLine("#目标统计");
        var dist = records.GroupBy(x => x.destination_ip).Select(x => (name: x.Key, count: x.Count())).ToList();
        dist.Sort((x, y) => { return y.count.CompareTo(x.count); });

        sw_csv = new StreamWriter(AfterProcessFolder + "dist.csv");
        foreach (var item in dist)
        {
            sw.WriteLine(item.name + ":" + item.count + "(" + Math.Round((float)item.count * 100 / TotalCnt, 2) + "%)");
            sw_csv.WriteLine(item.name + "," + item.count);
        }
        sw_csv.Close();
        sw.WriteLine();


        sw.Close();

    }


}

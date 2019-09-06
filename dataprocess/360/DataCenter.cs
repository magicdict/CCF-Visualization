using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public static class DataCenterFor360
{

    public const string RawDataFolder = @"F:\CCF-Visualization\RawData\企业网络资产及安全事件分析与可视化";

    public const string AfterProcessFolder = @"F:\CCF-Visualization\dataprocess\AfterProcess\企业网络资产及安全事件分析与可视化\";

    public const string EDAFile = @"F:\CCF-Visualization\dataprocess\AfterProcess\企业网络资产及安全事件分析与可视化\EDA.log";


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
        var sw = new StreamWriter(EDAFile);
        //基本信息CSV
        var basic_sw_csv = new StreamWriter(AfterProcessFolder + "basic_info.csv");
        var TotalCnt = records.Count();
        basic_sw_csv.WriteLine("RecordCnt," + TotalCnt);

        //0.IP地址信息
        var ip6Cnt = records.Count(x => x.source_ip.IsIpV6 || x.destination_ip.IsIpV6);
        //去除IPV6的地址
        records = records.Where(x => !x.source_ip.IsIpV6 && !x.destination_ip.IsIpV6).ToList();

        var SourcekindACnt = records.Count(x => x.source_ip.IsKindAIp);
        var SourcekindBCnt = records.Count(x => x.source_ip.IsKindBIp);
        var SourcekindCCnt = records.Count(x => x.source_ip.IsKindCIp);
        var SourceDHCPBlockCnt = records.Count(x => x.source_ip.IsDHCPBlockIp);
        var SourceTotalLanCnt = SourcekindACnt + SourcekindBCnt + SourcekindCCnt + SourceDHCPBlockCnt;

        var DestkindACnt = records.Count(x => x.destination_ip.IsKindAIp);
        var DestkindBCnt = records.Count(x => x.destination_ip.IsKindBIp);
        var DestkindCCnt = records.Count(x => x.destination_ip.IsKindCIp);
        var DestDHCPBlockCnt = records.Count(x => x.destination_ip.IsDHCPBlockIp);
        var DestTotalLanCnt = DestkindACnt + DestkindBCnt + DestkindCCnt + DestDHCPBlockCnt;

        //源头网段统计
        var SourceSegmentCnt = records.Where(x => x.source_ip.IsLAN).GroupBy(x => x.source_ip.Segment).Select(x => x.Key).Count();
        var DestSegmentCnt = records.Where(x => x.destination_ip.IsLAN).GroupBy(x => x.destination_ip.Segment).Select(x => x.Key).Count();

        sw.WriteLine("源头IP：");
        sw.WriteLine("\tA类IP地址：" + SourcekindACnt);
        sw.WriteLine("\tB类IP地址：" + SourcekindBCnt);
        sw.WriteLine("\tC类IP地址：" + SourcekindCCnt);
        sw.WriteLine("\t网段数：" + SourceSegmentCnt);
        sw.WriteLine("\tLAN数：" + SourceTotalLanCnt);

        sw.WriteLine("目标IP：");
        sw.WriteLine("\tA类IP地址：" + DestkindACnt);
        sw.WriteLine("\tB类IP地址：" + DestkindBCnt);
        sw.WriteLine("\tC类IP地址：" + DestkindCCnt);
        sw.WriteLine("\t网段数：" + DestSegmentCnt);
        sw.WriteLine("\tLAN数：" + DestTotalLanCnt);
        sw.WriteLine();

        basic_sw_csv.WriteLine("SourceSegmentCnt," + SourceSegmentCnt);
        basic_sw_csv.WriteLine("SourceTotalLanCnt," + SourceTotalLanCnt);
        basic_sw_csv.WriteLine("DestSegmentCnt," + DestSegmentCnt);
        basic_sw_csv.WriteLine("DestTotalLanCnt," + DestTotalLanCnt);

        //1.协议统计
        var protocols = records.GroupBy(x => x.protocol).Select(x => (protocol: x.Key, count: x.Count())).ToList();
        basic_sw_csv.WriteLine("ProtocolCnt," + protocols.Count);
        protocols.Sort((x, y) => { return y.count.CompareTo(x.count); });


        sw.WriteLine("#协议统计");
        var sw_csv = new StreamWriter(AfterProcessFolder + "protocols.csv");
        foreach (var item in protocols)
        {
            sw.WriteLine(item.protocol + ":" + item.count + "(" + Math.Round((float)item.count * 100 / TotalCnt, 2) + "%)");
            sw_csv.WriteLine(item.protocol + "," + item.count);
        }
        sw_csv.Close();
        sw.WriteLine();

        //2.1日期统计
        sw.WriteLine("#日期统计");
        var dates = records.GroupBy(x => x.record_time.Date.ToString("yyyy/MM/dd")).Select(x => (date: x.Key, count: x.Count())).ToList();
        dates.Sort((x, y) => { return x.date.CompareTo(y.date); });
        foreach (var item in dates)
        {
            sw.WriteLine(item.date + ":" + item.count + "(" + Math.Round((float)item.count * 100 / TotalCnt, 2) + "%)");
        }
        sw.WriteLine();

        //2.2按照时间统计 时间|15分钟单位
        var hours_rec_cnt = records.GroupBy(x => x.record_time.Hour.ToString("D2") + ":" + ((x.record_time.Minute / 15) * 15).ToString("D2")).Select(x => (hour: x.Key, count: x.Count())).ToList();
        hours_rec_cnt.Sort((x, y) => { return x.hour.CompareTo(y.hour); });
        sw_csv = new StreamWriter(AfterProcessFolder + "hours_rec_cnt.csv");
        foreach (var item in hours_rec_cnt)
        {
            sw.WriteLine(item.hour + ":" + item.count + "(" + Math.Round((float)item.count * 100 / TotalCnt, 2) + "%)");
            sw_csv.WriteLine(item.hour + "," + item.count);
        }
        sw_csv.Close();
        sw.WriteLine();

        //对于TOP5的进行分别统计
        var Top5protocols = protocols.Take(5).Select(x => x.protocol).ToList();
        var protocols_hours = records.Where(x => Top5protocols.Contains(x.protocol))
                              .GroupBy(x => x.protocol + "," + x.record_time.Hour.ToString("D2") + ":" + ((x.record_time.Minute / 15) * 15)
                              .ToString("D2")).Select(x => (hour: x.Key, count: x.Count())).ToList();
        protocols_hours.Sort((x, y) => { return x.hour.CompareTo(y.hour); });
        sw_csv = new StreamWriter(AfterProcessFolder + "protocols_hours.csv");
        foreach (var item in protocols_hours)
        {
            sw.WriteLine(item.hour + ":" + item.count + "(" + Math.Round((float)item.count * 100 / TotalCnt, 2) + "%)");
            sw_csv.WriteLine(item.hour + "," + item.count);
        }
        sw_csv.Close();
        sw.WriteLine();

        var protocols_hours_traffic = records.Where(x => Top5protocols.Contains(x.protocol))
                              .GroupBy(x => x.protocol + "," + x.record_time.Hour.ToString("D2") + ":" + ((x.record_time.Minute / 15) * 15)
                              .ToString("D2")).Select(x => (hour: x.Key, count: (double)x.Sum(y => y.uplink_length + y.downlink_length) / (1024 * 1024))).ToList();
        protocols_hours_traffic.Sort((x, y) => { return x.hour.CompareTo(y.hour); });
        sw_csv = new StreamWriter(AfterProcessFolder + "protocols_hours_traffic.csv");
        foreach (var item in protocols_hours_traffic)
        {
            sw.WriteLine(item.hour + ":" + Math.Round((double)item.count, 4) + "(" + Math.Round((float)item.count * 100 / TotalCnt, 2) + "%)");
            sw_csv.WriteLine(item.hour + "," + Math.Round((double)item.count, 4));
        }
        sw_csv.Close();
        sw.WriteLine();


        //3.源头和目标统计
        sw.WriteLine("#源头和目标统计");
        var source_dist = records.GroupBy(x => x.source_ip.RawIp + "->" + x.destination_ip.RawIp).Select(x => (name: x.Key, count: x.Count())).ToList();
        basic_sw_csv.WriteLine("SourceDistIpCnt," + source_dist.Count);  //抢先记录，下面被Take掉了

        source_dist.Sort((x, y) => { return y.count.CompareTo(x.count); });
        source_dist = source_dist.Take(100).ToList();

        sw_csv = new StreamWriter(AfterProcessFolder + "source_dist.csv");
        foreach (var item in source_dist)
        {
            sw.WriteLine(item.name + ":" + item.count + "(" + Math.Round((float)item.count * 100 / TotalCnt, 2) + "%)");
            sw_csv.WriteLine(item.name + "," + item.count);
        }
        sw_csv.Close();
        sw.WriteLine();

        //4.源头统计
        sw.WriteLine("#源头统计");
        var source = records.GroupBy(x => x.source_ip.RawIp).Select(x => (name: x.Key, count: x.Count())).ToList();
        basic_sw_csv.WriteLine("SourceIpCnt," + source.Count);  //抢先记录，下面被Take掉了
        source.Sort((x, y) => { return y.count.CompareTo(x.count); });
        source = source.Take(100).ToList();

        sw_csv = new StreamWriter(AfterProcessFolder + "source.csv");
        foreach (var item in source)
        {
            sw.WriteLine(item.name + ":" + item.count + "(" + Math.Round((float)item.count * 100 / TotalCnt, 2) + "%)");
            sw_csv.WriteLine(item.name + "," + item.count);
        }
        sw_csv.Close();
        sw.WriteLine();

        //5.目标统计
        sw.WriteLine("#目标统计");
        var dist = records.GroupBy(x => x.destination_ip.RawIp).Select(x => (name: x.Key, count: x.Count())).ToList();
        basic_sw_csv.WriteLine("DistIpCnt," + dist.Count);  //抢先记录，下面被Take掉了
        dist.Sort((x, y) => { return y.count.CompareTo(x.count); });
        dist = dist.Take(100).ToList();

        sw_csv = new StreamWriter(AfterProcessFolder + "dist.csv");
        foreach (var item in dist)
        {
            sw.WriteLine(item.name + ":" + item.count + "(" + Math.Round((float)item.count * 100 / TotalCnt, 2) + "%)");
            sw_csv.WriteLine(item.name + "," + item.count);
        }
        sw_csv.Close();
        sw.WriteLine();

        //6.各个协议的总流量统计
        sw.WriteLine("#下行总流量统计(单位：MB)");
        var downlink_length = records.GroupBy(x => x.protocol).Select(x => (protocol: x.Key, count: (double)x.Sum(y => y.downlink_length) / (1024 * 1024))).ToList();
        basic_sw_csv.WriteLine("downlink_length," + Math.Round(downlink_length.Sum(x => x.count), 2));
        downlink_length.Sort((x, y) => { return y.count.CompareTo(x.count); });
        sw_csv = new StreamWriter(AfterProcessFolder + "downlink_length.csv");
        foreach (var item in downlink_length)
        {
            sw.WriteLine(item.protocol + ":" + Math.Round((double)item.count, 4) + "(" + Math.Round((float)item.count * 100 / TotalCnt, 2) + "%)");
            sw_csv.WriteLine(item.protocol + "," + Math.Round((double)item.count, 4));
        }
        sw_csv.Close();
        sw.WriteLine();

        sw.WriteLine("#上行总流量统计(单位：MB)");
        var uplink_length = records.GroupBy(x => x.protocol).Select(x => (protocol: x.Key, count: (double)x.Sum(y => y.uplink_length) / (1024 * 1024))).ToList();
        basic_sw_csv.WriteLine("uplink_length," + Math.Round(uplink_length.Sum(x => x.count), 2));
        uplink_length.Sort((x, y) => { return y.count.CompareTo(x.count); });
        sw_csv = new StreamWriter(AfterProcessFolder + "uplink_length.csv");
        foreach (var item in uplink_length)
        {
            sw.WriteLine(item.protocol + ":" + Math.Round((double)item.count, 4) + "(" + Math.Round((float)item.count * 100 / TotalCnt, 2) + "%)");
            sw_csv.WriteLine(item.protocol + "," + Math.Round((double)item.count, 4));
        }
        sw_csv.Close();

        //上下行总流量按照时间（最近三天）      
        var traffic_hours_last3days = records.Where(x => x.record_time.Day == 15 || x.record_time.Day == 16 || x.record_time.Day == 17)
                                             .GroupBy(x => x.record_time.Hour.ToString("D2") + ":" + ((x.record_time.Minute / 15) * 15).ToString("D2"))
                                             .Select(x => (hour: x.Key, count: (double)x.Sum(y => y.uplink_length + y.downlink_length) / (1024 * 1024))).ToList();
        traffic_hours_last3days.Sort((x, y) => { return x.hour.CompareTo(y.hour); });
        sw_csv = new StreamWriter(AfterProcessFolder + "traffic_hours_last3days.csv");
        foreach (var item in traffic_hours_last3days)
        {
            sw.WriteLine(item.hour + ":" + item.count + "(" + Math.Round((float)item.count * 100 / TotalCnt, 2) + "%)");
            sw_csv.WriteLine(item.hour + "," + Math.Round((double)item.count, 4));
        }
        sw_csv.Close();
        sw.WriteLine();

        //上下行总流量按照时间（昨天）
        var traffic_hours_last1days = records.Where(x => x.record_time.Day == 17)
                                             .GroupBy(x => x.record_time.Hour.ToString("D2") + ":" + ((x.record_time.Minute / 15) * 15).ToString("D2"))
                                             .Select(x => (hour: x.Key, count: (double)x.Sum(y => y.uplink_length + y.downlink_length) / (1024 * 1024))).ToList();
        traffic_hours_last1days.Sort((x, y) => { return x.hour.CompareTo(y.hour); });
        sw_csv = new StreamWriter(AfterProcessFolder + "traffic_hours_last1days.csv");
        foreach (var item in traffic_hours_last1days)
        {
            sw.WriteLine(item.hour + ":" + item.count + "(" + Math.Round((float)item.count * 100 / TotalCnt, 2) + "%)");
            sw_csv.WriteLine(item.hour + "," + Math.Round((double)item.count, 4));
        }
        sw_csv.Close();
        sw.WriteLine();

        //上下行总流量按照时间（今天）
        var traffic_hours_today = records.Where(x => x.record_time.Day == 18)
                                             .GroupBy(x => x.record_time.Hour.ToString("D2") + ":" + ((x.record_time.Minute / 15) * 15).ToString("D2"))
                                             .Select(x => (hour: x.Key, count: (double)x.Sum(y => y.uplink_length + y.downlink_length) / (1024 * 1024))).ToList();
        traffic_hours_today.Sort((x, y) => { return x.hour.CompareTo(y.hour); });
        sw_csv = new StreamWriter(AfterProcessFolder + "traffic_hours_today.csv");
        foreach (var item in traffic_hours_today)
        {
            sw.WriteLine(item.hour + ":" + item.count + "(" + Math.Round((float)item.count * 100 / TotalCnt, 2) + "%)");
            sw_csv.WriteLine(item.hour + "," + Math.Round((double)item.count, 4));
        }
        sw_csv.Close();
        sw.WriteLine();

        //上下行总流量按照时间（每日）
        var traffic_hours_everyday = records.GroupBy(x => x.record_time.Date.ToString("MM/dd") + "|" + x.record_time.Hour.ToString("D2") + ":" + ((x.record_time.Minute / 15) * 15).ToString("D2"))
                                             .Select(x => (hour: x.Key, count: (double)x.Sum(y => y.uplink_length + y.downlink_length) / (1024 * 1024))).ToList();
        traffic_hours_everyday.Sort((x, y) => { return x.hour.CompareTo(y.hour); });
        sw_csv = new StreamWriter(AfterProcessFolder + "traffic_hours_everyday.csv");
        foreach (var item in traffic_hours_everyday)
        {
            sw.WriteLine(item.hour + ":" + item.count + "(" + Math.Round((float)item.count * 100 / TotalCnt, 2) + "%)");
            sw_csv.WriteLine(item.hour + "," + Math.Round((double)item.count, 4));
        }
        sw_csv.Close();
        sw.WriteLine();


        sw.WriteLine("#上行/下行总流量比统计");
        var uplink_downlink_rate = records.GroupBy(x => x.protocol).Select(x => (protocol: x.Key, count: (double)x.Sum(y => y.uplink_length) / x.Sum(y => y.downlink_length))).ToList();
        uplink_downlink_rate.Sort((x, y) => { return y.count.CompareTo(x.count); });
        sw_csv = new StreamWriter(AfterProcessFolder + "uplink_downlink_rate.csv");
        foreach (var item in uplink_downlink_rate)
        {
            sw.WriteLine(item.protocol + ":" + Math.Round((double)item.count, 4));
            sw_csv.WriteLine(item.protocol + "," + Math.Round((double)item.count, 4));
        }

        basic_sw_csv.Close();
        sw_csv.Close();
        sw.Close();
    }
}

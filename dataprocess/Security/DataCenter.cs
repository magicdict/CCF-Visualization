using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

public static class DataCenterForSecurity
{

    public const string RawDataFolder = @"F:\CCF-Visualization\RawData\企业网络资产及安全事件分析与可视化";

    public const string AfterProcessFolder = @"F:\CCF-Visualization\dataprocess\AfterProcess\企业网络资产及安全事件分析与可视化\";

    public const string EDAFile = @"F:\CCF-Visualization\dataprocess\AfterProcess\企业网络资产及安全事件分析与可视化\EDA.log";

    public const string AngularJsonAssetsFolder = @"F:\CCF-Visualization\UI\src\assets\security\json\";

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


    public static void Protocol_Port()
    {
        var x = records.Where(x => x.protocol == "unknown").GroupBy(x => x.destination_port).Select(x => new { port = x.Key, count = x.Count() }).ToList();
        x.Sort((x, y) => { return y.count.CompareTo(x.count); });
        foreach (var item in x.Take(100))
        {
            Console.WriteLine(item.port + ":" + item.count);
        }
    }

    public static void CreateSourceIpTreeJson()
    {
        //IP地址转树形json数据
        var tree = new treeItem() { name = "Source Ip", children = new List<treeItem>() };
        records = records.Where(x => x.source_ip.IsIpV4 && x.source_ip.IsLAN).ToList();
        //放入顶层IP地址
        var rootsegs = records.Select(x => x.source_ip.SegmentRoot).Distinct();
        var segs = records.GroupBy(x => x.source_ip.Segment)
                          .Select(x => new treeItem()
                          {
                              name = x.Key,
                              value = x.Select(x => x.source_ip.RawIp).Distinct().Count()
                          }).ToList();
        foreach (var rootseg in rootsegs)
        {
            var parentsegtrees = new List<treeItem>();
            //准备中间层
            var parentsegs = records.Where(x => x.source_ip.SegmentRoot == rootseg)
                                   .Select(x => x.source_ip.SegmentParent).Distinct();
            foreach (var parentseg in parentsegs)
            {
                var parenttree = new treeItem()
                {
                    name = parentseg,
                    collapsed = false
                };
                //注意，不能使用startwith做！21，213，这样无法判定
                parenttree.children = segs.Where(x => x.name.Substring(0, x.name.LastIndexOf(".")) == parentseg).ToList();
                parenttree.value = parenttree.children.Count();
                parentsegtrees.Add(parenttree);
            }
            tree.children.Add(new treeItem() { name = rootseg, children = parentsegtrees });
        }
        var sw = new StreamWriter(AngularJsonAssetsFolder + "sourceip_tree.json");
        sw.Write(JsonConvert.SerializeObject(tree));
        sw.Close();
    }

    public static void CreateDistIpTreeJson()
    {
        //IP地址转树形json数据
        var tree = new treeItem() { name = "Dist Ip", children = new List<treeItem>() };
        records = records.Where(x => x.destination_ip.IsIpV4 && x.destination_ip.IsLAN).ToList();
        //放入顶层IP地址
        var rootsegs = records.Select(x => x.destination_ip.SegmentRoot).Distinct();
        var segs = records.GroupBy(x => x.destination_ip.Segment)
                          .Select(x => new treeItem()
                          {
                              name = x.Key,
                              value = x.Select(x => x.destination_ip.RawIp).Distinct().Count()
                          }).ToList();
        foreach (var rootseg in rootsegs)
        {
            var parentsegtrees = new List<treeItem>();
            //准备中间层
            var parentsegs = records.Where(x => x.destination_ip.SegmentRoot == rootseg)
                                   .Select(x => x.destination_ip.SegmentParent).Distinct();
            foreach (var parentseg in parentsegs)
            {
                var parenttree = new treeItem()
                {
                    name = parentseg,
                    collapsed = false
                };
                //注意，不能使用startwith做！21，213，这样无法判定
                parenttree.children = segs.Where(x => x.name.Substring(0, x.name.LastIndexOf(".")) == parentseg).ToList();
                parenttree.value = parenttree.children.Count();
                parentsegtrees.Add(parenttree);
            }
            tree.children.Add(new treeItem() { name = rootseg, children = parentsegtrees });
        }
        var sw = new StreamWriter(AngularJsonAssetsFolder + "distip_tree.json");
        sw.Write(JsonConvert.SerializeObject(tree));
        sw.Close();

    }

    [Serializable()]
    class treeItem
    {
        public string name;
        public List<treeItem> children;
        public int value;
        public bool collapsed;
    }

    /// <summary>
    /// 通讯模式
    /// </summary>
    public static void CommunicationMode()
    {
        //按照源头地址和时间进行排序
        records.Sort((x, y) =>
        {
            if (x.source_ip.RawIp == y.source_ip.RawIp)
            {
                return x.record_time.CompareTo(y.record_time);
            }
            else
            {
                return x.source_ip.RawIp.CompareTo(y.source_ip.RawIp);
            }
        });
        var sw = new StreamWriter(AfterProcessFolder + "commucationMode.csv");
        foreach (var item in records)
        {
            sw.WriteLine(item.source_ip.RawIp + "," + item.record_time.ToString("yyyy-MM-dd HH:mm:ss.fff") + "," + item.protocol);
        }
        sw.Close();
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


        basic_sw_csv.WriteLine("SourcekindACnt," + SourcekindACnt);
        basic_sw_csv.WriteLine("SourcekindBCnt," + SourcekindBCnt);
        basic_sw_csv.WriteLine("SourcekindCCnt," + SourcekindCCnt);
        basic_sw_csv.WriteLine("SourceSegmentCnt," + SourceSegmentCnt);
        basic_sw_csv.WriteLine("SourceTotalLanCnt," + SourceTotalLanCnt);
        basic_sw_csv.WriteLine("DestkindACnt," + DestkindACnt);
        basic_sw_csv.WriteLine("DestkindBCnt," + DestkindBCnt);
        basic_sw_csv.WriteLine("DestkindCCnt," + DestkindCCnt);
        basic_sw_csv.WriteLine("DestSegmentCnt," + DestSegmentCnt);
        basic_sw_csv.WriteLine("DestTotalLanCnt," + DestTotalLanCnt);

        //1.协议统计
        sw.WriteLine("#协议统计");
        var protocols = records.GroupBy(x => x.protocol).Select(x => (protocol: x.Key, count: x.Count())).ToList();
        basic_sw_csv.WriteLine("ProtocolCnt," + protocols.Count);
        protocols.Sort((x, y) => { return y.count.CompareTo(x.count); });
        basic_sw_csv.Write("protocols,");
        foreach (var item in protocols)
        {
            sw.WriteLine(item.protocol + ":" + item.count + "(" + Math.Round((float)item.count * 100 / TotalCnt, 2) + "%)");
            basic_sw_csv.Write(item.protocol + "," + item.count + ",");
        }
        basic_sw_csv.WriteLine();
        sw.WriteLine();


        sw.WriteLine("#端口统计");
        var ports = records.GroupBy(x => x.destination_port).Select(x => (port: x.Key, count: x.Count())).ToList();
        basic_sw_csv.WriteLine("PortCnt," + ports.Count);
        ports.Sort((x, y) => { return y.count.CompareTo(x.count); });
        ports = ports.Take(100).ToList();
        basic_sw_csv.Write("ports,");
        foreach (var item in ports)
        {
            sw.WriteLine(item.port + ":" + item.count + "(" + Math.Round((float)item.count * 100 / TotalCnt, 2) + "%)");
            basic_sw_csv.Write(item.port + "," + item.count + ",");
        }
        basic_sw_csv.WriteLine();
        sw.WriteLine();


        sw.WriteLine("#端口协议统计");
        var protocolports = records.GroupBy(x => x.protocol + ":" + x.destination_port).Select(x => (port: x.Key, count: x.Count())).ToList();
        basic_sw_csv.WriteLine("protocolportsCnt," + protocolports.Count);
        protocolports.Sort((x, y) => { return y.count.CompareTo(x.count); });
        protocolports = protocolports.Take(100).ToList();
        basic_sw_csv.Write("protocolports,");
        foreach (var item in protocolports)
        {
            sw.WriteLine(item.port + ":" + item.count + "(" + Math.Round((float)item.count * 100 / TotalCnt, 2) + "%)");
            basic_sw_csv.Write(item.port + "," + item.count + ",");
        }
        basic_sw_csv.WriteLine();
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


        //上下行总次数按照时间（最近三天）      
        var access_hours_last3days = records.Where(x => x.record_time.Day == 15 || x.record_time.Day == 16 || x.record_time.Day == 17)
                                             .GroupBy(x => x.record_time.Hour.ToString("D2") + ":" + ((x.record_time.Minute / 15) * 15).ToString("D2"))
                                             .Select(x => (hour: x.Key, count: x.Count())).ToList();
        access_hours_last3days.Sort((x, y) => { return x.hour.CompareTo(y.hour); });
        basic_sw_csv.Write("access_hours_last3days,");
        foreach (var item in access_hours_last3days)
        {
            sw.WriteLine(item.hour + ":" + item.count + "(" + Math.Round((float)item.count * 100 / TotalCnt, 2) + "%)");
            basic_sw_csv.Write(item.hour + "," + item.count + ",");
        }
        basic_sw_csv.WriteLine();
        sw.WriteLine();

        //上下行总次数按照时间（昨天）
        var access_hours_last1days = records.Where(x => x.record_time.Day == 17)
                                             .GroupBy(x => x.record_time.Hour.ToString("D2") + ":" + ((x.record_time.Minute / 15) * 15).ToString("D2"))
                                             .Select(x => (hour: x.Key, count: x.Count())).ToList();
        access_hours_last1days.Sort((x, y) => { return x.hour.CompareTo(y.hour); });
        basic_sw_csv.Write("access_hours_last1days,");
        foreach (var item in access_hours_last1days)
        {
            sw.WriteLine(item.hour + ":" + item.count + "(" + Math.Round((float)item.count * 100 / TotalCnt, 2) + "%)");
            basic_sw_csv.Write(item.hour + "," + item.count + ",");
        }
        basic_sw_csv.WriteLine();
        sw.WriteLine();

        //上下行总次数按照时间（今天）
        var access_hours_today = records.Where(x => x.record_time.Day == 18)
                                             .GroupBy(x => x.record_time.Hour.ToString("D2") + ":" + ((x.record_time.Minute / 15) * 15).ToString("D2"))
                                             .Select(x => (hour: x.Key, count: x.Count())).ToList();
        access_hours_today.Sort((x, y) => { return x.hour.CompareTo(y.hour); });
        basic_sw_csv.Write("access_hours_today,");
        foreach (var item in access_hours_today)
        {
            sw.WriteLine(item.hour + ":" + item.count + "(" + Math.Round((float)item.count * 100 / TotalCnt, 2) + "%)");
            basic_sw_csv.Write(item.hour + "," + item.count + ",");
        }
        basic_sw_csv.WriteLine();
        sw.WriteLine();


        //2.2按照时间统计 时间|15分钟单位
        var hours_rec_cnt = records.GroupBy(x => x.record_time.Hour.ToString("D2") + ":" + ((x.record_time.Minute / 15) * 15).ToString("D2")).Select(x => (hour: x.Key, count: x.Count())).ToList();
        hours_rec_cnt.Sort((x, y) => { return x.hour.CompareTo(y.hour); });
        basic_sw_csv.Write("hours_rec_cnt,");
        foreach (var item in hours_rec_cnt)
        {
            sw.WriteLine(item.hour + ":" + item.count + "(" + Math.Round((float)item.count * 100 / TotalCnt, 2) + "%)");
            basic_sw_csv.Write(item.hour + "," + item.count + ",");
        }
        basic_sw_csv.WriteLine();
        sw.WriteLine();

        //对于TOP5的进行分别统计
        var Top5protocols = protocols.Take(5).Select(x => x.protocol).ToList();
        var protocols_hours = records.Where(x => Top5protocols.Contains(x.protocol))
                              .GroupBy(x => x.protocol + "," + x.record_time.Hour.ToString("D2") + ":" + ((x.record_time.Minute / 15) * 15)
                              .ToString("D2")).Select(x => (hour: x.Key, count: x.Count())).ToList();
        protocols_hours.Sort((x, y) => { return x.hour.CompareTo(y.hour); });
        var sw_csv = new StreamWriter(AfterProcessFolder + "protocols_hours.csv");
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
        var source_dist = records.GroupBy(x => x.source_ip.RawIp + "->" + x.destination_ip.RawIp + ":" + x.destination_port).Select(x => (name: x.Key, count: x.Count())).ToList();
        basic_sw_csv.WriteLine("SourceDistIpCnt," + source_dist.Count);  //抢先记录，下面被Take掉了

        source_dist.Sort((x, y) => { return y.count.CompareTo(x.count); });
        source_dist = source_dist.Take(100).ToList();

        basic_sw_csv.Write("source_dist,");
        foreach (var item in source_dist)
        {
            sw.WriteLine(item.name + ":" + item.count + "(" + Math.Round((float)item.count * 100 / TotalCnt, 2) + "%)");
            basic_sw_csv.Write(item.name + "," + item.count + ",");
        }
        basic_sw_csv.WriteLine();
        sw.WriteLine();

        //源头目标Distinct
        var distinctsource = source_dist.Select(x => x.name.Split("->")[0]).Distinct().Count();
        var distinctdist = source_dist.Select(x => x.name.Split("->")[1]).Distinct().Count();
        Console.WriteLine("distinctsource：" + distinctsource);
        Console.WriteLine("distinctdist" + distinctdist);




        //4.源头统计
        sw.WriteLine("#源头统计");
        var source = records.GroupBy(x => x.source_ip.RawIp).Select(x => (name: x.Key, count: x.Count())).ToList();
        basic_sw_csv.WriteLine("SourceIpCnt," + source.Count);  //抢先记录，下面被Take掉了
        source.Sort((x, y) => { return y.count.CompareTo(x.count); });
        var source_Top100 = source.Take(100).ToList();

        basic_sw_csv.Write("source,");
        foreach (var item in source_Top100)
        {
            sw.WriteLine(item.name + ":" + item.count + "(" + Math.Round((float)item.count * 100 / TotalCnt, 2) + "%)");
            basic_sw_csv.Write(item.name + "," + item.count + ",");
        }
        basic_sw_csv.WriteLine();
        sw.WriteLine();

        //5.目标统计
        sw.WriteLine("#目标统计");
        var dist = records.GroupBy(x => x.destination_ip.RawIp).Select(x => (name: x.Key, count: x.Count())).ToList();
        basic_sw_csv.WriteLine("DistIpCnt," + dist.Count);  //抢先记录，下面被Take掉了
        dist.Sort((x, y) => { return y.count.CompareTo(x.count); });
        var dist_Top100 = dist.Take(100).ToList();

        basic_sw_csv.Write("dist,");
        foreach (var item in dist_Top100)
        {
            sw.WriteLine(item.name + ":" + item.count + "(" + Math.Round((float)item.count * 100 / TotalCnt, 2) + "%)");
            basic_sw_csv.Write(item.name + "," + item.count + ",");
        }
        basic_sw_csv.WriteLine();
        sw.WriteLine();

        //在源头和目标的次数都很多
        basic_sw_csv.Write("source_dist_Same,");
        Parallel.ForEach(source, (s, loop) =>
        {
            var d = dist.Where(x => x.name == s.name);
            if (d.Count() == 1)
            {
                var rate = Math.Round((float)s.count * 100 / (s.count + d.First().count), 2);
                var totalcnt = s.count + d.First().count;
                if (rate > 35 && rate < 65 && totalcnt > 1000)
                {
                    basic_sw_csv.Write(s.name + "," + s.count + "|" + d.First().count + "|" + rate + "%" + ",");
                }
            }
        });
        basic_sw_csv.WriteLine();

        //6.流量统计
        sw.WriteLine("#下行总流量统计(单位：MB)");
        var downlink_length = records.GroupBy(x => x.protocol).Select(x => (protocol: x.Key, count: (double)x.Sum(y => y.downlink_length) / (1024 * 1024))).ToList();
        basic_sw_csv.WriteLine("downlink_length," + Math.Round(downlink_length.Sum(x => x.count), 2));
        downlink_length.Sort((x, y) => { return y.count.CompareTo(x.count); });
        basic_sw_csv.Write("downlink_length_rank,");
        foreach (var item in downlink_length)
        {
            sw.WriteLine(item.protocol + ":" + Math.Round((double)item.count, 4) + "(" + Math.Round((float)item.count * 100 / TotalCnt, 2) + "%)");
            basic_sw_csv.Write(item.protocol + "," + Math.Round((double)item.count, 4) + ",");
        }
        basic_sw_csv.WriteLine();
        sw.WriteLine();

        sw.WriteLine("#上行总流量统计(单位：MB)");
        var uplink_length = records.GroupBy(x => x.protocol).Select(x => (protocol: x.Key, count: (double)x.Sum(y => y.uplink_length) / (1024 * 1024))).ToList();
        basic_sw_csv.WriteLine("uplink_length," + Math.Round(uplink_length.Sum(x => x.count), 2));
        uplink_length.Sort((x, y) => { return y.count.CompareTo(x.count); });
        basic_sw_csv.Write("uplink_length_rank,");
        foreach (var item in uplink_length)
        {
            sw.WriteLine(item.protocol + ":" + Math.Round((double)item.count, 4) + "(" + Math.Round((float)item.count * 100 / TotalCnt, 2) + "%)");
            basic_sw_csv.Write(item.protocol + "," + Math.Round((double)item.count, 4) + ",");
        }
        basic_sw_csv.WriteLine();
        sw_csv.Close();

        //上下行总流量按照时间（最近三天）      
        var traffic_hours_last3days = records.Where(x => x.record_time.Day == 15 || x.record_time.Day == 16 || x.record_time.Day == 17)
                                             .GroupBy(x => x.record_time.Hour.ToString("D2") + ":" + ((x.record_time.Minute / 15) * 15).ToString("D2"))
                                             .Select(x => (hour: x.Key, count: (double)x.Sum(y => y.uplink_length + y.downlink_length) / (1024 * 1024))).ToList();
        traffic_hours_last3days.Sort((x, y) => { return x.hour.CompareTo(y.hour); });
        basic_sw_csv.Write("traffic_hours_last3days,");
        foreach (var item in traffic_hours_last3days)
        {
            sw.WriteLine(item.hour + ":" + item.count + "(" + Math.Round((float)item.count * 100 / TotalCnt, 2) + "%)");
            basic_sw_csv.Write(item.hour + "," + Math.Round((double)item.count, 4) + ",");
        }
        basic_sw_csv.WriteLine();
        sw.WriteLine();

        //上下行总流量按照时间（昨天）
        var traffic_hours_last1days = records.Where(x => x.record_time.Day == 17)
                                             .GroupBy(x => x.record_time.Hour.ToString("D2") + ":" + ((x.record_time.Minute / 15) * 15).ToString("D2"))
                                             .Select(x => (hour: x.Key, count: (double)x.Sum(y => y.uplink_length + y.downlink_length) / (1024 * 1024))).ToList();
        traffic_hours_last1days.Sort((x, y) => { return x.hour.CompareTo(y.hour); });
        basic_sw_csv.Write("traffic_hours_last1days,");
        foreach (var item in traffic_hours_last1days)
        {
            sw.WriteLine(item.hour + ":" + item.count + "(" + Math.Round((float)item.count * 100 / TotalCnt, 2) + "%)");
            basic_sw_csv.Write(item.hour + "," + Math.Round((double)item.count, 4) + ",");
        }
        basic_sw_csv.WriteLine();
        sw.WriteLine();

        //上下行总流量按照时间（今天）
        var traffic_hours_today = records.Where(x => x.record_time.Day == 18)
                                             .GroupBy(x => x.record_time.Hour.ToString("D2") + ":" + ((x.record_time.Minute / 15) * 15).ToString("D2"))
                                             .Select(x => (hour: x.Key, count: (double)x.Sum(y => y.uplink_length + y.downlink_length) / (1024 * 1024))).ToList();
        traffic_hours_today.Sort((x, y) => { return x.hour.CompareTo(y.hour); });
        basic_sw_csv.Write("traffic_hours_today,");
        foreach (var item in traffic_hours_today)
        {
            sw.WriteLine(item.hour + ":" + item.count + "(" + Math.Round((float)item.count * 100 / TotalCnt, 2) + "%)");
            basic_sw_csv.Write(item.hour + "," + Math.Round((double)item.count, 4) + ",");
        }
        basic_sw_csv.WriteLine();
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
        basic_sw_csv.Write("uplink_downlink_rate,");
        foreach (var item in uplink_downlink_rate)
        {
            sw.WriteLine(item.protocol + ":" + Math.Round((double)item.count, 4));
            basic_sw_csv.Write(item.protocol + "," + Math.Round((double)item.count, 4) + ",");
        }

        basic_sw_csv.Close();
        sw_csv.Close();
        sw.Close();
    }

    public class NameValueSet<T>
    {
        public string Name { get; set; }

        public T Value { get; set; }
    }
    public class ProtocolProfile
    {
        public string Name;
        public List<NameValueSet<int>> Ports;

        public List<NameValueSet<int>> DistIps;

        public List<NameValueSet<int>> SourceIps;

        public List<NameValueSet<int>> Source_dist;

    }

    public static void GetProtocolProfile(string protocolname)
    {
        var protocolrecs = records.Where(x => x.protocol == protocolname).ToList();
        var profile = new ProtocolProfile();
        profile.Name = protocolname;
        //端口号的整理
        profile.Ports = protocolrecs.GroupBy(x => x.destination_port).Select(x => new NameValueSet<int> { Name = x.Key, Value = x.Count() }).ToList();
        profile.Ports = profile.Ports.Take(9).ToList();
        profile.Ports.Add(new NameValueSet<int>() { Name = "Ohter", Value = protocolrecs.Count - profile.Ports.Sum(x => x.Value) });
        profile.Ports.Sort((x, y) => { return y.Value.CompareTo(x.Value); });

        profile.DistIps = protocolrecs.GroupBy(x => x.destination_ip.RawIp).Select(x => new NameValueSet<int> { Name = x.Key, Value = x.Count() }).ToList();
        profile.DistIps.Sort((x, y) => { return y.Value.CompareTo(x.Value); });
        profile.DistIps = profile.DistIps.Take(5).ToList();


        profile.SourceIps = protocolrecs.GroupBy(x => x.source_ip.RawIp).Select(x => new NameValueSet<int> { Name = x.Key, Value = x.Count() }).ToList();
        profile.SourceIps.Sort((x, y) => { return y.Value.CompareTo(x.Value); });
        profile.SourceIps = profile.SourceIps.Take(5).ToList();

        profile.Source_dist = protocolrecs.GroupBy(x => x.source_ip.RawIp + "->" + x.destination_ip.RawIp + ":" + x.destination_port).Select(x => new NameValueSet<int> { Name = x.Key, Value = x.Count() }).ToList();
        profile.Source_dist.Sort((x, y) => { return y.Value.CompareTo(x.Value); });
        profile.Source_dist = profile.Source_dist.Take(5).ToList();


        var sw = new StreamWriter(AngularJsonAssetsFolder + protocolname + ".json");
        sw.Write(JsonConvert.SerializeObject(profile));
        sw.Close();
    }
}

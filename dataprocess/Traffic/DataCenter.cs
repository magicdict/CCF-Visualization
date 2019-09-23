using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using static OrderDetails;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Diagnostics;

public static class DataCenterForTraffic
{

    public const string DataFolder = @"F:\CCF-Visualization\RawData\海口市-交通流量时空演变特征可视分析";
    public const string EDAFile = @"F:\CCF-Visualization\dataprocess\AfterProcess\海口市-交通流量时空演变特征可视分析\EDA.log";
    public const string AfterProcessFolder = @"F:\CCF-Visualization\dataprocess\AfterProcess\海口市-交通流量时空演变特征可视分析\";
    public const string AngularJsonAssetsFolder = @"F:\CCF-Visualization\UI\src\assets\traffic\json\";
    public static List<OrderDetails> orders = new List<OrderDetails>();
    public static List<DiaryProperty> diarys = new List<DiaryProperty>();

    /// <summary>
    /// 加载数据
    /// </summary>
    public static void Load(int MaxRecord = -1)
    {
        var cnt = 0;
        foreach (var filename in Directory.GetFiles(DataFolder))
        {
            var sr = new StreamReader(filename);
            sr.ReadLine();  //Skip Title
            while (!sr.EndOfStream)
            {
                orders.Add(new OrderDetails(sr.ReadLine()));
                cnt++;
                if (cnt == MaxRecord) break;    //内存限制
            }
            sr.Close();
            if (cnt == MaxRecord) break;        //内存限制
        }
        Console.WriteLine("Total Record Count:" + orders.Count);
    }


    public static bool IsCreateTrace = false;
    public static bool IsCreate24HoursGeoJson = false;
    public static bool IsCreateWeekNoGeoJson = false;
    public static bool IsCreateGeoJson = false;
    /// <summary>
    /// EDA
    /// </summary>
    public static void EDA()
    {
        //基本信息CSV
        var basic_sw_csv = new StreamWriter(AfterProcessFolder + "basic_info.csv");
        var sw = new StreamWriter(EDAFile);
        var TotalOrderCnt = orders.Count;
        var TotalFee = (int)orders.Sum(x => x.pre_total_fee);
        var TotalDistanceKm = (int)orders.Sum(x => x.start_dest_distance_km);

        basic_sw_csv.WriteLine("TotalOrderCnt," + TotalOrderCnt);
        basic_sw_csv.WriteLine("TotalFee," + TotalFee);
        basic_sw_csv.WriteLine("AvgFeePerOrder," + Math.Round((double)TotalFee / TotalOrderCnt, 2));
        basic_sw_csv.WriteLine("TotalDistanceKm," + TotalDistanceKm);
        basic_sw_csv.WriteLine("AvgDistanceKmPerOrder," + Math.Round((double)TotalDistanceKm / TotalOrderCnt, 4));
        basic_sw_csv.WriteLine("FeePerKm," + Math.Round((double)TotalFee / TotalDistanceKm, 2));
        basic_sw_csv.Flush();

        //0:区县
        var countys = orders.GroupBy(x => x.county).Select(x => (name: x.Key, count: x.Count())).ToList();
        sw.WriteLine("区号[countys]:");
        basic_sw_csv.Write("countys,");

        var othersCnt = 0;
        foreach (var item in countys)
        {
            var countyname = "";
            switch (item.name)
            {
                case "460106":
                    countyname = "龙华区";
                    break;
                case "460105":
                    countyname = "秀英区";
                    break;
                case "460107":
                    countyname = "琼山区";
                    break;
                case "460108":
                    countyname = "美兰区";
                    break;
                default:
                    othersCnt += item.count;
                    countyname = "其他";
                    break;
            }
            if (countyname == "其他") continue;
            sw.WriteLine(countyname + ":" + item.count);
            basic_sw_csv.Write(countyname + "," + item.count + ",");
        }
        sw.WriteLine("其他" + ":" + othersCnt);
        basic_sw_csv.Write("其他" + "," + othersCnt + ",");
        basic_sw_csv.WriteLine();

        //起止坐标POI
        sw.WriteLine("起点坐标POI[starting_pois]:");
        basic_sw_csv.Write("starting_pois,");
        int poiCnt = 0;
        foreach (var poiItemName in new string[] { "机场", "火车站", "汽车站", "医院", "商圈", "学校", "景点" })
        {
            var SinglePoiCnt = orders.Count(x => x.starting.POI.Equals(poiItemName));
            sw.WriteLine(poiItemName + ":" + SinglePoiCnt);
            basic_sw_csv.Write(poiItemName + "," + SinglePoiCnt + ",");
            poiCnt += SinglePoiCnt;
        }
        basic_sw_csv.Write("其他" + "," + (orders.Count - poiCnt) + ",");
        basic_sw_csv.WriteLine();

        sw.WriteLine("终点坐标POI[dest_pois]:");
        basic_sw_csv.Write("dest_pois,");
        poiCnt = 0;
        foreach (var poiItemName in new string[] { "机场", "火车站", "汽车站", "医院", "商圈", "学校", "景点" })
        {
            var SinglePoiCnt = orders.Count(x => x.dest.POI.Equals(poiItemName));
            sw.WriteLine(poiItemName + ":" + SinglePoiCnt);
            basic_sw_csv.Write(poiItemName + "," + SinglePoiCnt + ",");
            poiCnt += SinglePoiCnt;
        }
        basic_sw_csv.Write("其他" + "," + (orders.Count - poiCnt) + ",");
        basic_sw_csv.WriteLine();



        //1-1.订单量 按照日期统计 
        //部分订单时间为 0000-00-00 这里按照最后的日期为依据
        var sw_csv = new StreamWriter(AfterProcessFolder + "diary_info.csv");
        var diaryinfos = orders.GroupBy(x => x.year.ToString("D4") + x.month.ToString("D2") + x.day.ToString("D2"))
                        .Select(x => (
                            name: x.Key, count: x.Count(),
                            distance: x.ToList().Sum(o => o.start_dest_distance_km),
                            normaltime: x.ToList().Sum(o => o.normal_time),
                            fee: x.ToList().Sum(o => o.pre_total_fee),
                            premier: x.Count(x => x.product_1level == Eproduct_1level.专车),
                            reserve: x.Count(x => x.order_type == Eorder_type.预约),
                            pickup: x.Count(x => x.traffic_type != Etraffic_type.未知),
                            //POI
                            airport: x.Count(x => x.starting.POI == "机场" || x.dest.POI == "机场"),
                            train: x.Count(x => x.starting.POI == "火车站" || x.dest.POI == "火车站"),
                            longbus: x.Count(x => x.starting.POI == "汽车站" || x.dest.POI == "汽车站"),
                            school: x.Count(x => x.starting.POI == "学校" || x.dest.POI == "学校"),
                            hospital: x.Count(x => x.starting.POI == "医院" || x.dest.POI == "医院"),
                            travel: x.Count(x => x.starting.POI == "景点" || x.dest.POI == "景点"),
                            //等车时间分类
                            waittime_1: x.Where(x => x.order_type == Eorder_type.实时).Count(x => x.WaitTime != -1 && x.WaitTime <= 5),
                            waittime_2: x.Where(x => x.order_type == Eorder_type.实时).Count(x => x.WaitTime > 5 && x.WaitTime <= 15),
                            waittime_3: x.Where(x => x.order_type == Eorder_type.实时).Count(x => x.WaitTime > 15 && x.WaitTime <= 30),
                            waittime_4: x.Where(x => x.order_type == Eorder_type.实时).Count(x => x.WaitTime > 30),
                            //距离分类
                            distance_1: x.Count(x => x.start_dest_distance_km <= 5),
                            distance_2: x.Count(x => x.start_dest_distance_km > 5 && x.start_dest_distance_km <= 10),
                            distance_3: x.Count(x => x.start_dest_distance_km > 10 && x.start_dest_distance_km <= 20),
                            distance_4: x.Count(x => x.start_dest_distance_km > 20),
                            //乘车时间分类
                            normaltime_1: x.Count(x => x.normal_time != 0 && x.normal_time <= 15),
                            normaltime_2: x.Count(x => x.normal_time > 15 && x.normal_time <= 30),
                            normaltime_3: x.Count(x => x.normal_time > 30 && x.normal_time <= 60),
                            normaltime_4: x.Count(x => x.normal_time > 60)
                        )).ToList();
        diaryinfos.Sort((x, y) => { return x.name.CompareTo(y.name); });
        foreach (var item in diaryinfos)
        {
            sw_csv.WriteLine(item.name + "," + item.count + "," + Math.Round(item.distance) + "," + item.normaltime + "," +
                             Math.Round(item.fee) + "," + item.premier + "," + item.reserve + "," + item.pickup + "," +
                             item.airport + "," + item.train + "," + item.longbus + "," + item.school + "," + item.hospital + "," +
                             item.waittime_1 + "," + item.waittime_2 + "," + item.waittime_3 + "," + item.waittime_4 + "," +
                             item.distance_1 + "," + item.distance_2 + "," + item.distance_3 + "," + item.distance_4 + "," +
                             item.normaltime_1 + "," + item.normaltime_2 + "," + item.normaltime_3 + "," + item.normaltime_4 + "," + item.travel
                             );
        }
        sw_csv.Close();

        var TotalDayCnt = diaryinfos.Count;

        basic_sw_csv.WriteLine("TotalDayCnt," + TotalDayCnt);
        basic_sw_csv.WriteLine("AvgOrderCntEveryDay," + TotalOrderCnt / TotalDayCnt);
        basic_sw_csv.WriteLine("AvgFeeEveryDay," + TotalFee / TotalDayCnt);
        basic_sw_csv.WriteLine("AvgDistanceKmEveryDay," + TotalDistanceKm / TotalDayCnt);
        basic_sw_csv.Flush();

        //2-1:对于时间段进行统计
        var weekday_hour_orderCnt = orders.GroupBy(x => x.departure_time.DayOfWeek.GetHashCode() + "|" +
                                                   x.departure_time.Hour.ToString("D2") + ":" + ((x.departure_time.Minute / 15) * 15).ToString("D2"))
                                          .Select(x => (name: x.Key, count: x.Count())).ToList();
        weekday_hour_orderCnt.Sort((x, y) => { return x.name.CompareTo(y.name); });
        sw_csv = new StreamWriter(AfterProcessFolder + "weekday_hour_orderCnt.csv");
        foreach (var item in weekday_hour_orderCnt)
        {
            sw_csv.WriteLine(item.name + "," + item.count);
        }
        sw_csv.Close();


        var diary_HourCnt = orders.GroupBy(x => x.departure_time.Date)
                                  .Select(x => (name: x.Key, count: x.Count())).ToList();
        diary_HourCnt.Sort((x, y) => { return x.name.CompareTo(y.name); });

        sw.WriteLine(DiaryProperty.GetTitle());
        foreach (var diary in diarys)
        {
            sw.WriteLine(diary.ToString());
        }

        sw.WriteLine("每个时间点的订单数:");
        foreach (var item in diary_HourCnt)
        {
            sw.WriteLine(item.name + ":" + item.count);
        }

        //3-1：出发和目的分析
        if (IsCreateGeoJson)
        {
            var Timer = new Stopwatch();
            Timer.Start();
            CreateGeoJson(true, "startlocs", 1000);
            CreateGeoJson(false, "destlocs", 1000);
            Console.WriteLine("CreateGeoJson Time Usage(Seconds):" + Timer.Elapsed.TotalSeconds);
        }

        //24小时分时起点终点坐标分析
        if (IsCreate24HoursGeoJson)
        {
            var Timer = new Stopwatch();
            Timer.Start();
            Create24HoursGeoJson();
            Timer.Stop();
            Console.WriteLine("Create24HoursGeoJson Time Usage(Seconds):" + Timer.Elapsed.TotalSeconds);
        }

        if (IsCreateWeekNoGeoJson)
        {
            var Timer = new Stopwatch();
            Timer.Start();
            CreateWeekNoGeoJson();
            Timer.Stop();
            Console.WriteLine("CreateWeekNoGeoJson Time Usage(Seconds):" + Timer.Elapsed.TotalSeconds);
        }

        //相同起点和终点的分析
        if (IsCreateTrace)
        {
            var Timer = new Stopwatch();
            Timer.Start();
            CreateSameSourceAndDest();
            Timer.Stop();
            Console.WriteLine("CreateSameSourceAndDest Time Usage(Seconds):" + Timer.Elapsed.TotalSeconds);

        }

        //8.对于里程数的统计
        basic_sw_csv.Write("Distance,");
        basic_sw_csv.Write("小于5公里," + orders.Count(x => x.start_dest_distance_km <= 5) + ",");
        basic_sw_csv.Write("5-10公里," + orders.Count(x => x.start_dest_distance_km > 5 && x.start_dest_distance_km <= 10) + ",");
        basic_sw_csv.Write("10-20公里," + orders.Count(x => x.start_dest_distance_km > 10 && x.start_dest_distance_km <= 20) + ",");
        basic_sw_csv.Write("大于20公里," + orders.Count(x => x.start_dest_distance_km > 20) + ",");
        basic_sw_csv.WriteLine();

        basic_sw_csv.Write("NormalTime,");
        basic_sw_csv.Write("小于15分钟," + orders.Count(x => x.normal_time != 0 && x.normal_time <= 15) + ",");
        basic_sw_csv.Write("15-30分钟," + orders.Count(x => x.normal_time > 15 && x.normal_time <= 30) + ",");
        basic_sw_csv.Write("30-60分钟," + orders.Count(x => x.normal_time > 30 && x.normal_time <= 60) + ",");
        basic_sw_csv.Write("大于60分钟," + orders.Count(x => x.normal_time > 60) + ",");
        basic_sw_csv.WriteLine();


        basic_sw_csv.Write("WaitTime,");
        basic_sw_csv.Write("小于5分钟," + orders.Where(x => x.order_type == Eorder_type.实时).Count(x => x.WaitTime != -1 && x.WaitTime <= 5) + ",");
        basic_sw_csv.Write("5-15分钟," + orders.Where(x => x.order_type == Eorder_type.实时).Count(x => x.WaitTime > 5 && x.WaitTime <= 15) + ",");
        basic_sw_csv.Write("15-30分钟," + orders.Where(x => x.order_type == Eorder_type.实时).Count(x => x.WaitTime > 15 && x.WaitTime <= 30) + ",");
        basic_sw_csv.Write("大于30分钟," + orders.Where(x => x.order_type == Eorder_type.实时).Count(x => x.WaitTime > 30) + ",");
        basic_sw_csv.WriteLine();

        //9 各种区分统计
        //9-0 产品线ID
        var product_ids = orders.GroupBy(x => x.product_id).Select(x => (name: x.Key, count: x.Count())).ToList();
        sw.WriteLine("产品线ID[product_ids]:");
        basic_sw_csv.Write("product_ids,");
        foreach (var item in product_ids)
        {
            sw.WriteLine(item.name + ":" + item.count);
            basic_sw_csv.Write(item.name + "," + item.count + ",");
        }
        basic_sw_csv.WriteLine();

        //9-1 订单时效
        var order_types = orders.GroupBy(x => x.order_type).Select(x => (name: x.Key, count: x.Count())).ToList();
        sw.WriteLine("订单时效[order_type]:");
        basic_sw_csv.Write("order_type,");
        foreach (var item in order_types)
        {
            sw.WriteLine(item.name + ":" + item.count);
            basic_sw_csv.Write(item.name + "," + item.count + ",");
        }
        basic_sw_csv.WriteLine();

        //9-2 订单类型
        var combo_types = orders.GroupBy(x => x.combo_type).Select(x => (name: x.Key, count: x.Count())).ToList();
        sw.WriteLine("订单类型[combo_type]:");
        basic_sw_csv.Write("combo_type,");
        foreach (var item in combo_types)
        {
            sw.WriteLine(item.name + ":" + item.count);
            basic_sw_csv.Write(item.name + "," + item.count + ",");
        }
        basic_sw_csv.WriteLine();


        //9-3 交通类型
        var traffic_types = orders.GroupBy(x => x.traffic_type).Select(x => (name: x.Key, count: x.Count())).ToList();
        sw.WriteLine("交通类型[traffic_types]:");
        basic_sw_csv.Write("traffic_types,");
        foreach (var item in traffic_types)
        {
            sw.WriteLine(item.name + ":" + item.count);
            basic_sw_csv.Write(item.name + "," + item.count + ",");
        }
        basic_sw_csv.WriteLine();


        //9-4 一级业务线
        var product_1levels = orders.GroupBy(x => x.product_1level).Select(x => (name: x.Key, count: x.Count())).ToList();
        sw.WriteLine("一级业务线[product_1levels]:");
        basic_sw_csv.Write("product_1levels,");
        foreach (var item in product_1levels)
        {
            sw.WriteLine(item.name + ":" + item.count);
            basic_sw_csv.Write(item.name + "," + item.count + ",");
        }
        basic_sw_csv.WriteLine();


        basic_sw_csv.Close();
        sw.Close();
        orders.Clear();
        GC.Collect();
    }

    private static void CreateSameSourceAndDest()
    {
        //以1_000_000 为单位进行Groupby，然后汇总
        var TotalCnt = orders.Count();
        var TimeCnt = TotalCnt / 1_000_000;
        TimeCnt += 1;
        var MapReduceDictionary = new ConcurrentDictionary<(Geo source, Geo dest), int>();
        for (int i = 0; i < TimeCnt; i++)
        {
            Console.WriteLine("Time:" + i + "Start");
            var points = orders.Skip(i * 1_000_000).Take(1_000_000).GroupBy(x => x.Trace).Select(x => (coord: x.Key, value: x.Count())).ToList();

            Parallel.ForEach(points, (item, loop) =>
            {
                if (MapReduceDictionary.ContainsKey(item.coord))
                {
                    MapReduceDictionary[item.coord] += item.value;
                }
                else
                {
                    MapReduceDictionary.TryAdd(item.coord, item.value);
                }
            });
        }

        var result = MapReduceDictionary.Select(x => (coord: x.Key, Value: x.Value)).ToList();
        Console.WriteLine("Start Sort");
        result.Sort((x, y) => { return y.Value - x.Value; });
        result = result.Take(500).ToList();
        var json = new StreamWriter(AngularJsonAssetsFolder + "trace.json");
        int Cnt = 0;
        json.WriteLine("[");
        foreach (var item in result)
        {
            if (Cnt != 0) json.WriteLine(",");
            Cnt++;
            json.Write("[[" + item.coord.source.lng + "," + item.coord.source.lat + "],[" + item.coord.dest.lng + "," + item.coord.dest.lat + "]]");
        }
        json.WriteLine();
        json.WriteLine("]");
        json.Close();
        GC.Collect();
    }

    /// <summary>
    /// 坐标点的GroupBy
    /// </summary>
    /// <param name="isStart"></param>
    /// <param name="filename"></param>
    /// <param name="downlimit"></param>
    private static void CreateGeoJson(bool isStart, string filename, int downlimit = -1)
    {
        //以1_000_000 为单位进行Groupby，然后汇总
        var TotalCnt = orders.Count();
        var TimeCnt = TotalCnt / 1_000_000;
        TimeCnt += 1;
        var MapReduceDictionary = new ConcurrentDictionary<Geo, int>();
        for (int i = 0; i < TimeCnt; i++)
        {
            Console.WriteLine("Time:" + i + "Start");
            var points = orders.Skip(i * 1_000_000).Take(1_000_000).GroupBy(x => isStart ? x.starting : x.dest)
                                                   .Select(x => (coord: x.Key, value: x.Count())).ToList();
            Parallel.ForEach(points, (item, loop) =>
            {
                if (MapReduceDictionary.ContainsKey(item.coord))
                {
                    MapReduceDictionary[item.coord] += item.value;
                }
                else
                {
                    MapReduceDictionary.TryAdd(item.coord, item.value);
                }
            });
        }
        var result = MapReduceDictionary.Select(x => (coord: x.Key, Value: x.Value)).ToList();
        Console.WriteLine("Start Sort");
        result.Sort((x, y) => { return y.Value - x.Value; });
        var json = new StreamWriter(AngularJsonAssetsFolder + filename + "_PointSize.json");
        int Cnt = 0;
        json.WriteLine("[");
        foreach (var item in result)
        {
            if (item.Value > downlimit || downlimit == -1)
            {
                var point = item.coord;
                if (Cnt != 0) json.WriteLine(",");
                Cnt++;
                json.Write(" {\"name\": \"" + point.POI + Cnt + "\", \"value\": ");
                json.Write("[" + point.lng + "," + point.lat + "," + item.Value + "]}");
            }
        }
        json.WriteLine();
        json.WriteLine("]");
        json.Close();
        GC.Collect();
    }

    private static void Create24HoursGeoJson()
    {

        var json = new StreamWriter(AngularJsonAssetsFolder + "startlocs_24h_PointSize.json");
        int Cnt = 0;
        json.WriteLine("[");
        //按照小时计算地点
        for (int hour = 0; hour < 24; hour++)
        {
            var startlocs_hour = orders.Where(x => x.departure_time.Hour == hour)
                                       .GroupBy(x => x.starting).Select(x => (point: x.Key, count: x.Count())).ToList();
            startlocs_hour.Sort((x, y) => { return y.count - x.count; });
            startlocs_hour = startlocs_hour.Take(300).ToList();
            foreach (var item in startlocs_hour)
            {
                var point = item.point;
                if (Cnt != 0) json.WriteLine(",");
                Cnt++;
                json.Write(" {\"hour\":" + hour + ",\"name\": \"" + point.POI + Cnt + "\", \"value\": ");
                json.Write("[" + point.lng + "," + point.lat + "," + item.count + "]}");
            }
        }
        json.WriteLine();
        json.WriteLine("]");
        json.Close();


        json = new StreamWriter(AngularJsonAssetsFolder + "destlocs_24h_PointSize.json");
        Cnt = 0;
        json.WriteLine("[");
        //按照小时计算地点
        for (int hour = 0; hour < 24; hour++)
        {
            var destlocs_hour = orders.Where(x => x.departure_time.Hour == hour)
                                       .GroupBy(x => x.dest).Select(x => (point: x.Key, count: x.Count())).ToList();
            destlocs_hour.Sort((x, y) => { return y.count - x.count; });
            destlocs_hour = destlocs_hour.Take(300).ToList();
            foreach (var item in destlocs_hour)
            {
                var point = item.point;
                if (Cnt != 0) json.WriteLine(",");
                Cnt++;
                json.Write(" {\"hour\":" + hour + ",\"name\": \"" + point.POI + Cnt + "\", \"value\": ");
                json.Write("[" + point.lng + "," + point.lat + "," + item.count + "]}");
            }
        }
        json.WriteLine();
        json.WriteLine("]");
        json.Close();
        GC.Collect();
    }

    private static void CreateWeekNoGeoJson()
    {
        var weekNoList = orders.Select(x => x.WeekNo).Distinct().ToList();


        var json = new StreamWriter(AngularJsonAssetsFolder + "startlocs_weekno_PointSize.json");
        int Cnt = 0;
        json.WriteLine("[");
        //按照周次计算地点
        foreach (var weekno in weekNoList)
        {
            var startlocs_weekno = orders.Where(x => x.WeekNo == weekno)
                                       .GroupBy(x => x.starting).Select(x => (point: x.Key, count: x.Count())).ToList();
            startlocs_weekno.Sort((x, y) => { return y.count - x.count; });
            startlocs_weekno = startlocs_weekno.Take(300).ToList();
            foreach (var item in startlocs_weekno)
            {
                var point = item.point;
                if (Cnt != 0) json.WriteLine(",");
                Cnt++;
                json.Write(" {\"weekno\":" + weekno + ",\"name\": \"" + point.POI + Cnt + "\", \"value\": ");
                json.Write("[" + point.lng + "," + point.lat + "," + item.count + "]}");
            }
        }
        json.WriteLine();
        json.WriteLine("]");
        json.Close();


        json = new StreamWriter(AngularJsonAssetsFolder + "destlocs_weekno_PointSize.json");
        Cnt = 0;
        json.WriteLine("[");
        //按照周次计算地点
        foreach (var weekno in weekNoList)
        {
            var destlocs_weekno = orders.Where(x => x.WeekNo == weekno)
                                       .GroupBy(x => x.dest).Select(x => (point: x.Key, count: x.Count())).ToList();
            destlocs_weekno.Sort((x, y) => { return y.count - x.count; });
            destlocs_weekno = destlocs_weekno.Take(300).ToList();
            foreach (var item in destlocs_weekno)
            {
                var point = item.point;
                if (Cnt != 0) json.WriteLine(",");
                Cnt++;
                json.Write(" {\"weekno\":" + weekno + ",\"name\": \"" + point.POI + Cnt + "\", \"value\": ");
                json.Write("[" + point.lng + "," + point.lat + "," + item.count + "]}");
            }
        }
        json.WriteLine();
        json.WriteLine("]");
        json.Close();
        GC.Collect();
    }

}

/// <summary>
/// 每日属性
/// </summary>
public class DiaryProperty
{
    public string Date { get; set; }

    /// <summary>
    /// 订单总量
    /// </summary>
    /// /// <value></value>
    public int OrderCnt { get; set; }
    /// <summary>
    /// 营业额总量
    /// </summary>
    /// <value></value>
    public Single TotalFee { get; set; }

    /// <summary>
    /// 平均费用
    /// </summary>
    /// <value></value>
    public Single AvgFee
    {
        get
        {
            return TotalFee / OrderCnt;
        }
    }
    public static string GetTitle()
    {
        return "日期,订单数,总费用,每单费用";
    }
    public override String ToString()
    {
        return Date + "," + OrderCnt + "," + TotalFee + "," + AvgFee;
    }

}

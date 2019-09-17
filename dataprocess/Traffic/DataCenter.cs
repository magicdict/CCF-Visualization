using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using static OrderDetails;

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

    /// <summary>
    /// EDA
    /// </summary>
    public static void EDA()
    {
        //基本信息CSV
        var basic_sw_csv = new StreamWriter(AfterProcessFolder + "basic_info.csv");

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

        //1-1.订单量 按照日期统计 
        //部分订单时间为 0000-00-00 这里按照最后的日期为依据
        var sw_csv = new StreamWriter(AfterProcessFolder + "diary_orderCnt.csv");
        var diaryinfos = orders.GroupBy(x => x.year.ToString("D4") + x.month.ToString("D2") + x.day.ToString("D2"))
                          .Select(x => (name: x.Key, count: x.Count(), distance: x.ToList().Sum(o => o.start_dest_distance_km), fee: x.ToList().Sum(o => o.pre_total_fee))).ToList();
        diaryinfos.Sort((x, y) => { return x.name.CompareTo(y.name); });
        foreach (var item in diaryinfos)
        {
            sw_csv.WriteLine(item.name + "," + item.count + "," + Math.Round(item.distance) + "," + Math.Round(item.fee));
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
        var sw = new StreamWriter(EDAFile);
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
        var startlocs = orders.GroupBy(x => x.starting).Select(x => (point: x.Key, count: x.Count())).ToList();
        CreateGeoJson("startlocs", startlocs, 1000);
        var destlocs = orders.GroupBy(x => x.dest).Select(x => (point: x.Key, count: x.Count())).ToList();
        CreateGeoJson("destlocs", destlocs, 1000);
        sw.WriteLine("Start Loc Count:" + startlocs.Count);
        sw.WriteLine("Dest  Loc Count:" + destlocs.Count);
        //24小时分时起点终点坐标分析
        Create24HoursGeoJson();
        //相同起点和终点的分析
        CreateSameSourceAndDest();

        //8.对于里程数的统计
        basic_sw_csv.Write("Distance,");
        basic_sw_csv.Write("小于5公里," + orders.Count(x => x.start_dest_distance_km <= 5) + ",");
        basic_sw_csv.Write("5-10公里," + orders.Count(x => x.start_dest_distance_km > 5 && x.start_dest_distance_km <= 10) + ",");
        basic_sw_csv.Write("10-20公里," + orders.Count(x => x.start_dest_distance_km > 10 && x.start_dest_distance_km <= 20) + ",");
        basic_sw_csv.Write("大于20公里," + orders.Count(x => x.start_dest_distance_km > 20) + ",");
        basic_sw_csv.WriteLine();

        basic_sw_csv.Write("Time,");
        basic_sw_csv.Write("小于15分钟," + orders.Count(x => x.normal_time != 0 && x.normal_time <= 15) + ",");
        basic_sw_csv.Write("15-30分钟," + orders.Count(x => x.normal_time > 15 && x.normal_time <= 30) + ",");
        basic_sw_csv.Write("30-60分钟," + orders.Count(x => x.normal_time > 30 && x.normal_time <= 60) + ",");
        basic_sw_csv.Write("大于60分钟," + orders.Count(x => x.normal_time > 60) + ",");
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
        var points = orders.GroupBy(x => x.Trace).Select(x => new { coord = x.Key, Value = x.Count() }).ToList();
        points.Sort((x, y) => { return y.Value - x.Value; });
        points = points.Take(500).ToList();
        var json = new StreamWriter(AngularJsonAssetsFolder + "trace.json");
        int Cnt = 0;
        json.WriteLine("[");
        foreach (var item in points)
        {
            if (Cnt != 0) json.WriteLine(",");
            Cnt++;
            json.Write("[[" + item.coord.source.lng + "," +  item.coord.source.lat + "],[" + item.coord.dest.lng + "," +  item.coord.dest.lat + "]]");
        }
        json.WriteLine();
        json.WriteLine("]");
        json.Close();
    }

    private static void CreateGeoJson(string filename, List<(OrderDetails.Geo point, System.Int32 count)> points, int downlimit = -1)
    {
        var json = new StreamWriter(AngularJsonAssetsFolder + filename + "_PointSize.json");
        int Cnt = 0;
        json.WriteLine("[");
        foreach (var item in points)
        {
            if (item.count > downlimit || downlimit == -1)
            {
                var point = item.point;
                var poi = GetPOI(point);
                if (Cnt != 0) json.WriteLine(",");
                Cnt++;
                json.Write(" {\"name\": \"" + poi + Cnt + "\", \"value\": ");
                json.Write("[" + point.lng + "," + point.lat + "," + item.count + "]}");
            }
        }
        json.WriteLine();
        json.WriteLine("]");
        json.Close();
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
            startlocs_hour = startlocs_hour.Take(200).ToList();
            foreach (var item in startlocs_hour)
            {
                var point = item.point;
                var poi = GetPOI(point);
                if (Cnt != 0) json.WriteLine(",");
                Cnt++;
                json.Write(" {\"hour\":" + hour + ",\"name\": \"" + poi + Cnt + "\", \"value\": ");
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
            var startlocs_hour = orders.Where(x => x.departure_time.Hour == hour)
                                       .GroupBy(x => x.dest).Select(x => (point: x.Key, count: x.Count())).ToList();
            startlocs_hour.Sort((x, y) => { return y.count - x.count; });
            startlocs_hour = startlocs_hour.Take(200).ToList();
            foreach (var item in startlocs_hour)
            {
                var point = item.point;
                var poi = GetPOI(point);
                if (Cnt != 0) json.WriteLine(",");
                Cnt++;
                json.Write(" {\"hour\":" + hour + ",\"name\": \"" + poi + Cnt + "\", \"value\": ");
                json.Write("[" + point.lng + "," + point.lat + "," + item.count + "]}");
            }
        }
        json.WriteLine();
        json.WriteLine("]");
        json.Close();
    }

    /// <summary>
    /// 坐标转换和POI取得
    /// </summary>
    /// <param name="Point"></param>
    /// <returns></returns>
    public static string GetPOI(Geo point)
    {
        //美兰机场
        if (point.lng >= 110.4560 && point.lng <= 110.4875 && point.lat >= 19.9420 && point.lat <= 19.9480) return "机场";
        //火车站东站
        if (point.lng >= 110.3507 - 0.002 && point.lng <= 110.3507 + 0.002 && point.lat >= 19.9892 - 0.002 && point.lat <= 19.9892 + 0.002) return "火车站";
        //汽车站
        if (point.lng >= 110.2962 - 0.001 && point.lng <= 110.2962 + 0.001 && point.lat >= 20.0189 - 0.001 && point.lat <= 20.0189 + 0.001) return "汽车站";
        //医院
        if (point.lng >= 110.2933 - 0.001 && point.lng <= 110.2933 + 0.001 && point.lat >= 20.013 - 0.001 && point.lat <= 20.013 + 0.001) return "医院";
        //日月广场商圈
        if (point.lng >= 110.355 - 0.001 && point.lng <= 110.355 + 0.001 && point.lat >= 20.0217 - 0.001 && point.lat <= 20.0217 + 0.001) return "商圈";
        if (point.lng >= 110.3284 - 0.001 && point.lng <= 110.3284 + 0.001 && point.lat >= 20.0268 - 0.001 && point.lat <= 20.0268 + 0.001) return "商圈";
        if (point.lng >= 110.3488 - 0.001 && point.lng <= 110.3488 + 0.001 && point.lat >= 20.0359 - 0.001 && point.lat <= 20.0359 + 0.001) return "商圈";
        return "海口";
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

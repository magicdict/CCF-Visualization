using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public static class DataCenterForDidi
{

    public const string DataFolder = @"F:\CCF-Visualization\RawData\海口市-交通流量时空演变特征可视分析";
    public const string EDAFile = @"F:\CCF-Visualization\dataprocess\AfterProcess\海口市-交通流量时空演变特征可视分析\EDA.log";

    public const string AfterProcessFolder = @"F:\CCF-Visualization\dataprocess\AfterProcess\海口市-交通流量时空演变特征可视分析\";


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

        //部分订单时间为 0000-00-00 这里按照最后的日期为依据

        //1-1.订单量 按照日期统计 
        var diary_orderCnt = orders.GroupBy(x => x.year.ToString("D4") + x.month.ToString("D2") + x.day.ToString("D2"))
                          .Select(x => (name: x.Key, count: x.Count())).ToList();
        diary_orderCnt.Sort((x, y) => { return x.name.CompareTo(y.name); });
        var sw_csv = new StreamWriter(AfterProcessFolder + "diary_orderCnt.csv");
        foreach (var item in diary_orderCnt)
        {
            sw_csv.WriteLine(item.name + "," + item.count);
        }
        sw_csv.Close();

        var weekday_hour_orderCnt = orders.GroupBy(x => x.departure_time.DayOfWeek.GetHashCode() + "|" + x.departure_time.Hour.ToString("D2") + ":" + ((x.departure_time.Minute / 15) * 15).ToString("D2"))
                                          .Select(x => (name: x.Key, count: x.Count())).ToList();
        weekday_hour_orderCnt.Sort((x, y) => { return x.name.CompareTo(y.name); });
        sw_csv = new StreamWriter(AfterProcessFolder + "weekday_hour_orderCnt.csv");
        foreach (var item in weekday_hour_orderCnt)
        {
            sw_csv.WriteLine(item.name + "," + item.count);
        }
        sw_csv.Close();


        //1-2.总费用 按照日期统计
        var diary_Fee = orders.GroupBy(x => x.year.ToString("D4") + x.month.ToString("D2") + x.day.ToString("D2"))
                          .Select(x => (name: x.Key, sum: x.ToList().Sum(o => o.pre_total_fee))).ToList();
        diary_Fee.Sort((x, y) => { return x.name.CompareTo(y.name); });


        //每日统计结果    
        for (int i = 0; i < diary_orderCnt.Count; i++)
        {
            diarys.Add(new DiaryProperty()
            {
                Date = diary_orderCnt[i].name,
                OrderCnt = diary_orderCnt[i].count,
                TotalFee = diary_Fee[i].sum
            });
        }


        //2-1:对于时间段进行统计
        var diary_HourCnt = orders.GroupBy(x => x.departure_time.Date)
                                  .Select(x => (name: x.Key, count: x.Count())).ToList();
        diary_HourCnt.Sort((x, y) => { return x.name.CompareTo(y.name); });

        //出发位置
        var startlocs = orders.GroupBy(x => x.starting).Select(x => (point: x.Key, count: x.Count())).ToList();
        var destlocs = orders.GroupBy(x => x.dest).Select(x => (point: x.Key, count: x.Count())).ToList();

        CreateGeoJson("startlocs", startlocs);
        CreateGeoJson("destlocs", destlocs);

        startlocs.Sort((x, y) =>
        {
            if (x.point.lat == y.point.lat)
            {
                return x.point.lng.CompareTo(y.point.lng);
            }
            else
            {
                return x.point.lat.CompareTo(y.point.lat);
            }
        });

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

        sw.WriteLine("Start Loc Count:" + startlocs.Count);
        sw.WriteLine("Dest  Loc Count:" + destlocs.Count);


        //9 各种区分统计
        //9-0 产品线ID
        var product_ids = orders.GroupBy(x => x.product_id).Select(x => (name: x.Key, count: x.Count())).ToList();
        sw.WriteLine("产品线ID[product_ids]:");
        foreach (var item in product_ids)
        {
            sw.WriteLine(item.name + ":" + item.count);
        }

        //9-1 订单时效
        var order_types = orders.GroupBy(x => x.order_type).Select(x => (name: x.Key, count: x.Count())).ToList();
        sw.WriteLine("订单时效[order_type]:");
        foreach (var item in order_types)
        {
            sw.WriteLine(item.name + ":" + item.count);
        }

        //9-2 订单类型
        var combo_types = orders.GroupBy(x => x.combo_type).Select(x => (name: x.Key, count: x.Count())).ToList();
        sw.WriteLine("订单类型[combo_type]:");
        foreach (var item in combo_types)
        {
            sw.WriteLine(item.name + ":" + item.count);
        }

        //9-3 交通类型
        var traffic_types = orders.GroupBy(x => x.traffic_type).Select(x => (name: x.Key, count: x.Count())).ToList();
        sw.WriteLine("交通类型[traffic_types]:");
        foreach (var item in traffic_types)
        {
            sw.WriteLine(item.name + ":" + item.count);
        }

        //9-4 一级业务线
        var product_1levels = orders.GroupBy(x => x.product_1level).Select(x => (name: x.Key, count: x.Count())).ToList();
        sw.WriteLine("一级业务线[product_1levels]:");
        foreach (var item in product_1levels)
        {
            sw.WriteLine(item.name + ":" + item.count);
        }


        sw.Close();
        orders.Clear();
        GC.Collect();
    }

    const double baiduOffsetlng = 0.0063;
    const double baiduOffsetlat = 0.0058;


    private static void CreateGeoJson(string filename, List<(OrderDetails.Geo point, System.Int32 count)> points)
    {
        var json = new StreamWriter(AfterProcessFolder + filename + "_PointSize.json");
        int Cnt = 0;
        json.WriteLine("[");
        foreach (var item in points)
        {
            var radus = Math.Min(100, item.count / 100);
            if (radus > 5)
            {
                if (Cnt != 0) json.WriteLine(",");
                Cnt++;
                json.Write(" {\"name\": \"海口" + Cnt + "\", \"value\": " );
                json.Write("[" + Math.Round(item.point.lng + baiduOffsetlng, 4)
                                                + "," + Math.Round(item.point.lat + baiduOffsetlat, 4) + "," + radus + "]}");
            }
        }
        json.WriteLine();
        json.WriteLine("]");
        json.Close();
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

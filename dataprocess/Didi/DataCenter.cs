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

        CreateMap(startlocs);

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
        sw.Close();




        orders.Clear();
        GC.Collect();
    }


    private static void CreateMap(List<(OrderDetails.Geo point, System.Int32 count)> points)
    {

        var circle = "<circle cx=\"{{x}}\" cy=\"{{y}}\" r=\"{{r}}\" stroke=\"black\" stroke-width=\"2\" fill=\"red\"/>";
        var sw = new StreamWriter(AfterProcessFolder + "map.svg");
        var json = new StreamWriter(AfterProcessFolder + "PointSize.json");
        var json2 = new StreamWriter(AfterProcessFolder + "PointLoc.json");

        //Header
        sw.WriteLine("<?xml version=\"1.0\" standalone=\"no\"?>");
        sw.WriteLine("<!DOCTYPE svg PUBLIC \"-//W3C//DTD SVG 1.1//EN\"");
        sw.WriteLine("\"http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd\">");


        //经纬度最大差距的计算
        var Maxlng = points.Max(x => x.point.lng);      //经度：东西向
        var Minlng = points.Min(x => x.point.lng);
        var Maxlat = points.Max(x => x.point.lat);      //纬度：南北向
        var Minlat = points.Min(x => x.point.lat);
        var lngDiff = Maxlng - Minlng;                  //最大经度差
        var latDiff = Maxlat - Minlat;                  //最大纬度差
        //SVG实际宽度
        var svgwidth = 10000;
        var svgheight = 10000;
        var ScaleRatelng = svgwidth / lngDiff;
        var ScaleRatelat = svgheight / latDiff;
        var baiduOffsetlng = 0.0063;
        var baiduOffsetlat = 0.0058;

        sw.WriteLine("<svg width=\"10040\" height=\"10040\" version=\"1.1\"");
        sw.WriteLine("xmlns=\"http://www.w3.org/2000/svg\">");
        int Cnt = 0;
        foreach (var item in points)
        {
            var x = (item.point.lng - Minlng) * ScaleRatelng + 20;
            var y = (item.point.lat - Minlat) * ScaleRatelng + 20;
            //Y坐标反转
            y = 10000 - y;

            var radus = Math.Min(100, item.count / 100);
            sw.WriteLine(
                circle.Replace("{{x}}", x.ToString()).Replace("{{y}}", y.ToString()).Replace("{{r}}", radus.ToString())
            );

            if (radus > 5)
            {
                Cnt++;
                json.WriteLine(" {name: '海口" + Cnt + "', value: " + radus + "},");
                json2.WriteLine("'海口" + Cnt + "':[" + Math.Round(item.point.lng + baiduOffsetlng, 4)
                                                + "," + Math.Round(item.point.lat + baiduOffsetlat, 4) + "],");
            }

        }
        sw.WriteLine("</svg>");

        json.Close();
        json2.Close();
        sw.Close();
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

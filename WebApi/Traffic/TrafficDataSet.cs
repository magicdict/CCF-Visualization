using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public static class TrafficDataSet
{
    /// <summary>
    /// 不同协议通信次数
    /// </summary>
    /// <returns></returns>
    //public static List<NameValueSet<int>> weekday_hour_orderCnt = new List<NameValueSet<int>>();
    public static Dictionary<string, Weather> weathers = new Dictionary<string, Weather>();
    public static List<NameValueSet<AggeInfo>> diaryinfos = new List<NameValueSet<AggeInfo>>();
    public static List<NameValueSet<AggeInfo>> weeklyinfos = new List<NameValueSet<AggeInfo>>();

    public static string Folder = @"F:\CCF-Visualization\dataprocess\AfterProcess\海口市-交通流量时空演变特征可视分析\";
    //public static string Folder = @"/root/HelloChinaApi/AfterProcess/海口市-交通流量时空演变特征可视分析/";


    public static void LoadData()
    {
        var sr = new StreamReader(Folder + "海口历史天气数据.csv");
        while (!sr.EndOfStream)
        {
            var info = sr.ReadLine().Split("\t");
            weathers.Add(
                info[0].Replace("年", string.Empty).Replace("月", string.Empty).Replace("日", string.Empty),
                new Weather() { Description = info[1], Tempera = info[2], Wind = info[3] }
            );
        }
        sr.Close();

        sr = new StreamReader(Folder + "diary_info.csv");
        while (!sr.EndOfStream)
        {
            var info = sr.ReadLine().Split(",");
            diaryinfos.Add(new NameValueSet<AggeInfo>()
            {
                Name = DateTime.ParseExact(info[0], "yyyyMMdd", null).ToString("yyyy/MM/dd"), // d 的时候，操作系统的语言不通，结果也不同。这里强制定义格式
                Value = new AggeInfo()
                {
                    holiday = GetHoliday(info[0]),
                    isWorkday = IsWorkDay(info[0]),
                    weather = weathers[info[0]],
                    Weekno = GetWeekNo(info[0]),

                    ordercnt = int.Parse(info[1]),
                    distance = int.Parse(info[2]),
                    normaltime = int.Parse(info[3]),
                    fee = int.Parse(info[4]),

                    premier = int.Parse(info[5]),
                    reserve = int.Parse(info[6]),
                    pickup = int.Parse(info[7]),

                    airport = int.Parse(info[8]),
                    train = int.Parse(info[9]),
                    longbus = int.Parse(info[10]),
                    school = int.Parse(info[11]),
                    hospital = int.Parse(info[12]),

                    waittime_1 = int.Parse(info[13]),
                    waittime_2 = int.Parse(info[14]),
                    waittime_3 = int.Parse(info[15]),
                    waittime_4 = int.Parse(info[16]),

                    distance_1 = int.Parse(info[17]),
                    distance_2 = int.Parse(info[18]),
                    distance_3 = int.Parse(info[19]),
                    distance_4 = int.Parse(info[20]),

                    normaltime_1 = int.Parse(info[21]),
                    normaltime_2 = int.Parse(info[22]),
                    normaltime_3 = int.Parse(info[23]),
                    normaltime_4 = int.Parse(info[24]),

                    travel = int.Parse(info[25]),
                    cbd = int.Parse(info[26])
                }
            });
        }
        sr.Close();

        weeklyinfos = diaryinfos.GroupBy(x => x.Value.Weekno).Select(
            x => new NameValueSet<AggeInfo>()
            {
                Name = x.Key,
                Value = new AggeInfo()
                {
                    ordercnt = x.Sum(x => x.Value.ordercnt),
                    distance = x.Sum(x => x.Value.distance),
                    normaltime = x.Sum(x => x.Value.normaltime),
                    fee = x.Sum(x => x.Value.fee),

                    premier = x.Sum(x => x.Value.premier),
                    reserve = x.Sum(x => x.Value.reserve),
                    pickup = x.Sum(x => x.Value.pickup),

                    airport = x.Sum(x => x.Value.airport),
                    train = x.Sum(x => x.Value.train),
                    travel = x.Sum(x => x.Value.travel),
                    longbus = x.Sum(x => x.Value.longbus),
                    school = x.Sum(x => x.Value.school),
                    hospital = x.Sum(x => x.Value.hospital),
                    cbd = x.Sum(x => x.Value.cbd),

                    waittime_1 = x.Sum(x => x.Value.waittime_1),
                    waittime_2 = x.Sum(x => x.Value.waittime_2),
                    waittime_3 = x.Sum(x => x.Value.waittime_3),
                    waittime_4 = x.Sum(x => x.Value.waittime_4),

                    distance_1 = x.Sum(x => x.Value.distance_1),
                    distance_2 = x.Sum(x => x.Value.distance_2),
                    distance_3 = x.Sum(x => x.Value.distance_3),
                    distance_4 = x.Sum(x => x.Value.distance_4),

                    normaltime_1 = x.Sum(x => x.Value.normaltime_1),
                    normaltime_2 = x.Sum(x => x.Value.normaltime_2),
                    normaltime_3 = x.Sum(x => x.Value.normaltime_3),
                    normaltime_4 = x.Sum(x => x.Value.normaltime_4),
                }
            }
        ).ToList();
        //去掉10月30日的数据点，统计数据严重不足，无意义
        weeklyinfos.Remove(weeklyinfos.Last());
    }

    static string GetHoliday(string date)
    {
        switch (date)
        {
            case "20170501": return "劳动节";
            case "20170520": return "我爱你";
            case "20170530": return "端午节";
            case "20170828": return "七夕节";
            case "20171001": return "国庆节";
            case "20171004": return "中秋节";
        }
        return "";
    }

    static bool IsWorkDay(string date)
    {
        switch (date)
        {
            case "20170501": return false;  //劳动节
            case "20170527": return true;
            case "20170530": return false;  //端午节
            case "20170930": return true;
            case "20171001": return false;  //国庆节
            case "20171002": return false;
            case "20171003": return false;
            case "20171004": return false;
            case "20171005": return false;
            case "20171006": return false;
            case "20171007": return false;
            case "20171008": return false;
        }
        var d = DateTime.ParseExact(date, "yyyyMMdd", null);
        return d.DayOfWeek != DayOfWeek.Saturday && d.DayOfWeek != DayOfWeek.Sunday;
    }


    static string GetWeekNo(string date)
    {
        var d = DateTime.ParseExact(date, "yyyyMMdd", null);
        var startard = DateTime.ParseExact("20170501", "yyyyMMdd", null);
        var diff = d.Subtract(startard);
        var weekidx = (int)diff.TotalDays / 7;
        return startard.AddDays(weekidx * 7).ToString("yyyy/MM/dd");
    }
}


/// <summary>
/// 周信息
/// </summary>
public class AggeInfo
{
    public Weather weather { get; set; }

    public string Weekno { get; set; }

    public int ordercnt { get; set; }

    public int distance { get; set; }

    public int normaltime { get; set; }

    public int fee { get; set; }

    public string holiday { get; set; }

    public bool isWorkday { get; set; }


    public int premier { get; set; }

    public int reserve { get; set; }

    public int pickup { get; set; }

    public int airport { get; set; }

    public int train { get; set; }

    public int longbus { get; set; }

    public int school { get; set; }

    public int hospital { get; set; }

    public int travel { get; set; }


    public int cbd { get; set; }

    public int waittime_1 { get; set; }
    public int waittime_2 { get; set; }
    public int waittime_3 { get; set; }
    public int waittime_4 { get; set; }

    public int distance_1 { get; set; }
    public int distance_2 { get; set; }
    public int distance_3 { get; set; }
    public int distance_4 { get; set; }

    public int normaltime_1 { get; set; }
    public int normaltime_2 { get; set; }
    public int normaltime_3 { get; set; }
    public int normaltime_4 { get; set; }
}

/// <summary>
/// 天气数据
/// </summary>
public class Weather
{
    public string Description { get; set; }

    public string Tempera { get; set; }

    public string Wind { get; set; }
}

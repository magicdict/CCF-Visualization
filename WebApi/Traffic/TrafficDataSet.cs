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
    public static List<NameValueSet<int>> weekday_hour_orderCnt = new List<NameValueSet<int>>();
    public static Dictionary<string, Weather> weathers = new Dictionary<string, Weather>();
    public static List<NameValueSet<DiaryInfo>> diaryinfos = new List<NameValueSet<DiaryInfo>>();
    public static List<NameValueSet<WeeklyInfo>> weeklyinfos = new List<NameValueSet<WeeklyInfo>>();

    //public static string Folder = @"F:\CCF-Visualization\dataprocess\AfterProcess\海口市-交通流量时空演变特征可视分析\";
    public static string Folder = @"/root/HelloChinaApi/AfterProcess/海口市-交通流量时空演变特征可视分析/";


    public static void LoadData()
    {
        //协议统计的加载

        var sr = new StreamReader(Folder + "weekday_hour_orderCnt.csv");
        while (!sr.EndOfStream)
        {
            var info = sr.ReadLine().Split(",");
            weekday_hour_orderCnt.Add(new NameValueSet<int>() { Name = info[0], Value = int.Parse(info[1]) });
        }
        sr.Close();

        sr = new StreamReader(Folder + "海口历史天气数据.csv");
        while (!sr.EndOfStream)
        {
            var info = sr.ReadLine().Split("\t");
            weathers.Add(
                info[0].Replace("年", string.Empty).Replace("月", string.Empty).Replace("日", string.Empty),
                new Weather() { Description = info[1], Tempera = info[2], Wind = info[3] }
            );
        }
        sr.Close();

        sr = new StreamReader(Folder + "diary_orderCnt.csv");
        while (!sr.EndOfStream)
        {
            var info = sr.ReadLine().Split(",");
            diaryinfos.Add(new NameValueSet<DiaryInfo>()
            {
                Name = DateTime.ParseExact(info[0],"yyyyMMdd",null).ToString("d"),
                Value = new DiaryInfo()
                {
                    holiday = GetHoliday(info[0]),
                    isWorkday = IsWorkDay(info[0]),
                    ordercnt = int.Parse(info[1]),
                    distance = int.Parse(info[2]),
                    fee = int.Parse(info[3]),
                    weather = weathers[info[0]],
                    Weekno = GetWeekNo(info[0])
                }
            });
        }
        sr.Close();

        weeklyinfos = diaryinfos.GroupBy(x => x.Value.Weekno).Select(
            x => new NameValueSet<WeeklyInfo>()
            {
                Name = x.Key,
                Value = new WeeklyInfo()
                {
                    ordercnt = x.Sum(x => x.Value.ordercnt),
                    distance = x.Sum(x => x.Value.distance),
                    fee = x.Sum(x => x.Value.fee)
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


public class DiaryInfo
{
    public Weather weather { get; set; }

    public int ordercnt { get; set; }

    public int distance { get; set; }

    public int fee { get; set; }

    public string holiday { get; set; }

    public bool isWorkday { get; set; }

    public string Weekno { get; set; }
}

/// <summary>
/// 周信息
/// </summary>
public class WeeklyInfo
{
    public int ordercnt { get; set; }

    public int distance { get; set; }

    public int fee { get; set; }

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

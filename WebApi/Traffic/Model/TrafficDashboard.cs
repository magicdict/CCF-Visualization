using System.Collections.Generic;
using System.IO;

public class TrafficDashBoard
{
    public List<NameValueSet<WeeklyInfo>> weeklyinfos = new List<NameValueSet<WeeklyInfo>>();

    public int TotalOrderCnt;
    public int TotalFee;
    public double AvgFeePerOrder;
    public int TotalDistanceKm;
    public double AvgDistanceKmPerOrder;
    public double FeePerKm;
    public int TotalDayCnt;
    public int AvgOrderCntEveryDay;
    public int AvgFeeEveryDay;
    public int AvgDistanceKmEveryDay;

    /// <summary>
    /// 2017年海口人数
    /// </summary>
    public int Population = 227_0000;
    /// <summary>
    /// 过夜游客数（万人）
    /// </summary>
    /// <value></value>
    public double[] traveller = new double[] { 115.66, 138.08, 149.83, 158.99, 166.09, 209.31 };


    /// <summary>
    /// 产品线
    /// </summary>
    /// <returns></returns>
    public List<NameValueSet<int>> product_ids = new List<NameValueSet<int>>();

    public List<NameValueSet<int>> order_type = new List<NameValueSet<int>>();

    public List<NameValueSet<int>> traffic_types = new List<NameValueSet<int>>();

    public List<NameValueSet<int>> product_1levels = new List<NameValueSet<int>>();

    public List<NameValueSet<int>> Distance = new List<NameValueSet<int>>();

    public List<NameValueSet<int>> Time = new List<NameValueSet<int>>();


    public List<NameValueSet<double>> TravellerCnt = new List<NameValueSet<double>>();


    public TrafficDashBoard()
    {
        weeklyinfos = TrafficDataSet.weeklyinfos;
        for (int i = 5; i < 11; i++)
        {
            TravellerCnt.Add(new NameValueSet<double>() { Name = i + "月", Value = traveller[i - 5] });
        }

        var sr = new StreamReader(TrafficDataSet.Folder + "basic_info.csv");
        while (!sr.EndOfStream)
        {
            var info = sr.ReadLine().Split(",");
            switch (info[0])
            {
                case nameof(TotalOrderCnt):
                    TotalOrderCnt = int.Parse(info[1]);
                    break;
                case nameof(TotalFee):
                    TotalFee = int.Parse(info[1]);
                    break;
                case nameof(AvgFeePerOrder):
                    AvgFeePerOrder = double.Parse(info[1]);
                    break;
                case nameof(TotalDistanceKm):
                    TotalDistanceKm = int.Parse(info[1]);
                    break;
                case nameof(AvgDistanceKmPerOrder):
                    AvgDistanceKmPerOrder = double.Parse(info[1]);
                    break;
                case nameof(FeePerKm):
                    FeePerKm = double.Parse(info[1]);
                    break;
                case nameof(TotalDayCnt):
                    TotalDayCnt = int.Parse(info[1]);
                    break;
                case nameof(AvgOrderCntEveryDay):
                    AvgOrderCntEveryDay = int.Parse(info[1]);
                    break;
                case nameof(AvgFeeEveryDay):
                    AvgFeeEveryDay = int.Parse(info[1]);
                    break;
                case nameof(AvgDistanceKmEveryDay):
                    AvgDistanceKmEveryDay = int.Parse(info[1]);
                    break;
                case nameof(product_ids):
                    for (int i = 1; i < info.Length - 1; i += 2)
                    {
                        product_ids.Add(new NameValueSet<int>() { Name = info[i], Value = int.Parse(info[i + 1]) });
                    }
                    break;
                case nameof(order_type):
                    for (int i = 1; i < info.Length - 1; i += 2)
                    {
                        order_type.Add(new NameValueSet<int>() { Name = info[i], Value = int.Parse(info[i + 1]) });
                    }
                    break;
                case nameof(traffic_types):
                    for (int i = 1; i < info.Length - 1; i += 2)
                    {
                        traffic_types.Add(new NameValueSet<int>() { Name = info[i], Value = int.Parse(info[i + 1]) });
                    }
                    break;
                case nameof(product_1levels):
                    for (int i = 1; i < info.Length - 1; i += 2)
                    {
                        product_1levels.Add(new NameValueSet<int>() { Name = info[i], Value = int.Parse(info[i + 1]) });
                    }
                    break;
                case nameof(Time):
                    for (int i = 1; i < info.Length - 1; i += 2)
                    {
                        Time.Add(new NameValueSet<int>() { Name = info[i], Value = int.Parse(info[i + 1]) });
                    }
                    break;
                case nameof(Distance):
                    for (int i = 1; i < info.Length - 1; i += 2)
                    {
                        Distance.Add(new NameValueSet<int>() { Name = info[i], Value = int.Parse(info[i + 1]) });
                    }
                    break;

            }
        }
        sr.Close();

    }
}
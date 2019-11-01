using System.Collections.Generic;
using System.IO;

public class TrafficDashBoard
{
    public List<NameValueSet<AggeInfo>> weeklyinfos = new List<NameValueSet<AggeInfo>>();

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

    public List<NameValueSet<int>> countys = new List<NameValueSet<int>>();

    public List<NameValueSet<int>> product_1levels = new List<NameValueSet<int>>();

    public List<NameValueSet<int>> starting_pois = new List<NameValueSet<int>>();

    public List<NameValueSet<int>> dest_pois = new List<NameValueSet<int>>();

    public List<NameValueSet<int>> Distance = new List<NameValueSet<int>>();

    public List<NameValueSet<int>> NormalTime = new List<NameValueSet<int>>();

    public List<NameValueSet<int>> WaitTime = new List<NameValueSet<int>>();
    public List<NameValueSet<double>> TravellerCnt = new List<NameValueSet<double>>();

    public List<NameValueSet<int>> speed = new List<NameValueSet<int>>();

    public List<NameValueSet<int>> distance_km = new List<NameValueSet<int>>();

    public List<NameValueSet<int>> weekday_hour_speed = new List<NameValueSet<int>>();

    public List<NameValueSet<int>> weekday_hour_speed_airport = new List<NameValueSet<int>>();

    public List<NameValueSet<int>> weekday_hour_speed_train = new List<NameValueSet<int>>();

    public List<NameValueSet<int>> weekday_hour_speed_longbus = new List<NameValueSet<int>>();

    public List<NameValueSet<int>> weekday_hour_speed_cbd = new List<NameValueSet<int>>();

    public List<NameValueSet<int>> weekday_hour_speed_hospital = new List<NameValueSet<int>>();

    public List<NameValueSet<int>> weekday_hour_speed_school = new List<NameValueSet<int>>();

    public List<NameValueSet<int>> weekday_hour_speed_travel = new List<NameValueSet<int>>();

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
                case nameof(NormalTime):
                    for (int i = 1; i < info.Length - 1; i += 2)
                    {
                        NormalTime.Add(new NameValueSet<int>() { Name = info[i], Value = int.Parse(info[i + 1]) });
                    }
                    break;
                case nameof(WaitTime):
                    for (int i = 1; i < info.Length - 1; i += 2)
                    {
                        WaitTime.Add(new NameValueSet<int>() { Name = info[i], Value = int.Parse(info[i + 1]) });
                    }
                    break;
                case nameof(Distance):
                    for (int i = 1; i < info.Length - 1; i += 2)
                    {
                        Distance.Add(new NameValueSet<int>() { Name = info[i], Value = int.Parse(info[i + 1]) });
                    }
                    break;
                case nameof(countys):
                    for (int i = 1; i < info.Length - 1; i += 2)
                    {
                        countys.Add(new NameValueSet<int>() { Name = info[i], Value = int.Parse(info[i + 1]) });
                    }
                    break;
                case nameof(starting_pois):
                    for (int i = 1; i < info.Length - 1; i += 2)
                    {
                        starting_pois.Add(new NameValueSet<int>() { Name = info[i], Value = int.Parse(info[i + 1]) });
                    }
                    break;
                case nameof(dest_pois):
                    for (int i = 1; i < info.Length - 1; i += 2)
                    {
                        dest_pois.Add(new NameValueSet<int>() { Name = info[i], Value = int.Parse(info[i + 1]) });
                    }
                    break;
                case nameof(speed):
                    for (int i = 1; i < info.Length - 1; i += 2)
                    {
                        speed.Add(new NameValueSet<int>() { Name = info[i], Value = int.Parse(info[i + 1]) });
                    }
                    break;
                case nameof(distance_km):
                    for (int i = 1; i < info.Length - 1; i += 2)
                    {
                        distance_km.Add(new NameValueSet<int>() { Name = info[i], Value = int.Parse(info[i + 1]) });
                    }
                    break;
            }
        }
        sr.Close();


        sr = new StreamReader(TrafficDataSet.Folder + "weekday_hour_speed.csv");
        while (!sr.EndOfStream)
        {
            var info = sr.ReadLine().Split(",");
            weekday_hour_speed.Add(new NameValueSet<int>() { Name = info[0], Value = (int)double.Parse(info[1]) });
        }
        sr.Close();

        sr = new StreamReader(TrafficDataSet.Folder + "weekday_hour_speed_airport.csv");
        while (!sr.EndOfStream)
        {
            var info = sr.ReadLine().Split(",");
            weekday_hour_speed_airport.Add(new NameValueSet<int>() { Name = info[0], Value = (int)double.Parse(info[1]) });
        }
        sr.Close();

        sr = new StreamReader(TrafficDataSet.Folder + "weekday_hour_speed_train.csv");
        while (!sr.EndOfStream)
        {
            var info = sr.ReadLine().Split(",");
            weekday_hour_speed_train.Add(new NameValueSet<int>() { Name = info[0], Value = (int)double.Parse(info[1]) });
        }
        sr.Close();


        sr = new StreamReader(TrafficDataSet.Folder + "weekday_hour_speed_longbus.csv");
        while (!sr.EndOfStream)
        {
            var info = sr.ReadLine().Split(",");
            weekday_hour_speed_longbus.Add(new NameValueSet<int>() { Name = info[0], Value = (int)double.Parse(info[1]) });
        }
        sr.Close();

        sr = new StreamReader(TrafficDataSet.Folder + "weekday_hour_speed_cbd.csv");
        while (!sr.EndOfStream)
        {
            var info = sr.ReadLine().Split(",");
            weekday_hour_speed_cbd.Add(new NameValueSet<int>() { Name = info[0], Value = (int)double.Parse(info[1]) });
        }
        sr.Close();


        sr = new StreamReader(TrafficDataSet.Folder + "weekday_hour_speed_hospital.csv");
        while (!sr.EndOfStream)
        {
            var info = sr.ReadLine().Split(",");
            weekday_hour_speed_hospital.Add(new NameValueSet<int>() { Name = info[0], Value = (int)double.Parse(info[1]) });
        }
        sr.Close();

        sr = new StreamReader(TrafficDataSet.Folder + "weekday_hour_speed_school.csv");
        while (!sr.EndOfStream)
        {
            var info = sr.ReadLine().Split(",");
            weekday_hour_speed_school.Add(new NameValueSet<int>() { Name = info[0], Value = (int)double.Parse(info[1]) });
        }
        sr.Close();

        sr = new StreamReader(TrafficDataSet.Folder + "weekday_hour_speed_travel.csv");
        while (!sr.EndOfStream)
        {
            var info = sr.ReadLine().Split(",");
            weekday_hour_speed_travel.Add(new NameValueSet<int>() { Name = info[0], Value = (int)double.Parse(info[1]) });
        }
        sr.Close();

    }
}
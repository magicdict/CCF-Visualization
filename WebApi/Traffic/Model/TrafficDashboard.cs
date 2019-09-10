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

    public TrafficDashBoard()
    {
        weeklyinfos = TrafficDataSet.weeklyinfos;

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
            }
        }
        sr.Close();

    }
}
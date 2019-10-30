using System.Collections.Generic;
using System.IO;

public class TrafficTimeAnalysis
{
    public List<NameValueSet<int>> weekday_hour_orderCnt = new List<NameValueSet<int>>();

    public List<NameValueSet<int>> weekday_hour_orderCnt_airport = new List<NameValueSet<int>>();

    public List<NameValueSet<int>> weekday_hour_orderCnt_train = new List<NameValueSet<int>>();

    public List<NameValueSet<int>> weekday_hour_orderCnt_longbus = new List<NameValueSet<int>>();

    public List<NameValueSet<int>> weekday_hour_orderCnt_cbd = new List<NameValueSet<int>>();

    public List<NameValueSet<int>> weekday_hour_orderCnt_hospital = new List<NameValueSet<int>>();

    public List<NameValueSet<int>> weekday_hour_orderCnt_school = new List<NameValueSet<int>>();

    public List<NameValueSet<int>> weekday_hour_orderCnt_travel = new List<NameValueSet<int>>();


    public TrafficTimeAnalysis()
    {
        var sr = new StreamReader(TrafficDataSet.Folder + "weekday_hour_orderCnt.csv");
        while (!sr.EndOfStream)
        {
            var info = sr.ReadLine().Split(",");
            weekday_hour_orderCnt.Add(new NameValueSet<int>() { Name = info[0], Value = int.Parse(info[1]) });
        }
        sr.Close();

        sr = new StreamReader(TrafficDataSet.Folder + "weekday_hour_orderCnt_airport.csv");
        while (!sr.EndOfStream)
        {
            var info = sr.ReadLine().Split(",");
            weekday_hour_orderCnt_airport.Add(new NameValueSet<int>() { Name = info[0], Value = int.Parse(info[1]) });
        }
        sr.Close();

        sr = new StreamReader(TrafficDataSet.Folder + "weekday_hour_orderCnt_train.csv");
        while (!sr.EndOfStream)
        {
            var info = sr.ReadLine().Split(",");
            weekday_hour_orderCnt_train.Add(new NameValueSet<int>() { Name = info[0], Value = int.Parse(info[1]) });
        }
        sr.Close();


        sr = new StreamReader(TrafficDataSet.Folder + "weekday_hour_orderCnt_longbus.csv");
        while (!sr.EndOfStream)
        {
            var info = sr.ReadLine().Split(",");
            weekday_hour_orderCnt_longbus.Add(new NameValueSet<int>() { Name = info[0], Value = int.Parse(info[1]) });
        }
        sr.Close();

        sr = new StreamReader(TrafficDataSet.Folder + "weekday_hour_orderCnt_cbd.csv");
        while (!sr.EndOfStream)
        {
            var info = sr.ReadLine().Split(",");
            weekday_hour_orderCnt_cbd.Add(new NameValueSet<int>() { Name = info[0], Value = int.Parse(info[1]) });
        }
        sr.Close();


        sr = new StreamReader(TrafficDataSet.Folder + "weekday_hour_orderCnt_hospital.csv");
        while (!sr.EndOfStream)
        {
            var info = sr.ReadLine().Split(",");
            weekday_hour_orderCnt_hospital.Add(new NameValueSet<int>() { Name = info[0], Value = int.Parse(info[1]) });
        }
        sr.Close();

        sr = new StreamReader(TrafficDataSet.Folder + "weekday_hour_orderCnt_school.csv");
        while (!sr.EndOfStream)
        {
            var info = sr.ReadLine().Split(",");
            weekday_hour_orderCnt_school.Add(new NameValueSet<int>() { Name = info[0], Value = int.Parse(info[1]) });
        }
        sr.Close();

        sr = new StreamReader(TrafficDataSet.Folder + "weekday_hour_orderCnt_travel.csv");
        while (!sr.EndOfStream)
        {
            var info = sr.ReadLine().Split(",");
            weekday_hour_orderCnt_travel.Add(new NameValueSet<int>() { Name = info[0], Value = int.Parse(info[1]) });
        }
        sr.Close();
    }
}
using System.Collections.Generic;

public class TrafficTimeAnalysis
{
    public List<NameValueSet<int>> weekday_hour_orderCnt = new List<NameValueSet<int>>();

    public TrafficTimeAnalysis()
    {
        weekday_hour_orderCnt = TrafficDataSet.weekday_hour_orderCnt;
    }
}
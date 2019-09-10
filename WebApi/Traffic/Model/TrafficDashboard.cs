using System.Collections.Generic;

public class TrafficDashBoard
{
    public List<NameValueSet<WeeklyInfo>> weeklyinfos = new List<NameValueSet<WeeklyInfo>>();

    public TrafficDashBoard()
    {
        weeklyinfos = TrafficDataSet.weeklyinfos;
    }
}
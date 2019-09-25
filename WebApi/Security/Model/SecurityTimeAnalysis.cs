using System.Collections.Generic;
using System.Linq;
using System;

public class SecurityTimeAnalysis
{
    public Dictionary<string, List<NameValueSet<int>>> Protocols_Hours = new Dictionary<string, List<NameValueSet<int>>>();
    public Dictionary<string, List<NameValueSet<int>>> Protocols_Hours_Traffic = new Dictionary<string, List<NameValueSet<int>>>();
    public List<NameValueSet<int>> traffic_hours_everyday = new List<NameValueSet<int>>();

    public SecurityTimeAnalysis()
    {
        var p = SecurityDataSet.Protocols_Hours.GroupBy(x => x.Name.Split("|")[0]);
        foreach (var item in p)
        {
            Protocols_Hours.Add(item.Key, item.Select(x => new NameValueSet<int>() { Name = x.Name.Split("|")[1], Value = x.Value }).ToList());
        }

        p = SecurityDataSet.Protocols_Hours_Traffic.GroupBy(x => x.Name.Split("|")[0]);
        foreach (var item in p)
        {
            Protocols_Hours_Traffic.Add(item.Key, item.Select(x => new NameValueSet<int>() { Name = x.Name.Split("|")[1], Value = x.Value }).ToList());
        }
        traffic_hours_everyday = SecurityDataSet.traffic_hours_everyday;
    }
}
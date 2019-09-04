using System.Collections.Generic;
using System.Linq;

public class DashBoard
{
    public List<NameValueSet<int>> Protocols = new List<NameValueSet<int>>();
    public List<NameValueSet<int>> Hours = new List<NameValueSet<int>>();
    public Dictionary<string, List<NameValueSet<int>>> Protocols_Hours = new Dictionary<string, List<NameValueSet<int>>>();

    /// <summary>
    /// 上下行比例
    /// </summary>
    /// <returns></returns>
    public List<NameValueSet<double>> up_down_rate = new List<NameValueSet<double>>();
    

    public DashBoard()
    {
        //协议
        Protocols = SecurityDataSet.Protocols.Take(9).ToList();
        //将TOP9之后的协议都放到其他里面
        Protocols.Add(new NameValueSet<int>() { Name = "Others", Value = SecurityDataSet.Protocols.Sum(x => x.Value) - Protocols.Sum(x => x.Value) });
        Hours = SecurityDataSet.Hours;
        var p = SecurityDataSet.Protocols_Hours.GroupBy(x => x.Name.Split("|")[0]);
        foreach (var item in p)
        {
            Protocols_Hours.Add(item.Key, item.Select(x => new NameValueSet<int>() { Name = x.Name.Split("|")[1], Value = x.Value }).ToList());
        }
    }
}
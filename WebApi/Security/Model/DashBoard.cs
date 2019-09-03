using System.Collections.Generic;
using System.Linq;

public class DashBoard
{
    public List<NameValueSet<int>> Protocols = new List<NameValueSet<int>>();
    public List<NameValueSet<int>> Hours = new List<NameValueSet<int>>();
    public DashBoard()
    {
        //协议
        Protocols = SecurityDataSet.Protocols.Take(9).ToList();
        //将TOP9之后的协议都放到其他里面
        Protocols.Add(new NameValueSet<int>() { Name = "Others", Value = SecurityDataSet.Protocols.Sum(x => x.Value) - Protocols.Sum(x => x.Value) });
        Hours = SecurityDataSet.Hours;
    }
}
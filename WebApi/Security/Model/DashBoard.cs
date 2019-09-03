using System.Collections.Generic;

public class DashBoard
{
    public List<NameValueSet<int>> Protocols = new List<NameValueSet<int>>();
    public List<NameValueSet<int>> Hours = new List<NameValueSet<int>>();
    public DashBoard()
    {
        Protocols = SecurityDataSet.Protocols;
        Hours = SecurityDataSet.Hours;
    }
}
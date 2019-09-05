using System.Collections.Generic;
using System.Linq;
using System;

public class DashBoard
{

    public long RecordCnt = 0;
    public long ProtocolCnt = 0;
    public long SourceDistIpCnt = 0;
    public long SourceIpCnt = 0;
    public long DistIpCnt = 0;
    public long downlink_length = 0;
    public long uplink_length = 0;

    public long SourceSegmentCnt = 0;
    public long SourceTotalLanCnt = 0;
    public long DestSegmentCnt = 0;
    public long DestTotalLanCnt = 0;


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
        //基本信息
        RecordCnt = SecurityDataSet.BasicInfo.Where(x => x.Name == nameof(RecordCnt)).First().Value;
        ProtocolCnt = SecurityDataSet.BasicInfo.Where(x => x.Name == nameof(ProtocolCnt)).First().Value;
        SourceDistIpCnt = SecurityDataSet.BasicInfo.Where(x => x.Name == nameof(SourceDistIpCnt)).First().Value;
        SourceIpCnt = SecurityDataSet.BasicInfo.Where(x => x.Name == nameof(SourceIpCnt)).First().Value;
        DistIpCnt = SecurityDataSet.BasicInfo.Where(x => x.Name == nameof(DistIpCnt)).First().Value;
        downlink_length = SecurityDataSet.BasicInfo.Where(x => x.Name == nameof(downlink_length)).First().Value;
        uplink_length = SecurityDataSet.BasicInfo.Where(x => x.Name == nameof(uplink_length)).First().Value;

        SourceSegmentCnt = SecurityDataSet.BasicInfo.Where(x => x.Name == nameof(SourceSegmentCnt)).First().Value;
        SourceTotalLanCnt = SecurityDataSet.BasicInfo.Where(x => x.Name == nameof(SourceTotalLanCnt)).First().Value;
        DestSegmentCnt = SecurityDataSet.BasicInfo.Where(x => x.Name == nameof(DestSegmentCnt)).First().Value;
        DestTotalLanCnt = SecurityDataSet.BasicInfo.Where(x => x.Name == nameof(DestTotalLanCnt)).First().Value;


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
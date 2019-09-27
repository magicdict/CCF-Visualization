using System.Collections.Generic;
using System.IO;
using System.Linq;

public class SecurityDashBoard
{

    public static List<NameValueSet<long>> BasicInfo = new List<NameValueSet<long>>();
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

    public List<NameValueSet<int>> protocols = new List<NameValueSet<int>>();
    public List<NameValueSet<int>> ports = new List<NameValueSet<int>>();
    /// <summary>
    /// 上下行比例
    /// </summary>
    /// <returns></returns>
    public List<NameValueSet<double>> up_down_rate = new List<NameValueSet<double>>();
    public List<NameValueSet<int>> traffic_hours_today = new List<NameValueSet<int>>();
    public List<NameValueSet<int>> traffic_hours_last1days = new List<NameValueSet<int>>();
    public List<NameValueSet<int>> traffic_hours_last3days = new List<NameValueSet<int>>();

    public List<NameValueSet<int>> access_hours_today = new List<NameValueSet<int>>();
    public List<NameValueSet<int>> access_hours_last1days = new List<NameValueSet<int>>();
    public List<NameValueSet<int>> access_hours_last3days = new List<NameValueSet<int>>();

    public List<NameValueSet<int>> source_dist = new List<NameValueSet<int>>();
    public List<NameValueSet<int>> source = new List<NameValueSet<int>>();
    public List<NameValueSet<int>> dist = new List<NameValueSet<int>>();

    public SecurityDashBoard()
    {

        var sr = new StreamReader(SecurityDataSet.Folder + "basic_info.csv");
        while (!sr.EndOfStream)
        {
            var info = sr.ReadLine().Split(",");
            switch (info[0])
            {
                case nameof(SecurityDashBoard.downlink_length):
                    BasicInfo.Add(new NameValueSet<long>() { Name = info[0], Value = (long)double.Parse(info[1]) });
                    break;
                case nameof(SecurityDashBoard.uplink_length):
                    BasicInfo.Add(new NameValueSet<long>() { Name = info[0], Value = (long)double.Parse(info[1]) });
                    break;
                case nameof(protocols):
                    for (int i = 1; i < info.Length - 1; i += 2)
                    {
                        protocols.Add(new NameValueSet<int>() { Name = info[i], Value = int.Parse(info[i + 1]) });
                    }
                    break;
                case nameof(ports):
                    for (int i = 1; i < info.Length - 1; i += 2)
                    {
                        ports.Add(new NameValueSet<int>() { Name = info[i], Value = int.Parse(info[i + 1]) });
                    }
                    break;
                case nameof(traffic_hours_last1days):
                    for (int i = 1; i < info.Length - 1; i += 2)
                    {
                        traffic_hours_last1days.Add(new NameValueSet<int>() { Name = info[i], Value = (int)double.Parse(info[i + 1]) });
                    }
                    break;
                case nameof(traffic_hours_last3days):
                    for (int i = 1; i < info.Length - 1; i += 2)
                    {
                        traffic_hours_last3days.Add(new NameValueSet<int>() { Name = info[i], Value = (int)double.Parse(info[i + 1]) });
                    }
                    break;
                case nameof(traffic_hours_today):
                    for (int i = 1; i < info.Length - 1; i += 2)
                    {
                        traffic_hours_today.Add(new NameValueSet<int>() { Name = info[i], Value = (int)double.Parse(info[i + 1]) });
                    }
                    break;
                case nameof(access_hours_last1days):
                    for (int i = 1; i < info.Length - 1; i += 2)
                    {
                        access_hours_last1days.Add(new NameValueSet<int>() { Name = info[i], Value = (int)double.Parse(info[i + 1]) });
                    }
                    break;
                case nameof(access_hours_last3days):
                    for (int i = 1; i < info.Length - 1; i += 2)
                    {
                        access_hours_last3days.Add(new NameValueSet<int>() { Name = info[i], Value = (int)double.Parse(info[i + 1]) });
                    }
                    break;
                case nameof(access_hours_today):
                    for (int i = 1; i < info.Length - 1; i += 2)
                    {
                        access_hours_today.Add(new NameValueSet<int>() { Name = info[i], Value = (int)double.Parse(info[i + 1]) });
                    }
                    break;
                case nameof(source):
                    for (int i = 1; i < info.Length - 1; i += 2)
                    {
                        source.Add(new NameValueSet<int>() { Name = info[i], Value = int.Parse(info[i + 1]) });
                    }
                    break;

                case nameof(dist):
                    for (int i = 1; i < info.Length - 1; i += 2)
                    {
                        dist.Add(new NameValueSet<int>() { Name = info[i], Value = int.Parse(info[i + 1]) });
                    }
                    break;

                case nameof(source_dist):
                    for (int i = 1; i < info.Length - 1; i += 2)
                    {
                        source_dist.Add(new NameValueSet<int>() { Name = info[i], Value = int.Parse(info[i + 1]) });
                    }
                    break;
                default:
                    if (info.Length == 2) BasicInfo.Add(new NameValueSet<long>() { Name = info[0], Value = long.Parse(info[1]) });
                    break;
            }
        }
        sr.Close();


        //基本信息
        RecordCnt = BasicInfo.Where(x => x.Name == nameof(RecordCnt)).First().Value;
        ProtocolCnt = BasicInfo.Where(x => x.Name == nameof(ProtocolCnt)).First().Value;
        SourceDistIpCnt = BasicInfo.Where(x => x.Name == nameof(SourceDistIpCnt)).First().Value;
        SourceIpCnt = BasicInfo.Where(x => x.Name == nameof(SourceIpCnt)).First().Value;
        DistIpCnt = BasicInfo.Where(x => x.Name == nameof(DistIpCnt)).First().Value;
        downlink_length = BasicInfo.Where(x => x.Name == nameof(downlink_length)).First().Value;
        uplink_length = BasicInfo.Where(x => x.Name == nameof(uplink_length)).First().Value;
        SourceSegmentCnt = BasicInfo.Where(x => x.Name == nameof(SourceSegmentCnt)).First().Value;
        SourceTotalLanCnt = BasicInfo.Where(x => x.Name == nameof(SourceTotalLanCnt)).First().Value;
        DestSegmentCnt = BasicInfo.Where(x => x.Name == nameof(DestSegmentCnt)).First().Value;
        DestTotalLanCnt = BasicInfo.Where(x => x.Name == nameof(DestTotalLanCnt)).First().Value;
        //协议
        protocols = protocols.Take(9).ToList();
        //将TOP9之后的协议都放到其他里面
        protocols.Add(new NameValueSet<int>() { Name = "Others", Value = (int)RecordCnt - protocols.Sum(x => x.Value) });

        ports = ports.Take(9).ToList();
        //将TOP9之后的协议都放到其他里面
        ports.Add(new NameValueSet<int>() { Name = "Others", Value = (int)RecordCnt - ports.Sum(x => x.Value) });

    }
}
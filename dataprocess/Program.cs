using System.Diagnostics;
using System;
namespace dataprocess
{
    class Program
    {
        static void Main(string[] args)
        {
            var IsTraffic = true;
            if (IsTraffic)
            {
                if (!System.IO.Directory.Exists(@"F:\CCF-Visualization\RawData\"))
                {
                    //172.16.2.121运行时候设定的目录
                    DataCenterForTraffic.DataFolder = DataCenterForTraffic.DataFolder.Replace("F:", @"D:\share\CPU Test");
                    DataCenterForTraffic.EDAFile = DataCenterForTraffic.EDAFile.Replace("F:", @"D:\share\CPU Test");
                    DataCenterForTraffic.AfterProcessFolder = DataCenterForTraffic.AfterProcessFolder.Replace("F:", @"D:\share\CPU Test");
                    DataCenterForTraffic.AngularJsonAssetsFolder = DataCenterForTraffic.AngularJsonAssetsFolder.Replace("F:", @"D:\share\CPU Test");
                }
                DataCenterForTraffic.Load(-1);
                DataCenterForTraffic.CreateWeedDayTime((x) => { return x.starting.POI == "机场"; }, "weekday_hour_orderCnt_机场.csv");
                DataCenterForTraffic.CreateWeedDayTime((x) => { return x.starting.POI == "火车站"; }, "weekday_hour_orderCnt_火车站.csv");
                DataCenterForTraffic.CreateWeedDayTime((x) => { return x.starting.POI == "汽车站"; }, "weekday_hour_orderCnt_汽车站.csv");
                DataCenterForTraffic.CreateWeedDayTime((x) => { return x.starting.POI == "医院"; }, "weekday_hour_orderCnt_医院.csv");
                DataCenterForTraffic.CreateWeedDayTime((x) => { return x.starting.POI == "商圈"; }, "weekday_hour_orderCnt_商圈.csv");
                DataCenterForTraffic.CreateWeedDayTime((x) => { return x.starting.POI == "学校"; }, "weekday_hour_orderCnt_学校.csv");
                DataCenterForTraffic.CreateWeedDayTime((x) => { return x.starting.POI == "景点"; }, "weekday_hour_orderCnt_景点.csv");
                //DataCenterForTraffic.GetLongWait();
                //DataCenterForTraffic.GetHotPointAttr();
                //DataCenterForTraffic.Load(2_000_000);
                //相同起点和终点的分析(耗时)
                //DataCenterForTraffic.IsCreateTrace = true;
                //DataCenterForTraffic.IsCreate24HoursGeoJson = true;
                //DataCenterForTraffic.IsCreateWeekNoGeoJson = true;
                //DataCenterForTraffic.IsCreateGeoJson = true;
                //DataCenterForTraffic.EDA();
            }
            else
            {
                DataCenterForSecurity.Load(-1);
                //DataCenterForSecurity.Load(1_000_000);
                //DataCenterForSecurity.Load(100_000);
                //DataCenterForSecurity.GetProtocolProfile("ftp_control");
                //DataCenterForSecurity.GetProtocolProfile("ssl");
                //DataCenterForSecurity.GetProtocolProfile("http");
                //DataCenterForSecurity.GetProtocolProfile("http_proxy");
                //DataCenterForSecurity.Protocol_Port();

                //DataCenterForSecurity.CreateSourceIpTreeJson();
                //DataCenterForSecurity.CreateDistIpTreeJson();
                //DataCenterForSecurity.CommunicationMode();
                //DataCenterForSecurity.EDA();
            }
        }
    }
}

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
                DataCenterForTraffic.Load(-1);
                //DataCenterForTraffic.Load(2_000_000);
                //相同起点和终点的分析(耗时)
                DataCenterForTraffic.IsCreateTrace = true;
                //DataCenterForTraffic.IsCreate24HoursGeoJson = true;
                //DataCenterForTraffic.IsCreateWeekNoGeoJson = true;
                //DataCenterForTraffic.IsCreateGeoJson = true;
                DataCenterForTraffic.EDA();
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

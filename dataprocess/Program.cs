using System.Linq;

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
                    DataCenterForTraffic.ExtendFile = DataCenterForTraffic.ExtendFile.Replace("F:", @"D:\share\CPU Test");
                }
                //DataCenterForTraffic.LoadDestconuty();
                DataCenterForTraffic.Load(-1);
                //DataCenterForTraffic.Conuty();
                //DataCenterForTraffic.CreateCountyDict();
                //DataCenterForTraffic.CreateDistrictDict();
                //DataCenterForTraffic.LoadExtendInfo();
                //终点为机场的起点集合
                DataCenterForTraffic.CreateGeoJson(DataCenterForTraffic.orders.Where(x=>x.dest.POI.Equals("机场")).ToList(),true,"StartPoint4AirportDest",500);
                DataCenterForTraffic.CreateGeoJson(DataCenterForTraffic.orders.Where(x=>x.starting.POI.Equals("机场")).ToList(),false,"DestPoint4AirportStart",500);
                DataCenterForTraffic.CreateGeoJson(DataCenterForTraffic.orders.Where(x=>x.dest.POI.Equals("火车站")).ToList(),true,"StartPoint4TrainDest",500);
                DataCenterForTraffic.CreateGeoJson(DataCenterForTraffic.orders.Where(x=>x.starting.POI.Equals("火车站")).ToList(),false,"DestPoint4TrainStart",500);
            }
            else
            {
                if (!System.IO.Directory.Exists(@"F:\CCF-Visualization\RawData\"))
                {
                    //172.16.2.121运行时候设定的目录
                    DataCenterForSecuritySemi.RawDataFolder = DataCenterForSecuritySemi.RawDataFolder.Replace("F:", @"D:\share\CPU Test");
                    DataCenterForSecuritySemi.EDAFile = DataCenterForSecuritySemi.EDAFile.Replace("F:", @"D:\share\CPU Test");
                    DataCenterForSecuritySemi.AfterProcessFolder = DataCenterForSecuritySemi.AfterProcessFolder.Replace("F:", @"D:\share\CPU Test");
                    DataCenterForSecuritySemi.AngularJsonAssetsFolder = DataCenterForSecuritySemi.AngularJsonAssetsFolder.Replace("F:", @"D:\share\CPU Test");

                    DataCenterForSecurity.EDAFile = DataCenterForSecurity.EDAFile.Replace("F:", @"D:\share\CPU Test");
                    DataCenterForSecurity.AfterProcessFolder = DataCenterForSecurity.AfterProcessFolder.Replace("F:", @"D:\share\CPU Test");
                    DataCenterForSecurity.AngularJsonAssetsFolder = DataCenterForSecurity.AngularJsonAssetsFolder.Replace("F:", @"D:\share\CPU Test");
                }
                DataCenterForSecurity.RawDataFolder = DataCenterForSecuritySemi.RawDataFolder + "\\tcpflow";
                DataCenterForSecurity.Load(2500_0000);
                DataCenterForSecurity.GetProtocolProfile("ftp_control");
                DataCenterForSecurity.GetProtocolProfile("ssl");
                DataCenterForSecurity.GetProtocolProfile("http");
                DataCenterForSecurity.GetProtocolProfile("http_proxy");
                DataCenterForSecurity.Protocol_Port();
                DataCenterForSecurity.CreateSourceIpTreeJson();
                DataCenterForSecurity.CreateDistIpTreeJson();
                DataCenterForSecurity.CommunicationMode();
                DataCenterForSecurity.EDA();
            }
        }
    }
}

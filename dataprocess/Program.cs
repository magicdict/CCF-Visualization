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
                DataCenterForTraffic.IsCreateTrace = false;
                DataCenterForTraffic.IsCreate24HoursGeoJson = false;
                DataCenterForTraffic.IsCreateWeekNoGeoJson = false;
                DataCenterForTraffic.IsCreateGeoJson = true;
                DataCenterForTraffic.EDA();
            }
            else
            {
                DataCenterForSecurity.Load(-1);
                DataCenterForSecurity.EDA();
            }
        }
    }
}

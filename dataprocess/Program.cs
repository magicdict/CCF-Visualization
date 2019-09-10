using System;

namespace dataprocess
{
    class Program
    {
        static void Main(string[] args)
        {
            var IsDidi = true;
            if (IsDidi)
            {
                DataCenterForTraffic.Load(5_000_000);
                DataCenterForTraffic.EDA();
            }
            else
            {
                DataCenterFor360.Load(-1);
                DataCenterFor360.EDA();
            }
        }
    }
}

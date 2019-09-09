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
                DataCenterForDidi.Load(2_000_000);
                DataCenterForDidi.EDA();
            }
            else
            {
                DataCenterFor360.Load(-1);
                DataCenterFor360.EDA();
            }
        }
    }
}

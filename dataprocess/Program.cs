using System;

namespace dataprocess
{
    class Program
    {
        static void Main(string[] args)
        {
            var IsDidi = false;
            if (IsDidi)
            {
                DataCenterForDidi.Load(10_0000);
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

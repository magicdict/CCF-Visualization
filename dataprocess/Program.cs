namespace dataprocess
{
    class Program
    {
        static void Main(string[] args)
        {
            var IsDidi = true;
            if (IsDidi)
            {
                DataCenterForTraffic.Load(4_000_000);
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

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Concurrent;

public static class DataCenterForSecuritySemi
{

    public static string RawDataFolder = @"F:\CCF-Visualization\RawData\企业网络资产及安全事件分析与可视化复赛";

    public static string AfterProcessFolder = @"F:\CCF-Visualization\dataprocess\AfterProcess\企业网络资产及安全事件分析与可视化复赛\";

    public static string EDAFile = @"F:\CCF-Visualization\dataprocess\AfterProcess\企业网络资产及安全事件分析与可视化复赛\EDA.log";

    public static string AngularJsonAssetsFolder = @"F:\CCF-Visualization\UI\src\assets\security\json\";




    #region DB

    public static List<DBInfo> records_DB = new List<DBInfo>();
    /// <summary>
    /// 加载数据
    /// </summary>
    public static void Load_DB(int MaxRecord = -1)
    {
        var RawDataFolder_db = RawDataFolder + "\\db";
        var cnt = 0;
        foreach (var filename in Directory.GetFiles(RawDataFolder_db))
        {
            var sr = new StreamReader(filename);
            while (!sr.EndOfStream)
            {
                records_DB.Add(JsonConvert.DeserializeObject<DBInfo>(sr.ReadLine()));
                cnt++;
                if (cnt == MaxRecord) break;    //内存限制
            }
            sr.Close();
            if (cnt == MaxRecord) break;        //内存限制
            Console.WriteLine("DB File:" + filename);
        }
        Console.WriteLine("Total Record Count:" + records_DB.Count);
    }

    public static void EDA_DB()
    {
        var result = records_DB.GroupBy(x => x.command).Select(x => { return (x.Key, x.Count()); });
        foreach (var item in result)
        {
            Console.WriteLine(item.Key + ":" + item.Item2);
        }
    }

    #endregion

    #region Login

    public static List<LoginInfo> records_Login = new List<LoginInfo>();
    /// <summary>
    /// 加载数据
    /// </summary>
    public static void Load_Login(int MaxRecord = -1)
    {
        var RawDataFolder_login = RawDataFolder + "\\login";
        var cnt = 0;
        foreach (var filename in Directory.GetFiles(RawDataFolder_login))
        {
            var sr = new StreamReader(filename);
            while (!sr.EndOfStream)
            {
                records_Login.Add(JsonConvert.DeserializeObject<LoginInfo>(sr.ReadLine()));
                cnt++;
                if (cnt == MaxRecord) break;    //内存限制
            }
            sr.Close();
            if (cnt == MaxRecord) break;        //内存限制
            Console.WriteLine("DB File:" + filename);
        }
        Console.WriteLine("Total Record Count:" + records_Login.Count);
    }

    public static void EDA_Login()
    {
        var result = records_Login.GroupBy(x => x.user).Select(x => { return (x.Key, x.Count()); });
        foreach (var item in result)
        {
            Console.WriteLine(item.Key + ":" + item.Item2);
        }
    }

    #endregion

}
using System;
using System.Globalization;
public class OrderDetails
{
    /// <summary>
    /// 订单ID	string类型且已脱敏
    /// </summary>
    /// <value></value>
    public string order_id { get; set; }
    /// <summary>
    /// 产品线ID	1滴滴专车， 2滴滴企业专车， 3滴滴快车， 4滴滴企业快车
    /// </summary>
    /// <value></value>
    public Eproduct_id product_id { get; set; }
    /// <summary>
    /// 城市ID	选取海口当地
    /// </summary>
    /// <value></value>	
    public string city_id { get; set; }
    /// <summary>
    /// 城市区号	海口区号
    /// </summary>
    /// <value></value>	
    public string district { get; set; }
    /// <summary>
    /// 二级区县	记录区县id
    /// </summary>
    /// <value></value>
    public string county { get; set; }
    /// <summary>
    ///  订单时效	0实时，1预约   
    /// </summary>
    /// <value></value>
    public Eorder_type order_type { get; set; }
    /// <summary>
    /// 订单类型	1包车，4拼车
    /// </summary>
    /// <value></value>
    public Ecombo_type combo_type { get; set; }
    /// <summary>
    /// 交通类型	1企业时租，2企业接机套餐，3企业送机套餐，4拼车，5接机，6送机，302跨城拼车
    /// </summary>
    /// <value></value>
    public Etraffic_type traffic_type { get; set; }
    /// <summary>
    /// 乘车人数	拼车场景，乘客选择的乘车人数
    /// </summary>
    /// <value></value>
    public int passenger_count { get; set; }
    /// <summary>
    /// 司机子产品线	司机所属产品线
    /// </summary>
    /// <value></value>
    public string driver_product_id { get; set; }
    /// <summary>
    /// 乘客发单时出发地与终点的预估路面距离
    /// 乘客发单时，出发地与终点的预估路面距离
    /// </summary>
    /// <value></value>
    public double start_dest_distance_km { get; set; }
    /// <summary>
    /// 司机点击‘到达’的时间	司机点击‘到达目的地’的时间
    /// </summary>
    /// <value></value>
    public DateTime arrive_time { get; set; }
    /// <summary>
    /// 出发时间	
    /// 如果是实时单，出发时间(departure_time) 与司机点击‘开始计费’的时间(begin_charge_time)含义相同；
    /// 如果是预约单，是指乘客填写的出发时间
    /// </summary>
    /// <value></value>
    public DateTime departure_time { get; set; }
    /// <summary>
    /// 预估价格	根据用户输入的起始点和目的地预估价格
    /// </summary>
    /// <value></value>
    public Single pre_total_fee { get; set; }
    /// <summary>
    /// 时长（分钟）
    /// </summary>
    /// <value></value>
    public int normal_time { get; set; }

    /// <summary>
    /// TraceID
    /// </summary>
    /// <value></value>
    public string bubble_trace_id { get; set; }
    /// <summary>
    /// 一级业务线	1专车，3快车，9豪华车
    /// </summary>
    /// <value></value>
    public Eproduct_1level product_1level { get; set; }
    /// <summary>
    /// 对应乘客填写的目的地
    /// </summary>
    /// <value></value>
    public Geo dest { get; set; }
    /// <summary>
    /// 乘客填写的起始点
    /// </summary>
    /// <value></value>
    public Geo starting { get; set; }

    /// <summary>
    /// 年
    /// </summary>
    /// <value></value>
    public int year { get; set; }

    /// <summary>
    /// 月
    /// </summary>
    /// <value></value>
    public int month { get; set; }

    /// <summary>
    /// 日
    /// </summary>
    /// <value></value>
    public int day { get; set; }

    public string WeekNo
    {
        get
        {
            var d = new DateTime(year, month, day);
            var startard = DateTime.ParseExact("20170501", "yyyyMMdd", null);
            var diff = d.Subtract(startard);
            var weekidx = (int)diff.TotalDays / 7;
            return startard.AddDays(weekidx * 7).ToString("yyyyMMdd");
        }
    }

    /// <summary>
    /// 等待时间
    /// </summary>
    /// <value></value>
    public int WaitTime
    {
        get
        {
            if (arrive_time == DateTime.MinValue || departure_time == DateTime.MinValue) return -1;
            return (int)arrive_time.Subtract(departure_time).TotalMinutes;
        }
    }

    public enum Eproduct_id
    {
        滴滴专车 = 1,
        滴滴企业专车 = 2,
        滴滴快车 = 3,
        滴滴企业快车 = 4
    }

    public enum Eorder_type
    {
        实时 = 0,
        预约 = 1
    }

    public enum Ecombo_type
    {
        未知 = -1,
        包车 = 1,
        拼车 = 4,
    }

    public enum Etraffic_type
    {
        未知 = -1,
        企业时租 = 1,
        企业接机套餐 = 2,
        企业送机套餐 = 3,
        拼车 = 4,
        接机 = 5,
        送机 = 6,
        跨城拼车 = 302
    }


    public enum Eproduct_1level
    {
        专车 = 1,
        快车 = 3,
        豪华车 = 9
    }

    public struct Geo
    {
        /// <summary>
        /// 经度
        /// </summary>
        public double lng;
        /// <summary>
        /// /// 纬度
        /// </summary>
        public double lat;

        const double baiduOffsetlng = 0.0063;
        const double baiduOffsetlat = 0.0058;

        public Geo(double _lng, double _lat)
        {
            lng = Math.Round(_lng + baiduOffsetlng, 4);
            lat = Math.Round(_lat + baiduOffsetlat, 4);
        }

        public string POI
        {
            get
            {
                //美兰机场
                if (lng >= 110.4560 && lng <= 110.4875 && lat >= 19.9420 && lat <= 19.9480) return "机场";  //美兰机场
                //火车站东站
                if (lng >= 110.3507 - 0.002 && lng <= 110.3507 + 0.002 && lat >= 19.9892 - 0.002 && lat <= 19.9892 + 0.002) return "火车站";    //海口火车东站
                if (lng >= 110.1688 - 0.002 && lng <= 110.1688 + 0.002 && lat >= 20.0332 - 0.002 && lat <= 20.0332 + 0.002) return "火车站";    //海口火车站
                //汽车站
                if (lng >= 110.2962 - 0.001 && lng <= 110.2962 + 0.001 && lat >= 20.0189 - 0.001 && lat <= 20.0189 + 0.001) return "汽车站";
                //医院
                if (lng >= 110.2933 - 0.001 && lng <= 110.2933 + 0.001 && lat >= 20.013 - 0.001 && lat <= 20.013 + 0.001) return "医院";    //海口省人民医院
                if (lng >= 110.3485 - 0.001 && lng <= 110.3485 + 0.001 && lat >= 20.0662 - 0.001 && lat <= 20.0662 + 0.001) return "医院";  //海口市人民医院

                //商圈
                if (lng >= 110.355 - 0.001 && lng <= 110.355 + 0.001 && lat >= 20.0217 - 0.001 && lat <= 20.0217 + 0.001) return "商圈";
                if (lng >= 110.3284 - 0.001 && lng <= 110.3284 + 0.001 && lat >= 20.0268 - 0.001 && lat <= 20.0268 + 0.001) return "商圈";
                if (lng >= 110.3488 - 0.001 && lng <= 110.3488 + 0.001 && lat >= 20.0359 - 0.001 && lat <= 20.0359 + 0.001) return "商圈";

                //学校
                if (lng >= 110.3328 - 0.001 && lng <= 110.3328 + 0.001 && lat >= 20.0673 - 0.001 && lat <= 20.0673 + 0.001) return "学校";  //海南大学
                if (lng >= 110.5008 - 0.001 && lng <= 110.5008 + 0.001 && lat >= 19.9685 - 0.001 && lat <= 19.9685 + 0.001) return "学校";  //海口经济学院
                if (lng >= 110.4898 - 0.001 && lng <= 110.4898 + 0.001 && lat >= 19.9743 - 0.001 && lat <= 19.9743 + 0.001) return "学校";  //海口经济学院

                return "海口";
            }
        }
    }

    public (Geo source, Geo dest) Trace
    {
        get
        {
            return (starting, dest);
        }
    }


    public OrderDetails(string RawData)
    {
        var Cols = RawData.Split("\t");
        order_id = Cols[0];

        switch (Cols[1])
        {
            case "1": product_id = Eproduct_id.滴滴专车; break;
            case "2": product_id = Eproduct_id.滴滴企业专车; break;
            case "3": product_id = Eproduct_id.滴滴快车; break;
            case "4": product_id = Eproduct_id.滴滴企业快车; break;
            default:
                throw new Exception("Product Id Error:" + Cols[1]);
        }

        city_id = Cols[2];
        district = Cols[3];
        county = Cols[4];

        switch (Cols[5])
        {
            case "0": order_type = Eorder_type.实时; break;
            case "1": order_type = Eorder_type.预约; break;
            default:
                throw new Exception("order_type Id Error:" + Cols[5]);
        }

        switch (Cols[6])
        {
            case "1": combo_type = Ecombo_type.包车; break;
            case "4": combo_type = Ecombo_type.拼车; break;
            default:
                combo_type = Ecombo_type.未知; break;
        }

        switch (Cols[7])
        {
            case "1": traffic_type = Etraffic_type.企业时租; break;
            case "2": traffic_type = Etraffic_type.企业接机套餐; break;
            case "3": traffic_type = Etraffic_type.企业送机套餐; break;
            case "4": traffic_type = Etraffic_type.拼车; break;
            case "5": traffic_type = Etraffic_type.接机; break;
            case "6": traffic_type = Etraffic_type.送机; break;
            case "302": traffic_type = Etraffic_type.跨城拼车; break;
            default:
                traffic_type = Etraffic_type.未知; break;
        }

        passenger_count = int.Parse(Cols[8]);
        driver_product_id = Cols[9];
        start_dest_distance_km = double.Parse(Cols[10]) / 1000;
        if (Cols[11] != "0000-00-00 00:00:00") arrive_time = DateTime.ParseExact(Cols[11], "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture, DateTimeStyles.None);
        if (Cols[12] != "0000-00-00 00:00:00") departure_time = DateTime.ParseExact(Cols[12], "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture, DateTimeStyles.None);
        pre_total_fee = Single.Parse(Cols[13]);
        if (Cols[14] != "NULL")
        {
            normal_time = int.Parse(Cols[14]);
        }
        bubble_trace_id = Cols[15];

        switch (Cols[16])
        {
            case "1": product_1level = Eproduct_1level.专车; break;
            case "3": product_1level = Eproduct_1level.快车; break;
            case "9": product_1level = Eproduct_1level.豪华车; break;
            default:
                throw new Exception("product_1level Id Error:" + Cols[16]);
        }

        dest = new Geo(double.Parse(Cols[17]), double.Parse(Cols[18]));
        starting = new Geo(double.Parse(Cols[19]), double.Parse(Cols[20]));

        year = int.Parse(Cols[21]);
        month = int.Parse(Cols[22]);
        day = int.Parse(Cols[23]);

    }
}
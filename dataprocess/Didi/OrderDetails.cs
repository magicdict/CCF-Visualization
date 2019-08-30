using System;

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
    public string start_dest_distance { get; set; }
    /// <summary>
    /// 司机点击‘到达’的时间	司机点击‘到达目的地’的时间
    /// </summary>
    /// <value></value>
    public string arrive_time { get; set; }
    /// <summary>
    /// 出发时间	
    /// 如果是实时单，出发时间(departure_time) 与司机点击‘开始计费’的时间(begin_charge_time)含义相同；
    /// 如果是预约单，是指乘客填写的出发时间
    /// </summary>
    /// <value></value>
    public string departure_time { get; set; }
    /// <summary>
    /// 预估价格	根据用户输入的起始点和目的地预估价格
    /// </summary>
    /// <value></value>
    public Single pre_total_fee { get; set; }
    /// <summary>
    /// 时长（分钟）
    /// </summary>
    /// <value></value>
    public string normal_time { get; set; }
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
        /// 纬度
        /// </summary>
        public double lat;


        public Geo(double _lng, double _lat)
        {
            lng = _lng;
            lat = _lat;
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
        start_dest_distance = Cols[10];
        arrive_time = Cols[11];
        departure_time = Cols[12];
        pre_total_fee = Single.Parse(Cols[13]);
        normal_time = Cols[14];
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
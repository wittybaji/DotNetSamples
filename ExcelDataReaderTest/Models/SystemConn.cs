using System;
using System.Net;

namespace ExcelDataReaderTest
{
    /// <summary>
    /// 系统通信地址
    /// </summary>
    public class SystemConn : SystemData
    {
        /// <summary>
        /// 紫网
        /// </summary>
        public IPEndPoint PurpleIPPoint { get; set; }
        public string PurpleIPPointStr
        {
            get { return PurpleIPPoint != null ? PurpleIPPoint.ToString() : null; }
            set { PurpleIPPoint = new IPEndPoint(IPAddress.Parse(value.Split(':')[0]), Convert.ToInt32(value.Split(':')[1])); }
        }
        /// <summary>
        /// 黄网
        /// </summary>
        public IPEndPoint YellowIPPoint { get; set; }
        public string YellowIPPointStr
        {
            get { return YellowIPPoint != null ? YellowIPPoint.ToString() : null; }
            set { YellowIPPoint = new IPEndPoint(IPAddress.Parse(value.Split(':')[0]), Convert.ToInt32(value.Split(':')[1])); }
        }
        /// <summary>
        /// 红网
        /// </summary>
        public IPEndPoint RedIPPoint { get; set; }
        public string RedIPPointStr
        {
            get { return RedIPPoint != null ? RedIPPoint.ToString() : null; }
            set { RedIPPoint = new IPEndPoint(IPAddress.Parse(value.Split(':')[0]), Convert.ToInt32(value.Split(':')[1])); }
        }
        /// <summary>
        /// 蓝网
        /// </summary>
        public IPEndPoint BlueIPPoint { get; set; }
        public string BlueIPPointStr
        {
            get { return BlueIPPoint != null ? BlueIPPoint.ToString() : null; }
            set { BlueIPPoint = new IPEndPoint(IPAddress.Parse(value.Split(':')[0]), Convert.ToInt32(value.Split(':')[1])); }
        }
        /// <summary>
        /// 直连网1
        /// </summary>
        public IPEndPoint Direct1IPPoint { get; set; }
        public string Direct1IPPointStr
        {
            get { return Direct1IPPoint != null ? Direct1IPPoint.ToString() : null; }
            set { Direct1IPPoint = new IPEndPoint(IPAddress.Parse(value.Split(':')[0]), Convert.ToInt32(value.Split(':')[1])); }
        }
        /// <summary>
        /// 直连网2
        /// </summary>
        public IPEndPoint Direct2IPPoint { get; set; }
        public string Direct2IPPointStr
        {
            get { return Direct2IPPoint != null ? Direct2IPPoint.ToString() : null; }
            set { Direct2IPPoint = new IPEndPoint(IPAddress.Parse(value.Split(':')[0]), Convert.ToInt32(value.Split(':')[1])); }
        }
        /// <summary>
        /// 外部红网
        /// </summary>
        public IPEndPoint OuterRedIPPoint { get; set; }
        public string OuterRedIPPointStr
        {
            get { return OuterRedIPPoint != null ? OuterRedIPPoint.ToString() : null; }
            set { OuterRedIPPoint = new IPEndPoint(IPAddress.Parse(value.Split(':')[0]), Convert.ToInt32(value.Split(':')[1])); }
        }
        /// <summary>
        /// 外部蓝网
        /// </summary>
        public IPEndPoint OuterBlueIPPoint { get; set; }
        public string OuterBlueIPPointStr
        {
            get { return OuterBlueIPPoint != null ? OuterBlueIPPoint.ToString() : null; }
            set { OuterBlueIPPoint = new IPEndPoint(IPAddress.Parse(value.Split(':')[0]), Convert.ToInt32(value.Split(':')[1])); }
        }
    }
}

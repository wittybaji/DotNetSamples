using System;
using System.Collections.Generic;
using System.Net;
using System.Xml.Serialization;

namespace XmlTest
{
    /// <summary>
    /// 组播配置
    /// </summary>
    public class MultiCastConfig
    {
        /// <summary>
        /// 是否使用组播
        /// </summary>
        public bool UseMultiCast { get; set; } = false;

        /// <summary>
        /// 组播帧配置列表
        /// </summary>
        public List<MultiCastNode> MultiCastNodeList { get; set; } = new List<MultiCastNode>();
    }

    /// <summary>
    /// 单个组播组的配置
    /// </summary>
    public class MultiCastNode
    {
        /// <summary>
        /// 虚拟组播设备信息
        /// </summary>
        public SystemConn System { get; set; }

        /// <summary>
        /// 使用该组播帧的设备类型
        /// </summary>
        public List<SystemSubType> SubTypes { get; set; }

    }

    /// <summary>
    ///系统基础数据类 (通过SysType+SubSysID唯一确定一个系统)
    /// </summary>
    public class SystemData : SysDataBase, IEquatable<SystemData>
    {
        /// <summary>
        /// 系统名称
        /// </summary>
        public string SysName { get; set; }
        /// <summary>
        /// 系统类型
        /// </summary>
        public SystemType SysType { get; set; }
        /// <summary>
        /// 内部子系统名称（外部设备（非ATS非RTDB）与SysName一致）
        /// </summary>
        public string SubSysName { get; set; }
        /// <summary>
        /// 内部子系统类型
        /// </summary>
        public SystemSubType SubSysType { get; set; }

        /// <summary>
        /// 重写系统数据相等方法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(SystemData x, SystemData y)
        {
            if (x == null || y == null)
            {
                return false;
            }
            return x.SysType == y.SysType && x.SubSysID == y.SubSysID;
        }

        /// <summary>
        /// 重写系统数据hash方法
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(SystemData obj)
        {
            return (obj.SysType.GetHashCode() << 16) | obj.SubSysID;
        }

        /// <summary>
        /// 重写系统数据相等方法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(SystemData y)
        {
            return Equals(this, y);
        }

        /// <summary>
        /// 重写系统数据hash方法
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return GetHashCode(this);
        }

        /// <summary>
        /// 打印全部内容
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"SysType:{SysType}, SysId:{SysID}, SysName:{SysName ?? string.Empty}, ControlID:{ControlID}, CIID:{CIID}, StationID:{StationID}, SubSysType:{SubSysType}, SubSysID:{SubSysID}, SubSysName:{SubSysName ?? string.Empty}";
        }

        /// <summary>
        /// 打印SysType,SubSysID
        /// </summary>
        /// <returns></returns>
        public string ToShortString()
        {
            return $"SysType:{SysType},SubSysID:{SubSysID}";
        }

        /// <summary>
        /// 打印SysType,SubSysType,CIID
        /// </summary>
        /// <returns></returns>
        public string ToSummryString()
        {
            return $" SysType:{SysType}, SubSysType:{SubSysType}, CIID:{CIID}";
        }
        /// <summary>
        /// 获取副本
        /// </summary>
        /// <returns></returns>
        public SystemData Copy()
        {
            return new SystemData
            {
                SysID = this.SysID,
                SubSysID = this.SubSysID,
                SysName = this.SysName,
                SubSysName = this.SubSysName,
                SysType = this.SysType,
                SubSysType = this.SubSysType,
                DirectSubSysID = this.DirectSubSysID,
                CIID = this.CIID,
                ControlID = this.ControlID,
                RtuID = this.RtuID,
                StationID = this.StationID,
            };
        }
    }


    /// <summary>
    /// 系统数据基类
    /// </summary>
    public class SysDataBase
    {
        /// <summary>
        /// 系统ID
        /// </summary>
        public ushort SysID { get; set; }
        /// <summary>
        /// 系统子ID（外部设备（非ATS非RTDB）与SysID一致）
        /// </summary>
        public ushort SubSysID { get; set; }
        /// <summary>
        /// 所属联锁ID（中心设备为0）
        /// </summary>
        public ushort CIID { get; set; }
        /// <summary>
        /// 所属RtuID（中心设备为0）
        /// </summary>
        public ushort RtuID { get; set; }
        /// <summary>
        /// 所属控制中心ID(车站设备为0)
        /// </summary>
        public ushort ControlID { get; set; }
        /// <summary>
        /// 车站ID（中心设备为0）
        /// </summary>
        public ushort StationID { get; set; }
        /// <summary>
        /// 直连的另一机子系统ID(SubSysID)
        /// </summary>
        public ushort DirectSubSysID { get; set; }
    }

    /// <summary>
    /// 系统子类类型
    /// </summary>
    public enum SystemSubType : byte
    {
        /// <summary>
        /// 无效设备类型
        /// </summary>
        [XmlEnum(Name = "None")]
        [Enum(Title = "无效设备")]
        None = 127,
        /// <summary>
        /// 默认类型
        /// </summary>
        [XmlEnum(Name = "DefaultType")]
        [Enum(Title = "默认类型")]
        DefaultType = 0,
        /// <summary>
        /// 应用服务器
        /// </summary>
        [XmlEnum(Name = "ApplicationServerType")]
        [Enum(Title = "应用服务器")]
        ApplicationServerType = 1,
        /// <summary>
        /// 调度工作站
        /// </summary>
        [XmlEnum(Name = "DispatchWorkStationType")]
        [Enum(Title = "调度工作站")]
        DispatchWorkStationType = 2,
        /// <summary>
        /// 车辆调度工作站
        /// </summary>
        [XmlEnum(Name = "VehicleDispatchWorkStationType")]
        [Enum(Title = "车辆调度工作站")]
        VehicleDispatchWorkStationType = 3,
        /// <summary>
        /// 数据库
        /// </summary>
        [XmlEnum(Name = "DataBaseType")]
        [Enum(Title = "数据库")]
        DataBaseType = 4,
        /// <summary>
        /// 计划工作站
        /// </summary>
        [XmlEnum(Name = "PlanWorkStationType")]
        [Enum(Title = "计划工作站")]
        PlanWorkStationType = 5,
        /// <summary>
        /// 派班工作站
        /// </summary>
        [XmlEnum(Name = "ScheduleWorkStationType")]
        [Enum(Title = "派班工作站")]
        ScheduleWorkStationType = 6,
        /// <summary>
        /// 维护工作站
        /// </summary>
        [XmlEnum(Name = "MaintainWorkStationType")]
        [Enum(Title = "维护工作站")]
        MaintainWorkStationType = 7,
        /// <summary>
        /// 派班插件
        /// </summary>
        [XmlEnum(Name = "DispatchType")]
        [Enum(Title = "派班插件")]
        DispatchType = 8,
        /// <summary>
        /// 网关计算机
        /// </summary>
        [XmlEnum(Name = "GateWayType")]
        [Enum(Title = "网关计算机")]
        GateWayType = 9,
        /// <summary>
        /// 现地
        /// </summary>
        [XmlEnum(Name = "Xiandi")]
        [Enum(Title = "现地")]
        Xiandi = 10,
        /// <summary>
        /// 运行图插件
        /// </summary>
        [XmlEnum(Name = "TrainGraphType")]
        [Enum(Title = "运行图插件")]
        TrainGraphType = 11,
        /// <summary>
        /// 监视终端
        /// </summary>
        [XmlEnum(Name = "View")]
        [Enum(Title = "监视终端")]
        View = 12,
        /// <summary>
        /// 大屏
        /// </summary>
        [XmlEnum(Name = "DisplayScreen")]
        [Enum(Title = "大屏")]
        DisplayScreen = 13,
        /// <summary>
        /// 智能调度
        /// </summary>
        [XmlEnum(Name = "IntelligentDispatch")]
        [Enum(Title = "智能调度")]
        IntelligentDispatch = 14,
        /// <summary>
        /// 发车列表插件
        /// </summary>
        [XmlEnum(Name = "DepartListType")]
        [Enum(Title = "发车列表插件")]
        DepartListType = 20,
        /// <summary>
        /// 站场软件
        /// </summary>
        [XmlEnum(Name = "StationUiType")]
        [Enum(Title = "站场软件")]
        StationUiType = 21,
        /// <summary>
        /// 列车信息软件
        /// </summary>
        [XmlEnum(Name = "TrainInfoType")]
        [Enum(Title = "列车信息软件")]
        TrainInfoType = 22,
        /// <summary>
        /// 列车休眠唤醒软件
        /// </summary>
        [XmlEnum(Name = "TrainSleepWakeType")]
        [Enum(Title = "列车休眠唤醒软件")]
        TrainSleepWakeType = 23,
        /// <summary>
        /// 通信中间件
        /// </summary>
        [XmlEnum(Name = "CommMidSoft")]
        [Enum(Title = "通信中间件")]
        CommMidSoft = 26,
        /// <summary>
        /// 路网中心分线路监控
        /// </summary>
        [XmlEnum(Name = "TccFront")]
        [Enum(Title = "路网中心分线路监控")]
        TccFront = 27,
        /// <summary>
        /// TDT网关
        /// </summary>
        [XmlEnum(Name = "TdtGateway")]
        [Enum(Title = "TDT网关")]
        TdtGateway = 30,
        /// <summary>
        /// 车站分机
        /// </summary>
        [XmlEnum(Name = "StationServerType")]
        [Enum(Title = "车站分机")]
        StationServerType = 31,
        /// <summary>
        /// 互联接口机 用于与各线路车站分机通信
        /// </summary>
        [XmlEnum(Name = "ConnectedInterfaceMachine")]
        [Enum(Title = "互联接口机")]
        ConnectedInterfaceMachine = 35,
        /// <summary>
        /// 前台通信中间件
        /// </summary>
        [XmlEnum(Name = "FrontCommMidSoft")]
        [Enum(Title = "前台通信中间件")]
        FrontCommMidSoft = 36,
        /// <summary>
        /// 数据库同步软件
        /// </summary>
        [XmlEnum(Name = "Dbsynchrolize")]
        [Enum(Title = "数据库同步软件")]
        Dbsynchrolize = 37,
        /// <summary>
        /// 看门狗软件
        /// </summary>
        [XmlEnum(Name = "WatchDog")]
        [Enum(Title = "看门狗软件")]
        WatchDog = 38,
        /// <summary>
        /// 通信前置机
        /// </summary>
        [XmlEnum(Name = "FrontEndProcessType_FEP")]
        [Enum(Title = "通信前置机")]
        FrontEndProcessType_FEP = 51,
        /// <summary>
        /// 调度备控
        /// </summary>
        [XmlEnum(Name = "DispatchReserveControl")]
        [Enum(Title = "调度备控")]
        DispatchReserveControl = 97,
        /// <summary>
        /// 检修网关 
        /// </summary>
        [XmlEnum(Name = "RepairGatewayType")]
        [Enum(Title = "检修网关")]
        RepairGatewayType = 98,
        /// <summary>
        /// 车辆网关计算机
        /// </summary>
        [XmlEnum(Name = "VehicleGateWayType")]
        [Enum(Title = "车辆网关计算机")]
        VehicleGateWayType = 99,
        /// <summary>
        /// RTDB
        /// </summary>
        [XmlEnum(Name = "RTDBType")]
        [Enum(Title = "RTDB")]
        RtdbType = 100,
        /// <summary>
        /// 调度主任
        /// </summary>
        [XmlEnum(Name = "AdminDirector")]
        [Enum(Title = "调度主任")]
        AdminDirector = 101,
        /// <summary>
        /// 屏蔽门
        /// </summary>
        [XmlEnum(Name = "PsdType")]
        [Enum(Title = "屏蔽门")]
        PsdType = 102,
        /// <summary>
        /// 运行图插件
        /// </summary>
        [XmlEnum(Name = "TrainGraphExePlugIn")]
        [Enum(Title = "运行图插件")]
        TrainGraphExePlugIn = 103,
        /// <summary>
        /// 车辆调度插件
        /// </summary>
        [XmlEnum(Name = "VehicleDispatchPlugin")]
        [Enum(Title = "车辆调度插件")]
        VehicleDispatchPlugin = 104,
        /// <summary>
        /// 数据库DG
        /// </summary>
        [XmlEnum(Name = "DataBaseDg")]
        [Enum(Title = "数据库DG")]
        DataBaseDg = 105,
        /// <summary>
        /// 运行图大屏工作站
        /// </summary>
        [XmlEnum(Name = "TrainGraphDisplayWorkStationType")]
        [Enum(Title = "运行图大屏工作站")]
        TrainGraphDisplayWorkStationType = 106,
        /// <summary>
        /// linux版PSD接口机
        /// </summary>
        [XmlEnum(Name = "PsdLinux")]
        [Enum(Title = "linux版PSD接口机")]
        PsdLinux = 107,
        /// <summary>
        /// 现地RTDB
        /// </summary>
        [XmlEnum(Name = "XiandiRtdbType")]
        [Enum(Title = "现地RTDB")]
        XiandiRtdbType = 108,
        /// <summary>
        /// 车站分机RTDB
        /// </summary>
        [XmlEnum(Name = "StationServerRtdbType")]
        [Enum(Title = "车站分机RTDB")]
        StationServerRtdbType = 109,
        /// <summary>
        /// 中心RTDB
        /// </summary>
        [XmlEnum(Name = "CenterRtdbType")]
        [Enum(Title = "中心RTDB")]
        CenterRtdbType = 110,
        /// <summary>
        /// 中心车辆网关RTDB
        /// </summary>
        [XmlEnum(Name = "CenterVehicleGateWayRtdb")]
        [Enum(Title = "中心车辆网关RTDB")]
        CenterVehicleGateWayRtdb = 111,
        /// <summary>
        /// 车站车辆网关RTDB
        /// </summary>
        [XmlEnum(Name = "StationVehicleGateWayRtdb")]
        [Enum(Title = "车站车辆网关RTDB")]
        StationVehicleGateWayRtdb = 112,
        /// <summary>
        /// 中央联锁工作站
        /// </summary>
        [XmlEnum(Name = "CentralCIWorkStation")]
        [Enum(Title = "中央联锁工作站")]
        CentralCIWorkStation = 113,
    }


    /// <summary>
    /// 系统通信地址
    /// </summary>
    public class SystemConn : SystemData
    {
        /// <summary>
        /// 紫网
        /// </summary>
        [XmlIgnore]
        public IPEndPoint PurpleIPPoint { get; set; }
        public string PurpleIPPointStr
        {
            get { return PurpleIPPoint != null ? PurpleIPPoint.ToString() : null; }
            set { PurpleIPPoint = new IPEndPoint(IPAddress.Parse(value.Split(':')[0]), Convert.ToInt32(value.Split(':')[1])); }
        }
        /// <summary>
        /// 黄网
        /// </summary>
        [XmlIgnore]
        public IPEndPoint YellowIPPoint { get; set; }
        public string YellowIPPointStr
        {
            get { return YellowIPPoint != null ? YellowIPPoint.ToString() : null; }
            set { YellowIPPoint = new IPEndPoint(IPAddress.Parse(value.Split(':')[0]), Convert.ToInt32(value.Split(':')[1])); }
        }
        /// <summary>
        /// 红网
        /// </summary>
        [XmlIgnore]
        public IPEndPoint RedIPPoint { get; set; }
        public string RedIPPointStr
        {
            get { return RedIPPoint != null ? RedIPPoint.ToString() : null; }
            set { RedIPPoint = new IPEndPoint(IPAddress.Parse(value.Split(':')[0]), Convert.ToInt32(value.Split(':')[1])); }
        }
        /// <summary>
        /// 蓝网
        /// </summary>
        [XmlIgnore]
        public IPEndPoint BlueIPPoint { get; set; }
        public string BlueIPPointStr
        {
            get { return BlueIPPoint != null ? BlueIPPoint.ToString() : null; }
            set { BlueIPPoint = new IPEndPoint(IPAddress.Parse(value.Split(':')[0]), Convert.ToInt32(value.Split(':')[1])); }
        }
        /// <summary>
        /// 直连网1
        /// </summary>
        [XmlIgnore]
        public IPEndPoint Direct1IPPoint { get; set; }
        public string Direct1IPPointStr
        {
            get { return Direct1IPPoint != null ? Direct1IPPoint.ToString() : null; }
            set { Direct1IPPoint = new IPEndPoint(IPAddress.Parse(value.Split(':')[0]), Convert.ToInt32(value.Split(':')[1])); }
        }
        /// <summary>
        /// 直连网2
        /// </summary>
        [XmlIgnore]
        public IPEndPoint Direct2IPPoint { get; set; }
        public string Direct2IPPointStr
        {
            get { return Direct2IPPoint != null ? Direct2IPPoint.ToString() : null; }
            set { Direct2IPPoint = new IPEndPoint(IPAddress.Parse(value.Split(':')[0]), Convert.ToInt32(value.Split(':')[1])); }
        }
        /// <summary>
        /// 外部红网
        /// </summary>
        [XmlIgnore]
        public IPEndPoint OuterRedIPPoint { get; set; }
        public string OuterRedIPPointStr
        {
            get { return OuterRedIPPoint != null ? OuterRedIPPoint.ToString() : null; }
            set { OuterRedIPPoint = new IPEndPoint(IPAddress.Parse(value.Split(':')[0]), Convert.ToInt32(value.Split(':')[1])); }
        }
        /// <summary>
        /// 外部蓝网
        /// </summary>
        [XmlIgnore]
        public IPEndPoint OuterBlueIPPoint { get; set; }
        public string OuterBlueIPPointStr
        {
            get { return OuterBlueIPPoint != null ? OuterBlueIPPoint.ToString() : null; }
            set { OuterBlueIPPoint = new IPEndPoint(IPAddress.Parse(value.Split(':')[0]), Convert.ToInt32(value.Split(':')[1])); }
        }
    }

    /// <summary>
    /// 系统大类类型
    /// </summary>
    public enum SystemType : byte
    {
        /// <summary>
        /// ATS系统
        /// </summary>
        [XmlEnum(Name = "ATSType")]
        [Enum(Title = "ATS系统")]
        ATSType = 3,
        /// <summary>
        /// RTDB系统
        /// </summary>
        [XmlEnum(Name = "RTDB")]
        [Enum(Title = "RTDB系统")]
        RTDB = 4,
        /// <summary>
        /// 通信中间件 区别于ATS
        /// </summary>
        [XmlEnum(Name = "CommMidSoftType")]
        [Enum(Title = "通信中间件")]
        CommMidSoftType = 5,
        /// <summary>
        /// 邻线ATS接口机（跨线专用接口机）
        /// </summary>
        [XmlEnum(Name = "NeighborLineAtsDvc")]
        [Enum(Title = "邻线ATS接口机")]
        NeighborLineAtsDvc = 6,
        /// <summary>
        /// VOBC系统
        /// </summary>
        [XmlEnum(Name = "VOBCType")]
        [Enum(Title = "VOBC系统")]
        VOBCType = 20,
        /// <summary>
        /// AOM系统
        /// </summary>
        [XmlEnum(Name = "AOMType")]
        [Enum(Title = "AOM系统")]
        AOMType = 21,
        /// <summary>
        /// 综合监控
        /// </summary>
        [XmlEnum(Name = "ISCSType")]
        [Enum(Title = "综合监控")]
        ISCSType = 22,
        /// <summary>
        /// TCC系统
        /// </summary>
        [XmlEnum(Name = "TCCType")]
        [Enum(Title = "TCC系统")]
        TCCType = 23,
        /// <summary>
        /// 时钟
        /// </summary>
        [XmlEnum(Name = "ClockType")]
        [Enum(Title = "时钟")]
        ClockType = 24,
        /// <summary>
        /// 无线
        /// </summary>
        [XmlEnum(Name = "RadioType")]
        [Enum(Title = "无线")]
        RadioType = 25,
        /// <summary>
        /// 广播
        /// </summary>
        [XmlEnum(Name = "BsType")]
        [Enum(Title = "广播")]
        BsType = 26,
        /// <summary>
        /// 乘客
        /// </summary>
        [XmlEnum(Name = "PisType")]
        [Enum(Title = "乘客")]
        PisType = 27,
        /// <summary>
        /// 站台TDT
        /// </summary>
        [XmlEnum(Name = "TDTType")]
        [Enum(Title = "站台TDT")]
        TDTType = 28,
        /// <summary>
        /// MSS系统
        /// </summary>
        [XmlEnum(Name = "MSSType")]
        [Enum(Title = "MSS系统")]
        MSSType = 29,
        /// <summary>
        /// ZC系统
        /// </summary>
        [XmlEnum(Name = "ZCType")]
        [Enum(Title = "ZC系统")]
        ZCType = 30,
        /// <summary>
        /// Power
        /// </summary>
        [XmlEnum(Name = "PowerType")]
        [Enum(Title = "Power")]
        PowerType = 31,
        /// <summary>
        /// SCC系统
        /// </summary>
        [XmlEnum(Name = "SCCType")]
        [Enum(Title = "SCC系统")]
        SCCType = 32,
        /// <summary>
        /// 检修安全管理系统 
        /// </summary>
        [XmlEnum(Name = "RepairSystemType")]
        [Enum(Title = "检修安全管理系统")]
        RepairSystemType = 33,
        /// <summary>
        /// 智能检修系统
        /// </summary>
        [XmlEnum(Name = "IntelligenceRepairSystem")]
        [Enum(Title = "智能检修系统")]
        IntelligenceRepairSystem = 34,
        /// <summary>
        /// PSCADA
        /// </summary>
        [XmlEnum(Name = "PSCADA")]
        [Enum(Title = "PSCADA")]
        PSCADA = 35,
        /// <summary>
        /// 全局调度
        /// </summary>
        [XmlEnum(Name = "GlobalSchedul")]
        [Enum(Title = "全局调度")]
        GlobalSchedul = 36,
        /// <summary>
        /// 司机派班
        /// </summary>
        [XmlEnum(Name = "DriverDispatch")]
        [Enum(Title = "司机派班")]
        DriverDispatch = 37,
        /// <summary>
        /// MDIAS
        /// </summary>
        [XmlEnum(Name = "MDIAS")]
        [Enum(Title = "MDIAS")]
        MDIAS = 38,
        /// <summary>
        /// 车辆调
        /// </summary>
        [XmlEnum(Name = "VehicleDispatch")]
        [Enum(Title = "车辆调")]
        VehicleDispatch = 39,
        /// <summary>
        /// 电流表
        /// </summary>  
        [XmlEnum(Name = "AMMETER")]
        [Enum(Title = "电流表")]
        AMMETER = 40,
        /// <summary>
        /// PIS车地无线
        /// </summary> 
        [XmlEnum(Name = "PisRadio")]
        [Enum(Title = "PIS车地无线")]
        PisRadio = 41,
        /// <summary>
        /// ATS接口机（厦门3用于转发邻线首末班车信息）
        /// </summary>
        [XmlEnum(Name = "ATS2ATSType")]
        [Enum(Title = "ATS接口机")]
        ATS2ATSType = 42,
        /// <summary>
        /// DSU系统
        /// </summary>
        [XmlEnum(Name = "DSUType")]
        [Enum(Title = "DSU系统")]
        DSUType = 43,
        /// <summary>
        /// 联锁维护工作站
        /// </summary>
        [XmlEnum(Name = "CIMType")]
        [Enum(Title = "联锁维护工作站")]
        CIMType = 48,
        /// <summary>
        /// 联锁系统（TCT）
        /// </summary>
        [XmlEnum(Name = "CIType")]
        [Enum(Title = "联锁系统")]
        CIType = 60,
        /// <summary>
        /// OC系统
        /// </summary>
        [XmlEnum(Name = "OCType")]
        [Enum(Title = "OC系统")]
        OCType = 61,
        /// <summary>
        /// KVM倒切单元
        /// </summary>
        [XmlEnum(Name = "KVMUnit")]
        [Enum(Title = "KVM倒切单元")]
        KVMUnit = 62,
        /// <summary>
        /// 其它线路的互联接口机
        /// </summary>
        [XmlEnum(Name = "OtherLineInterfaceGateway")]
        [Enum(Title = "其它线路的互联接口机")]
        OtherLineInterfaceGateway = 66,
        /// <summary>
        /// 现地系统
        /// </summary>
        [XmlEnum(Name = "LOCALSTATIONType")]
        [Enum(Title = "现地系统")]
        LOCALSTATIONType = 77,
        /// <summary>
        /// 智能调度
        /// </summary>
        [XmlEnum(Name = "IntelligentDispatch")]
        [Enum(Title = "智能调度")]
        IntelligentDispatch = 80,
        /// <summary>
        /// 联锁上位机(WL)
        /// </summary>
        [XmlEnum(Name = "上位机")]
        [Enum(Title = "联锁上位机(WL)")]
        上位机 = 93,
        /// <summary>
        /// 屏蔽门设备
        /// </summary>
        [XmlEnum(Name = "PsdType")]
        [Enum(Title = "屏蔽门设备")]
        PsdType = 94,
        /// <summary>
        /// TCMS
        /// </summary>
        [XmlEnum(Name = "TCMSType")]
        [Enum(Title = "TCMS")]
        TCMSType = 96,
        /// <summary>
        /// MBAS（多线BAS，北京11）
        /// </summary>
        [XmlEnum(Name = "MBAS")]
        [Enum(Title = "MBAS")]
        MBAS = 150,
        /// <summary>
        /// MSCADA（多线SCADA，北京11）
        /// </summary>
        [XmlEnum(Name = "MSCADA")]
        [Enum(Title = "MSCADA")]
        MSCADA = 151,
        /// <summary>
        /// 组播
        /// </summary>
        [XmlEnum(Name = "Multicast")]
        [Enum(Title = "Multicast")]
        Multicast = 152,
    }
}

namespace ExcelDataReaderTest
{
    /// <summary>
    /// 系统子类类型
    /// </summary>
    public enum SystemSubType : byte
    {
        /// <summary>
        /// 无效设备类型
        /// </summary>
        None = 127,
        /// <summary>
        /// 默认类型
        /// </summary>
        DefaultType = 0,
        /// <summary>
        /// 应用服务器
        /// </summary>
        ApplicationServerType = 1,
        /// <summary>
        /// 调度工作站
        /// </summary>
        DispatchWorkStationType = 2,
        /// <summary>
        /// 车辆调度工作站
        /// </summary>
        VehicleDispatchWorkStationType = 3,
        /// <summary>
        /// 数据库
        /// </summary>
        DataBaseType = 4,
        /// <summary>
        /// 计划工作站
        /// </summary>
        PlanWorkStationType = 5,
        /// <summary>
        /// 派班工作站
        /// </summary>
        ScheduleWorkStationType = 6,
        /// <summary>
        /// 维护工作站
        /// </summary>
        MaintainWorkStationType = 7,
        /// <summary>
        /// 派班插件
        /// </summary>
        DispatchType = 8,
        /// <summary>
        /// 网关计算机
        /// </summary>
        GateWayType = 9,
        /// <summary>
        /// 现地
        /// </summary>
        Xiandi = 10,
        /// <summary>
        /// 运行图插件-嵌入RTDB
        /// </summary>
        TrainGraphType = 11,
        /// <summary>
        /// 监视终端
        /// </summary>
        View = 12,
        /// <summary>
        /// 大屏
        /// </summary>
        DisplayScreen = 13,
        /// <summary>
        /// 智能调度
        /// </summary>
        IntelligentDispatch = 14,
        /// <summary>
        /// 发车列表插件
        /// </summary>
        DepartListType = 20,
        /// <summary>
        /// 站场软件
        /// </summary>
        StationUiType = 21,
        /// <summary>
        /// 列车信息软件
        /// </summary>
        TrainInfoType = 22,
        /// <summary>
        /// 列车休眠唤醒软件
        /// </summary>
        TrainSleepWakeType = 23,
        /// <summary>
        /// 通信中间件
        /// </summary>
        CommMidSoft = 26,
        /// <summary>
        /// 路网中心分线路监控
        /// </summary>
        TccFront = 27,
        /// <summary>
        /// TDT网关
        /// </summary>
        TdtGateway = 30,
        /// <summary>
        /// 车站分机
        /// </summary>
        StationServerType = 31,
        /// <summary>
        /// 互联接口机 用于与各线路车站分机通信
        /// </summary>
        ConnectedInterfaceMachine = 35,
        /// <summary>
        /// 前台通信中间件
        /// </summary>
        FrontCommMidSoft = 36,
        /// <summary>
        /// 数据库同步软件
        /// </summary>
        Dbsynchrolize = 37,
        /// <summary>
        /// 看门狗软件
        /// </summary>
        WatchDog = 38,
        /// <summary>
        /// 通信前置机
        /// </summary>
        FrontEndProcessType_FEP = 51,
        /// <summary>
        /// 调度备控
        /// </summary>
        DispatchReserveControl = 97,
        /// <summary>
        /// 检修网关 
        /// </summary>
        RepairGatewayType = 98,
        /// <summary>
        /// 车辆网关计算机
        /// </summary>
        VehicleGateWayType = 99,
        /// <summary>
        /// RTDB
        /// </summary>
        RtdbType = 100,
        /// <summary>
        /// 调度主任
        /// </summary>
        AdminDirector = 101,
        /// <summary>
        /// 屏蔽门
        /// </summary>
        PsdType = 102,
        /// <summary>
        /// 运行图插件-呼市TCC
        /// </summary>
        TrainGraphExePlugIn = 103,
        /// <summary>
        /// 车辆调度插件
        /// </summary>
        VehicleDispatchPlugin = 104,
        /// <summary>
        /// 数据库DG
        /// </summary>
        DataBaseDg = 105,
        /// <summary>
        /// 运行图大屏工作站
        /// </summary>
        TrainGraphDisplayWorkStationType = 106,
        /// <summary>
        /// linux版PSD接口机
        /// </summary>
        PsdLinux = 107,
        /// <summary>
        /// 现地RTDB
        /// </summary>
        XiandiRtdbType = 108,
        /// <summary>
        /// 车站分机RTDB
        /// </summary>
        StationServerRtdbType = 109,
        /// <summary>
        /// 中心RTDB
        /// </summary>
        CenterRtdbType = 110,
        /// <summary>
        /// 中心车辆网关RTDB
        /// </summary>
        CenterVehicleGateWayRtdb = 111,
        /// <summary>
        /// 车站车辆网关RTDB
        /// </summary>
        StationVehicleGateWayRtdb = 112,
        /// <summary>
        /// 中央联锁工作站
        /// </summary>
        CentralCIWorkStation = 113,
        /// <summary>
        /// 组播
        Multicast = 114,
    }
}

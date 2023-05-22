namespace ExcelDataReaderTest
{
    /// <summary>
    /// 系统大类类型
    /// </summary>
    public enum SystemType : byte
    {
        /// <summary>
        /// ATS系统
        /// </summary>
        ATSType = 3,
        /// <summary>
        /// RTDB系统
        /// </summary>
        RTDB = 4,
        /// <summary>
        /// 通信中间件 区别于ATS
        /// </summary>
        CommMidSoftType = 5,
        /// <summary>
        /// 邻线ATS接口机（跨线专用接口机）
        /// </summary>
        NeighborLineAtsDvc = 6,
        /// <summary>
        /// VOBC系统
        /// </summary>
        VOBCType = 20,
        /// <summary>
        /// AOM系统
        /// </summary>
        AOMType = 21,
        /// <summary>
        /// 综合监控
        /// </summary>
        ISCSType = 22,
        /// <summary>
        /// TCC系统
        /// </summary>
        TCCType = 23,
        /// <summary>
        /// 时钟
        /// </summary>
        ClockType = 24,
        /// <summary>
        /// 无线
        /// </summary>
        RadioType = 25,
        /// <summary>
        /// 广播
        /// </summary>
        BsType = 26,
        /// <summary>
        /// 乘客
        /// </summary>
        PisType = 27,
        /// <summary>
        /// 站台TDT
        /// </summary>
        TDTType = 28,
        /// <summary>
        /// MSS系统
        /// </summary>
        MSSType = 29,
        /// <summary>
        /// ZC系统
        /// </summary>
        ZCType = 30,
        /// <summary>
        /// Power
        /// </summary>
        PowerType = 31,
        /// <summary>
        /// SCC系统
        /// </summary>
        SCCType = 32,
        /// <summary>
        /// 检修安全管理系统 
        /// </summary>
        RepairSystemType = 33,
        /// <summary>
        /// 智能检修系统
        /// </summary>
        IntelligenceRepairSystem = 34,
        /// <summary>
        /// PSCADA
        /// </summary>
        PSCADA = 35,
        /// <summary>
        /// 全局调度
        /// </summary>
        GlobalSchedul = 36,
        /// <summary>
        /// 司机派班
        /// </summary>
        DriverDispatch = 37,
        /// <summary>
        /// MDIAS
        /// </summary>
        MDIAS = 38,
        /// <summary>
        /// 车辆调
        /// </summary>
        VehicleDispatch = 39,
        /// <summary>
        /// 电流表
        /// </summary>  
        AMMETER = 40,
        /// <summary>
        /// PIS车地无线
        /// </summary> 
        PisRadio = 41,
        /// <summary>
        /// ATS接口机（厦门3用于转发邻线首末班车信息）
        /// </summary>
        ATS2ATSType = 42,
        /// <summary>
        /// DSU系统
        /// </summary>
        DSUType = 43,
        /// <summary>
        /// 车辆火灾系统
        /// </summary>
        VehicleFire = 44,
        /// <summary>
        /// 联锁维护工作站
        /// </summary>
        CIMType = 48,
        /// <summary>
        /// 联锁系统（TCT）
        /// </summary>
        CIType = 60,
        /// <summary>
        /// OC系统
        /// </summary>
        OCType = 61,
        /// <summary>
        /// KVM倒切单元
        /// </summary>
        KVMUnit = 62,
        /// <summary>
        /// 其它线路的互联接口机
        /// </summary>
        OtherLineInterfaceGateway = 66,
        /// <summary>
        /// 现地系统
        /// </summary>
        LOCALSTATIONType = 77,
        /// <summary>
        /// 智能调度
        /// </summary>
        IntelligentDispatch = 80,
        /// <summary>
        /// 联锁上位机(WL)
        /// </summary>
        上位机 = 93,
        /// <summary>
        /// 屏蔽门设备
        /// </summary>
        PsdType = 94,
        /// <summary>
        /// TCMS
        /// </summary>
        TCMSType = 96,
        /// <summary>
        /// MBAS（多线BAS，北京11）
        /// </summary>
        MBAS = 150,
        /// <summary>
        /// MSCADA（多线SCADA，北京11）
        /// </summary>
        MSCADA = 151,
    }
}

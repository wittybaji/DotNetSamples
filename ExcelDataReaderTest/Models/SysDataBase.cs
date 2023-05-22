namespace ExcelDataReaderTest
{
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
}

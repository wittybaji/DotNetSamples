using System;

namespace ExcelDataReaderTest
{
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
}

using System.Collections.Generic;
using System.Net;
using System;

namespace XmlTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MultiCastConfig config = new MultiCastConfig()
            {
                UseMultiCast = true,
                MultiCastNodeList = new List<MultiCastNode>()
                {
                    new MultiCastNode()
                    {
                        System = new SystemConn()
                        {
                            SysType = SystemType.Multicast,
                            SysName = "组播",
                            SubSysID = 0x7101,
                            SubSysName = "站场状态",
                            PurpleIPPoint = new IPEndPoint(IPAddress.Parse("239.71.1.1"), 50001),
                            YellowIPPoint = new IPEndPoint(IPAddress.Parse("239.71.1.2"), 50001),
                        },
                        SubTypes = new List<SystemSubType>()
                        {
                            SystemSubType.ApplicationServerType,
                            SystemSubType.DispatchWorkStationType,
                            SystemSubType.ScheduleWorkStationType,
                            SystemSubType.MaintainWorkStationType,
                            SystemSubType.View,
                            SystemSubType.DisplayScreen,
                            SystemSubType.FrontEndProcessType_FEP,
                            SystemSubType.RepairGatewayType,
                        }
                    },
                    new MultiCastNode()
                    {
                        System = new SystemConn()
                        {
                            SysType = SystemType.Multicast,
                            SysName = "组播",
                            SubSysID = 0x1013,
                            SubSysName = "车次追踪",
                            PurpleIPPoint = new IPEndPoint(IPAddress.Parse("239.10.13.1"), 50001),
                            YellowIPPoint = new IPEndPoint(IPAddress.Parse("239.10.13.2"), 50001),
                        },
                        SubTypes = new List<SystemSubType>()
                        {
                            SystemSubType.ApplicationServerType,
                            SystemSubType.DispatchWorkStationType,
                            SystemSubType.ScheduleWorkStationType,
                            SystemSubType.MaintainWorkStationType,
                            SystemSubType.View,
                            SystemSubType.DisplayScreen,
                            SystemSubType.FrontEndProcessType_FEP,
                            SystemSubType.RepairGatewayType,
                            SystemSubType.VehicleDispatchPlugin,
                        }
                    }
                }
            };
            SerializeXml.SerializeToXml("MultiCastConfig.xml", config);
            Console.WriteLine("OK");
        }
    }
}

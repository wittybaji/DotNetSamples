using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;

namespace ExcelDataReaderTest
{
    class Program
    {
        static void Main()
        {
            var atsDevice = GetAtsDeviceData();
            foreach (var device in atsDevice)
            {
                Console.WriteLine(device.SubSysName);
            }

            Console.WriteLine("OK");
            var intelligentDispatchDevice = GetIntelligentDispatchDeviceData();
            foreach (var device in intelligentDispatchDevice)
            {
                Console.WriteLine(device.SubSysName);
            }
            Console.WriteLine("OK");
        }

        static List<SystemConn> GetAtsDeviceData()
        {
            var result = new List<SystemConn>();

            string atsDeviceDataPath = @"D:\Desktop\设备.xls";
            if (File.Exists(atsDeviceDataPath))
            {
                DataTable dt = ReadExcel(atsDeviceDataPath, "AtsDevice", useHeaderRow: true);
                if (dt != null)
                {
                    dt.Rows.RemoveAt(0);
                    dt.Rows.RemoveAt(0);
                    var tempLinq = dt.AsEnumerable();
                    result.AddRange(tempLinq.Select(
                        x => new SystemConn()
                        {
                            ControlID = Convert.ToUInt16(x.Field<string>("F1")),
                            CIID = Convert.ToUInt16(x.Field<string>("F2")),
                            RtuID = Convert.ToUInt16(x.Field<string>("F3")),
                            StationID = Convert.ToUInt16(x.Field<string>("F4")),
                            SysType = (SystemType)Convert.ToInt32(x.Field<string>("F5")),
                            SubSysType = (SystemSubType)Convert.ToInt32(x.Field<string>("F6")),
                            SysID = Convert.ToUInt16(x.Field<string>("F7")),
                            SubSysID = Convert.ToUInt16(x.Field<string>("F8")),
                            SubSysName = x.Field<string>("F9"),
                            DirectSubSysID = Convert.ToUInt16(x.Field<string>("F34")),

                            PurpleIPPoint = GetIpEndPointFun(x, "F12", "F10"),
                            YellowIPPoint = GetIpEndPointFun(x, "F15", "F13"),
                            RedIPPoint = GetIpEndPointFun(x, "F18", "F16"),
                            BlueIPPoint = GetIpEndPointFun(x, "F21", "F19"),
                            OuterRedIPPoint = GetIpEndPointFun(x, "F24", "F22"),
                            OuterBlueIPPoint = GetIpEndPointFun(x, "F27", "F25"),
                            Direct1IPPoint = GetIpEndPointFun(x, "F30", "F28"),
                            Direct2IPPoint = GetIpEndPointFun(x, "F33", "F31"),
                        }).ToList());
                }
            }

            return result;
        }

        static List<SystemConn> GetIntelligentDispatchDeviceData()
        {
            var result = new List<SystemConn>();

            string intelligentDispatchDeviceDataPath = @"D:\Desktop\device.xlsx";
            if (File.Exists(intelligentDispatchDeviceDataPath))
            {
                DataTable dt = ReadExcel(intelligentDispatchDeviceDataPath);
                if (dt != null)
                {
                    if (dt.Columns.Count >= 10)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 3; i < dt.Rows.Count; i++)
                            {
                                DataRow row = dt.Rows[i];
                                object type = row[0];
                                if (type != null && Double.TryParse(type.ToString(), out double deviceType) && deviceType < 7)
                                {
                                    var tempLocalSystemData = new SystemConn()
                                    {
                                        SysType = SystemType.IntelligentDispatch,
                                        SubSysType = SystemSubType.IntelligentDispatch,
                                    };
                                    object subSysNameObj = row[1];
                                    object purpleNetObj = row[6];
                                    object yellowNetObj = row[9];

                                    if (subSysNameObj != null && (purpleNetObj != null || yellowNetObj != null))
                                    {
                                        string subSysName = subSysNameObj.ToString();
                                        if (!string.IsNullOrWhiteSpace(subSysName))
                                        {
                                            tempLocalSystemData.SubSysName = $"智能调度-{subSysName}";
                                        }
                                        else
                                        {
                                            Console.WriteLine("初始化智能调度数据异常：设备名称无效");
                                            continue;
                                        }

                                        if (TryGetIpEndPointFun(purpleNetObj.ToString(), out IPAddress purpleIp))
                                        {
                                            tempLocalSystemData.PurpleIPPoint = new IPEndPoint(purpleIp, 0);
                                        }
                                        else
                                        {
                                            Console.WriteLine($"初始化智能调度数据 {tempLocalSystemData.SubSysName} 紫网无效");
                                            continue;
                                        }

                                        if (TryGetIpEndPointFun(yellowNetObj.ToString(), out IPAddress yellowIp))
                                        {
                                            tempLocalSystemData.YellowIPPoint = new IPEndPoint(yellowIp, 0);
                                        }
                                        else
                                        {
                                            Console.WriteLine($"初始化智能调度数据 {tempLocalSystemData.SubSysName} 黄网无效");
                                            continue;
                                        }

                                    }
                                    else
                                    {
                                        Console.WriteLine($"初始化智能调度数据异常：设备名称或IP无效 行数 {i}");
                                        continue;
                                    }

                                    result.Add(tempLocalSystemData);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("智能调度设备配置行数小于1，不读取");
                        }
                    }
                    else
                    {
                        Console.WriteLine("智能调度设备配置列数小于10，不读取");
                    }
                }
                else
                {
                    Console.WriteLine("智能调度设备配置中无sheet");
                }
            }
            return result;
        }


        static DataTable ReadExcel(string path, string sheetName = default, int sheetIndex = 0, bool useHeaderRow = false)
        {
            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    UseColumnDataType = false,
                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = useHeaderRow
                    }
                }).ToAllStringFields();
                if (sheetName != default)
                {
                    return dataSet.Tables[sheetName];
                }
                else
                {
                    return dataSet.Tables[sheetIndex];
                }
            }
        }

        /// <summary>
        /// 获取IP和端口号
        /// </summary>
        /// <param name="x"></param>
        /// <param name="ipField"></param>
        /// <param name="portField"></param>
        /// <returns></returns>
        static IPEndPoint GetIpEndPointFun(DataRow x, string ipField, string portField)
        {
            string ip = x.Field<string>(ipField);
            if (string.IsNullOrWhiteSpace(ip) || !int.TryParse(x.Field<string>(portField), out int port))
            {
                return null;
            }
            else
            {
                return new IPEndPoint(IPAddress.Parse(ip), port);
            }
        }

        /// <summary>
        /// 获取IP和端口号
        /// </summary>
        /// <param name="x"></param>
        /// <param name="ipField"></param>
        /// <param name="portField"></param>
        /// <returns></returns>
        private static bool TryGetIpEndPointFun(string ipaddr, out IPAddress ip)
        {
            ip = default;
            try
            {
                return IPAddress.TryParse(ipaddr, out ip);
            }
            catch (Exception ex)
            {
                Console.WriteLine("初始化设备数据IP和端口异常");
                return false;
            }
        }
    }
}

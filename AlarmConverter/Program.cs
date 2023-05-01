using System;
using System.Collections.Generic;
using System.Text;

namespace AlarmConverter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("抓包右键As A HexStream");
            string pcap = "90eb030200032e0051100000013700010000300001201010140000000002002e001f00010075cf956200000000d50036000336000c00c2d6beb6d0a3d5fdcaa7b0dc20a93078";
            RTDBAlarm(pcap);
            //pcap = "6805cab38e6c98f2b308c77c08004500005d77020000801118c90310d24a0310d25ac351c3510049bc3690eb033b00033f00cea12d240132000100002b000120101058000000003b003f001a000100ce21e961000000005901020006000000210200003c02000017234800";
            //ATSAlarm(pcap);
            //pcap = "6805cab5c06998f2b308c77c08004500005d36790000801159500310d24a0310d25cc351c35100491cf390eb033b00034200dfc62a240132000100002b000120101058000000003b0042001a000100ce21e961000000005901020006000000210200003c0200003f83392e";
            //ATSAlarm(pcap);
            //RTDBAlarm(pcap);

            Console.ReadKey();
        }


        public static DateTime CalcuTime(UInt64 timeSpan)
        {
            DateTime standard = new DateTime(1970, 1, 1, 0, 0, 0);
            standard = standard.AddHours(TimeZoneInfo.Local.BaseUtcOffset.TotalHours);
            return standard.AddSeconds(timeSpan);
        }

        public static void RTDBAlarm(string pcap)
        {
            //取数据域

            //pcap = pcap.Substring(124);// 抓包右键As A HexStream
            //pcap = pcap.Substring(154);// 抓包右键As A HexStream
            pcap = pcap.Substring(70);// Data 右键As A HexStream
            Console.WriteLine(pcap);
            List<byte> pDataList = new List<byte>();
            for (int i = 0; i < pcap.Length; i += 2)
            {
                pDataList.Add(Byte.Parse(pcap[i].ToString() + pcap[i + 1].ToString(), System.Globalization.NumberStyles.HexNumber));
            }
            byte[] pData = pDataList.ToArray();
            int index = 2;
            //报警时间 8个字节
            ulong totalSeconds = BitConverter.ToUInt64(pData, index);
            Console.WriteLine("报警时间：" + CalcuTime(totalSeconds));
            index += 8;

            //报警类型编号，2字节
            ushort AlarmTypeId = BitConverter.ToUInt16(pData, index);
            Console.WriteLine("报警类型：" + AlarmTypeId);
            index += 2;

            //报警源，报警参数ID，2字节
            ushort AlarmParaId = BitConverter.ToUInt16(pData, index);
            Console.WriteLine("报警源：" + AlarmParaId);
            index += 2;

            //报警设备类型ID，1字节
            byte AlarmDvcTypeId = pData[index++];
            Console.WriteLine("报警设备类型：" + AlarmDvcTypeId);

            //报警设备ID，2字节
            ushort AlarmDvcId = BitConverter.ToUInt16(pData, index);
            Console.WriteLine("报警设备ID：" + AlarmDvcId);
            index += 2;

            //报警内容长度 2字节
            short alarmContentLen = BitConverter.ToInt16(pData, index);
            Console.WriteLine("报警内容长度：" + alarmContentLen);
            index += 2;
            string AlarmDetail = Encoding.Default.GetString(pData, index, alarmContentLen);//Encoding.UTF8.GetString(pData, index, alarmContentLen);
            Console.WriteLine("报警内容：" + AlarmDetail);
            Console.ReadKey();
        }

        public static void ATSAlarm(string pcap)
        {
            pcap = pcap.Substring(154);
            Console.WriteLine(pcap);
            List<byte> pDataList = new List<byte>();
            for (int i = 0; i < pcap.Length; i += 2)
            {
                pDataList.Add(Byte.Parse(pcap[i].ToString() + pcap[i + 1].ToString(), System.Globalization.NumberStyles.HexNumber));
            }
            byte[] pData = pDataList.ToArray();
            int index = 0;
            //报警总数，2字节
            ushort alarmCount = BitConverter.ToUInt16(pData, index);
            Console.WriteLine("报警总数：" + alarmCount);
            index += 2;
            for (int i = 0; i < alarmCount; i++)
            {
                //报警时间 8个字节
                ulong totalSeconds = BitConverter.ToUInt64(pData, index);
                Console.WriteLine("报警时间：" + CalcuTime(totalSeconds));
                index += 8;

                //报警类型编号，2字节
                ushort AlarmTypeId = BitConverter.ToUInt16(pData, index);
                Console.WriteLine("报警类型：" + AlarmTypeId);
                index += 2;

                //报警源，报警参数ID，2字节
                ushort AlarmParaId = BitConverter.ToUInt16(pData, index);
                Console.WriteLine("报警源：" + AlarmParaId);
                index += 2;
                for (int j = 0; j < AlarmParaId; j++)
                {
                    //报警参数，4字节
                    uint alarmParaId = BitConverter.ToUInt32(pData, index);
                    Console.WriteLine("报警参数" + j + "：" + alarmParaId);
                    index += 4;
                }

                //报警设备类型，1字节
                byte AlarmDvcTypeId = pData[index++];
                Console.WriteLine("报警设备类型：" + AlarmDvcTypeId);

                //报警设备ID 2字节
                short alarmContentLen = BitConverter.ToInt16(pData, index);
                Console.WriteLine("报警设备ID：" + alarmContentLen);
                index += 2;

                //产生恢复
                byte generate = pData[index++];
                Console.WriteLine("产生恢复：" + generate);
            }
        }
    }
}

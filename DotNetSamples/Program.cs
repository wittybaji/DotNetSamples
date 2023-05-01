using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DotNetSamples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(TimeZoneInfo.Local.BaseUtcOffset.TotalHours);

            Console.WriteLine(0b01);
            Console.WriteLine(0b10);
            byte[] data = new byte[4] { 0x00, 0x00, 0xF0, 0x41 };
            var stopSeconds = BitConverter.ToSingle(data, 0);
            Console.WriteLine(stopSeconds);
            Console.ReadKey();
            List<int> rawData = new List<int>() { 1, 2, 3, 4 };
            int x = 12;
            int y = x;
            Console.WriteLine(x);
            var z = ChangeParam(ref x, rawData);
            Console.WriteLine(x);
            Console.WriteLine(String.Join("-", z));
            Console.WriteLine(String.Join("-", rawData));
            var z2 = ChangeParam(ref y, rawData);
            Console.WriteLine(y);
            Console.WriteLine(String.Join("-", z2));
            Console.WriteLine(String.Join("-", rawData));
            return;
            Fault f = new Fault();
            Console.WriteLine(f.Valid);

            Console.WriteLine("UInt16 " + UInt16.MinValue + "-" + UInt16.MaxValue);
            Console.WriteLine("UInt32 " + UInt32.MinValue + "-" + UInt32.MaxValue);

            List<Fault> times = new List<Fault>
            {
                new Fault() { Id = 1, Time = DateTime.Now.ToString() },
                new Fault() { Id = 2, Time = DateTime.Now.AddSeconds(30).ToString() },
                new Fault() { Id = 3, Time = DateTime.Now.AddSeconds(5).ToString() }
            };

            times = times.OrderByDescending(t => t.Time).ToList();
            foreach (var item in times)
            {
                Console.WriteLine(item.Id);
                Console.WriteLine(item.Time);
            }


            Console.ReadLine();
        }

        static List<int> ChangeParam(ref int a, List<int> b)
        {
            a = 10;
            b.Add(6);
            return b;
        }

        void Md5()
        {
            string str = "asefergaesdfserfawdfsfaw";
            string md5 = "";
            using (MD5 md5Hash = MD5.Create())
            {
                // 将输入字符串转换为byte数组并计算哈希值
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(str));
                StringBuilder sBuilder = new StringBuilder();
                // 修改格式
                foreach (var t in data)
                {
                    sBuilder.Append(t.ToString("x2"));
                }
                md5 = sBuilder.ToString();
            }
            Console.WriteLine(md5);
        }
    }

    public class Fault
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public bool Valid { get; set; }
    }
}

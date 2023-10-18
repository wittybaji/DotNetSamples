using System;
using System.Text;

namespace TimeSpanTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                StringBuilder tsStr =  new StringBuilder();
                var ts = TimeSpan.FromSeconds(36000);

                if (ts.Days > 0)
                {
                    tsStr.Append($"{ts.Days}天");
                }
                if (ts.Hours > 0)
                {
                     tsStr.Append($"{ts.Hours}小时");
                }
                tsStr.Append($"{ts.Minutes}分钟");
                tsStr.Append($"{ts.Seconds}秒");
                Console.WriteLine(tsStr);

                DateTime dateTime = DateTime.Now;
                dateTime.ToString("HH:mm:ss");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

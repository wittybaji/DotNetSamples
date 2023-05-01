using System;
using System.Text;

namespace TimeConverter
{
    internal class Program
    {
        private static TimeZoneInfo localTimeZone = TimeZoneInfo.Local;
        static void Main(string[] args)
        {
            //string[] times = { "00-0A-EF-60", "53-A6-66-5F" , "33-43-A2-5F" };
            string[] times = { "00-0A-EF-60", "60-EF-0A-00" };

            foreach (var item in times)
            {
                Console.WriteLine(CalculateTime(item));
            }

            Console.ReadLine();
        }

        public static string CalculateTime(string res)
        {
            string time = "";
            if (!String.IsNullOrWhiteSpace(res))
            {
                byte[] pData = Encoding.Default.GetBytes(res);
                foreach (var data in pData)
                {
                    Console.WriteLine(data);
                }
                var timeSpan = BitConverter.ToUInt32(pData, 0);
                if (timeSpan == 0)
                {
                    time = "";
                }
                else
                {
                    Console.WriteLine("timeSpan-" + timeSpan);
                    DateTime standard = new DateTime(1970, 1, 1, 0, 0, 0);
                    standard = standard.AddHours(localTimeZone.BaseUtcOffset.TotalHours);
                    time = standard.AddSeconds(timeSpan).ToString();
                }
            }
            return time;
        }
    }
}

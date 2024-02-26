using System;
using System.Text;

namespace TimeConverter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //string[] times = { "00-0A-EF-60", "53-A6-66-5F" , "33-43-A2-5F" };
            //string[] times = { "00-0A-EF-60", "60-EF-0A-00" };

            //foreach (var item in times)
            //{
            //    Console.WriteLine(CalculateTime(item));
            //}

            //var maxTs = TimeSpan.FromSeconds(UInt32.MaxValue);
            //Console.WriteLine(maxTs.Days);
            //var maxTime = CalCurrenTime(UInt32.MaxValue);
            //Console.WriteLine(maxTime.Year);
            //var minTime = CalCurrenTime(UInt32.MinValue);
            //Console.WriteLine(minTime.Year);

            //var maxTimeSpan = CalTimeSpan(new DateTime(2023, 11, 11, 22, 30, 00));
            //uint maxSec = Convert.ToUInt32(maxTimeSpan.TotalSeconds);
            //Console.WriteLine(maxSec);

            //var minTimeSpan = CalTimeSpan(new DateTime(0001, 1, 1, 0, 0, 0));
            //uint minSec = Convert.ToUInt32(minTimeSpan.TotalSeconds);
            //Console.WriteLine(minSec);

            //DateTime baseTime = new DateTime(1970, 1, 1, 0, 0, 0).AddHours(TimeZoneInfo.Local.BaseUtcOffset.TotalHours);
            DateTime currenTime = default;
            Console.WriteLine(CalTimeSpan(currenTime).TotalSeconds);
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
                time = CalCurrenTime(timeSpan).ToString();
            }
            return time;
        }


        public static TimeSpan CalTimeSpan(DateTime currenTime)
        {
            if (currenTime < new DateTime(1970, 1, 1, 0, 0, 0))
            {
                return new TimeSpan();
            }
            else
            {
                DateTime standard = new DateTime(1970, 1, 1, 0, 0, 0);
                TimeZoneInfo localTimeZone = TimeZoneInfo.Local;
                standard = standard.AddHours(localTimeZone.BaseUtcOffset.TotalHours);
                var timeSpan = currenTime.Subtract(standard);
                if (timeSpan.TotalSeconds > 0)
                    return currenTime.Subtract(standard);
                else return new TimeSpan();
            }
        }

        public static DateTime CalCurrenTime(UInt64 timeSpan)
        {
            if (timeSpan == 0)
            {
                return new DateTime();
            }
            else
            {
                DateTime standard = new DateTime(1970, 1, 1, 0, 0, 0);
                TimeZoneInfo localTimeZone = TimeZoneInfo.Local;
                standard = standard.AddHours(localTimeZone.BaseUtcOffset.TotalHours);
                return standard.AddSeconds(timeSpan);
            }
        }

    }
}

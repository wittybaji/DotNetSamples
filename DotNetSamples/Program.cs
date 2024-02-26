using System;
using System.Linq;
using System.Text;

namespace DotNetSamples
{
    internal class Program
    {
        static void Main()
        {
            DateTime time = DateTime.Now;
            long timestamp = new DateTimeOffset(time).ToUnixTimeSeconds();
            Console.WriteLine(timestamp);

            DateTime dateTime = DateTimeOffset.FromUnixTimeSeconds(timestamp).LocalDateTime;
            Console.WriteLine(dateTime);


            string log = "E5-BA-94-E7-94-A8-E6-9C-8D-E5-8A-A1-E5-99-A8-E7-AD-89-E9-97-B4-E9-9A-94-E8-B0-83-E5-9B-BE-E6-97-B6-E5-BC-82-E5-B8-B8-EF-BC-8C-E6-97-A0-E6-B3-95-E7-A1-AE-E5-AE-9A-E5-88-97-E8-BD-A6-20-31-33-31-34-20-E8-BF-90-E8-A1-8C-E8-B7-AF-E5-BE-84-";

            var testList = log.Split('-').Where(x => !string.IsNullOrWhiteSpace(x));
            var byteList = testList.Select(x => byte.Parse(x, System.Globalization.NumberStyles.HexNumber)).ToArray();
            string msg = Encoding.UTF8.GetString(byteList);
            Console.WriteLine(msg);

            DateTime tempArrTime = new DateTime();//到站时间
            bool res = tempArrTime == new DateTime();
            Console.WriteLine(res);

            Console.WriteLine(TimeZoneInfo.Local.BaseUtcOffset.TotalHours);

            Console.ReadLine();
        }
    }
}

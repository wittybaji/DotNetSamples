using System;

namespace TimeSpanTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string tsStr = default;
                var ts = TimeSpan.FromSeconds(3600);
                if (ts.Hours > 0)
                {
                     tsStr = ts.ToString(@"h\时m\分s\秒");
                }
                else
                {
                    tsStr = ts.ToString(@"m\分s\秒");
                }
                Console.WriteLine(tsStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

using System;

namespace ByteTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ushort trainID = 1301;
            ushort zoneID = 24;
            byte direction = 0x55;

            ulong key = (ulong)trainID << 24 | (ulong)zoneID << 8 | direction;

            var result = key.ToString("X2");
            Console.WriteLine(result);







            Console.WriteLine("Hello World!");
            byte res = 0;
            res |= 2;
            Console.WriteLine(res);
            res &= 1;
            Console.WriteLine(res);

            res = 1;
            res |= 2;
            Console.WriteLine(res);
            res &= 1;
            Console.WriteLine(res);

            res = 0;
            res |= 1;
            Console.WriteLine(res);
            res &= 2;
            Console.WriteLine(res);

            res = 2;
            res |= 1;
            Console.WriteLine(res);
            res &= 2;
            Console.WriteLine(res);
        }
    }
}

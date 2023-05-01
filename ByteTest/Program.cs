using System;

namespace ByteTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
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

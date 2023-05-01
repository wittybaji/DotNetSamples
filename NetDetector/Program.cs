using System;
using System.Threading;

namespace NetDetector
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            while (true)
            {
                bool isNetConn = NetDetector.IsConnectedInternet(out string connectionType);
                Console.WriteLine(isNetConn);
                Thread.Sleep(1000);
            }
        }
    }
}

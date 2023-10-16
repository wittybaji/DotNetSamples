using System;
using System.Threading;

namespace TaskTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CancellationTask task = new CancellationTask();
            task.Run();
            Thread.Sleep(5000);
            task.Cancel();
            Console.ReadLine();
        }
    }
}

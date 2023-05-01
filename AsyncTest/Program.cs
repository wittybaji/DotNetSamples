using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncTest
{
    internal class Program
    {
        static CancellationTokenSource tokenSource = new CancellationTokenSource();
        static async Task Main(string[] args)
        {
            Console.WriteLine("Application started.");
            try
            {
                tokenSource.CancelAfter(3000);
                await AsyncTask();
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Tasks cancelled: timed out.");
            }
            finally
            {
                tokenSource.Dispose();
            }
            Console.WriteLine("Application ending.");
            Console.ReadKey();
        }

        static async Task AsyncTask()
        {
            while (true)
            {
                if (tokenSource.Token.IsCancellationRequested == true)
                {
                    //throw new OperationCanceledException("Operation Time Not Enough");
                    break;
                }
                Thread.Sleep(1000);
                Console.WriteLine($"AsyncTask{DateTime.Now}");
            }
        }
    }
}

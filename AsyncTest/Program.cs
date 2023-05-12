using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncTest
{
    internal class Program
    {
        static readonly CancellationTokenSource tokenSource = new CancellationTokenSource();
        static async Task Main()
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
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri("https://www.thunderclient.com/welcome"),
                    Method = HttpMethod.Get
                };

                request.Headers.Add("Accept", "*/*");
                request.Headers.Add("User-Agent", "Thunder Client (https://www.thunderclient.com)");

                var response = await client.SendAsync(request);
                var result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);
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

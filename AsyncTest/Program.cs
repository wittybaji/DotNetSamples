using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncTest
{
    internal class Program
    {
        static readonly CancellationTokenSource _tokenSource = new CancellationTokenSource();

        private static void Main()
        {
            Console.WriteLine("Application started.");
            try
            {
                Task.Run(AsyncTask);
                Console.ReadKey();
                _tokenSource.CancelAfter(3000);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Tasks cancelled: timed out.");
            }
            finally
            {
                _tokenSource.Dispose();
            }
            Console.WriteLine("Application ending.");
            Console.ReadLine();
        }

        static async Task AsyncTask()
        {
            while (true)
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri("https://v1.hitokoto.cn"),
                    Method = HttpMethod.Get
                };

                request.Headers.Add("Accept", "*/*");
                request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/113.0.0.0 Safari/537.36");

                var response = await client.SendAsync(request);
                var result = await response.Content.ReadAsStringAsync();
                var hitokoto = Newtonsoft.Json.JsonConvert.DeserializeObject<Hitokoto>(result);
                Console.WriteLine(hitokoto.hitokoto);
                if (_tokenSource.Token.IsCancellationRequested)
                {
                    //throw new OperationCanceledException("Operation Time Not Enough");
                    break;
                }
                Console.WriteLine($"BeforeSleep{DateTime.Now}");
                Thread.Sleep(1000);
                Console.WriteLine($"AfterSleep{DateTime.Now}");

                Console.WriteLine($"BeforeDelay{DateTime.Now}");
                await Task.Delay(1000);
                Console.WriteLine($"AfterDelay{DateTime.Now}");

                Console.WriteLine($"AsyncTask{DateTime.Now}");
            }
        }

        private class Hitokoto
        {
            public string hitokoto { get; set; }
            public string from { get; set; }
        }

    }
}

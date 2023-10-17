using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task.Run(Request);
            Console.ReadLine();
        }

        static async void Request()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://v1.hitokoto.cn");
            request.Method = HttpMethod.Get;

            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3100.0 Safari/537.36");

            var response = await client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
        }
    }
}

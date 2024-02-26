using System;
using System.Net;
using System.Text;
using TouchSocket.Core;
using TouchSocket.Sockets;

namespace TouchSocketTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TcpService service = new TcpService();
            service.Connecting = (client, e) => { Console.WriteLine($"有客户端正在连接:{client.GetIPPort()}"); };//有客户端正在连接
            service.Connected = (client, e) => { Console.WriteLine($"有客户端连接:{client.GetIPPort()}"); };//有客户端连接
            service.Disconnected = (client, e) => { Console.WriteLine($"有客户端断开连接:{client.GetIPPort()}"); };//有客户端断开连接

            service.Received = (client, byteBlock, req) =>
            {
                //从客户端收到信息
                string mes = Encoding.UTF8.GetString(byteBlock.Buffer, 0, byteBlock.Len);
                client.Logger.Info($"已从{client.ID}接收到信息：{mes}");
            };

            service.Setup(new TouchSocketConfig().SetListenIPHosts(new IPHost[] { new IPHost(IPAddress.Parse("5.16.51.132"), 50580) }));
            service.Start();//启动



            Console.ReadLine();

        }
    }
}

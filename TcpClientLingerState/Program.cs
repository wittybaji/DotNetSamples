using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace TcpClientLingerState
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IEnumerable<string> ipList = default;
                string hostNmae = Dns.GetHostName();
                IPHostEntry ipEntry = Dns.GetHostEntry(hostNmae);
                ipList = ipEntry.AddressList
                    .Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork && !Equals(ip, IPAddress.Loopback))
                    .Select(x => x.ToString());
                var bindip = ipList.First();
                var client = new System.Net.Sockets.TcpClient(new IPEndPoint(IPAddress.Parse(bindip), 40000));
                Console.WriteLine($"Enabled:{client.LingerState.Enabled} LingerTime:{client.LingerState.LingerTime}");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"IP获取异常 {ex.Message}{Environment.NewLine}{ex.StackTrace}");
            }

        }
    }
}

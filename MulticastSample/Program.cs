using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MulticastSample
{
    internal class Program
    {
        static bool _disposedValue = false;
        static int _maxByteLength = 1024;
        static void Main(string[] args)
        {
            IPAddress localIp = IPAddress.Parse("5.16.210.70");
            IPAddress multicastAddress = IPAddress.Parse("239.71.1.1");
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(new IPEndPoint(localIp, 2000));
            var multicastOption = new MulticastOption(multicastAddress, localIp);
            socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, multicastOption);

            Task.Run(() =>
            {
                var bytes = new byte[_maxByteLength];
                EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                try
                {
                    while (!_disposedValue)
                    {
                        var length = socket.ReceiveFrom(bytes, ref remoteEndPoint);
                        Console.WriteLine(Encoding.UTF8.GetString(bytes, 0, length));

                        //socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, multicastOption);
                        // 获取当前加入的组播组
                        var allInterfaces = NetworkInterface.GetAllNetworkInterfaces();
                        var interfaceProperties = allInterfaces
                            .Where(n => n.SupportsMulticast && n.OperationalStatus == OperationalStatus.Up)
                            .Select(n => n.GetIPProperties());
                        foreach (var interfaceProperty in interfaceProperties)
                        {
                            if (interfaceProperty.UnicastAddresses.Any(x => x.Address.Equals(localIp)))
                            {
                                Console.WriteLine($"Group Address: {Environment.NewLine}" +
                                    $"{string.Join(Environment.NewLine, interfaceProperty.MulticastAddresses.Select(x => x.Address))}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            Console.ReadLine();
        }
    }
}

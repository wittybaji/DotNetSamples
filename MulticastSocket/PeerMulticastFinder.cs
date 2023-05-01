using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MulticastSocket
{
    internal class PeerMulticastFinder : IDisposable
    {
        /// <summary>
        /// 收到消息
        /// </summary>
        public event Action<string> ReceivedMessage;
        /// <summary>
        /// 组播地址
        /// <para/>
        /// 224.0.0.0～224.0.0.255为预留的组播地址（永久组地址），地址224.0.0.0保留不做分配，其它地址供路由协议使用；
        /// <para/>
        /// 224.0.1.0～224.0.1.255是公用组播地址，可以用于Internet；
        /// <para/>
        /// 224.0.2.0～238.255.255.255为用户可用的组播地址（临时组地址），全网范围内有效；
        /// <para/>
        /// 239.0.0.0～239.255.255.255为本地管理组播地址，仅在特定的本地范围内有效。
        /// </summary>
        public IPAddress MulticastAddress { set; get; } = IPAddress.Parse("234.0.0.1");

        public int MulticastPort = 15002;

        private const int MaxByteLength = 1024;
        public IPAddress LocalIpAddress { set; get; } = IPAddress.Parse("5.16.220.70");

        private Socket MulticastSocket { get; }


        public PeerMulticastFinder()
        {
            MulticastSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }

        /// <summary>
        /// 寻找局域网设备
        /// </summary>
        public void FindPeer()
        {
            // 实际是反过来，让其他设备询问
            StartMulticast();
            var ipList = GetLocalIpList().ToList();
            var message = string.Join(";", ipList);
            SendBroadcastMessage(message);
            // 先发送再获取消息，这样就不会收到自己发送的消息
            //ReceivedMessage += (msg) => { Console.WriteLine($"发送 {msg}"); };

        }

        /// <summary>
        /// 获取本地 IP 地址
        /// </summary>
        /// <returns></returns>
        private IEnumerable<IPAddress> GetLocalIpList()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    yield return ip;
                }
            }
        }

        /// <summary>
        /// 启动组播
        /// </summary>
        public void StartMulticast()
        {
            try
            {
                // 如果首次绑定失败，那么将无法接收，但是可以发送
                TryBindSocket();

                // Define a MulticastOption object specifying the multicast group 
                // address and the local IPAddress.
                // The multicast group address is the same as the address used by the server.
                // 有多个 IP 时，指定本机的 IP 地址，此时可以接收到具体的内容
                var multicastOption = new MulticastOption(MulticastAddress, LocalIpAddress);

                MulticastSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, multicastOption);
            }
            catch (Exception ex)
            {
                LogEx(ex);
            }

            Task.Run(ReceiveBroadcastMessages);
        }


        private void ReceiveBroadcastMessages()
        {
            // 接收需要绑定 MulticastPort 端口
            var bytes = new byte[MaxByteLength];
            EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

            try
            {
                while (!_disposedValue)
                {
                    var length = MulticastSocket.ReceiveFrom(bytes, ref remoteEndPoint);

                    OnReceivedMessage(Encoding.UTF8.GetString(bytes, 0, length));
                }
            }
            catch (Exception ex)
            {
                LogEx(ex);
            }
        }


        /// <summary>
        /// 发送组播
        /// </summary>
        /// <param name="message"></param>
        public void SendBroadcastMessage(string message)
        {
            try
            {
                var endPoint = new IPEndPoint(MulticastAddress, MulticastPort);
                var byteList = Encoding.UTF8.GetBytes(message);

                if (byteList.Length > MaxByteLength)
                {
                    throw new ArgumentException($"传入 message 转换为 byte 数组长度太长，不能超过{MaxByteLength}字节")
                    {
                        Data =
                        {
                            { "message", message },
                            { "byteList", byteList }
                        }
                    };
                }

                MulticastSocket.SendTo(byteList, endPoint);
            }
            catch (Exception ex)
            {
                LogEx(ex);
            }
        }

        private void TryBindSocket()
        {
            for (var i = MulticastPort; i < 15010; i++)
            {
                try
                {
                    EndPoint localEndPoint = new IPEndPoint(LocalIpAddress, i);

                    MulticastSocket.Bind(localEndPoint);
                    return;
                }
                catch (SocketException ex)
                {
                    LogEx(ex);
                }
            }
        }


        #region IDisposable Support

        private bool _disposedValue = false;

        private void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                }

                MulticastSocket.Dispose();

                ReceivedMessage = null;
                MulticastAddress = null;

                _disposedValue = true;
            }
        }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        private void OnReceivedMessage(string e)
        {
            ReceivedMessage?.Invoke(e);
        }


        /// <summary>
        /// 控制台输出异常
        /// </summary>
        /// <param name="ex"></param>
        private static void LogEx(Exception ex)
        {
            Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MulticastSocket
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("输入本机IP");
            string localIp = Console.ReadLine();
            PeerMulticastFinder peerMulticastFinder = new PeerMulticastFinder();
            peerMulticastFinder.LocalIpAddress = IPAddress.Parse(localIp);
            peerMulticastFinder.FindPeer();
            peerMulticastFinder.ReceivedMessage += ReceivedMessage;

            while (true)
            {
                //peerMulticastFinder.SendBroadcastMessage(DateTime.Now.ToString());
                //Console.WriteLine("广播：");
                string msg = Console.ReadLine();
                peerMulticastFinder.SendBroadcastMessage(msg);
                Thread.Sleep(1000);
            }
        }

        private static void ReceivedMessage(string msg)
        {
            Console.WriteLine("收到：" + msg);
        }
    }
}

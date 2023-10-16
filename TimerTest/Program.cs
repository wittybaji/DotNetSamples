using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TimerTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var client = new Client()
            //{
            //    LastMessageReceivedTime = DateTime.Now.AddSeconds(1),
            //    IsConnected = true
            //};
            //int delay = 5000;
            //var _receiveTimeoutTimer = new ClientReceiveTimeoutTimer(client, delay, () => Console.WriteLine("断开"))
            //{
            //    Enabled = true
            //};

            TimeoutTimerTest();
            //DaemonTimerTest();

            Console.ReadLine();

        }

        private static void TimeoutTimerTest()
        {
            TimeoutTimer timer = new TimeoutTimer(3000, () => { Console.WriteLine("TimeoutTimer Elapsed"); });
            for (int i = 0; i < 20; i++)
            {
                timer.Reset(3000);
                Thread.Sleep(200);
            }

            Console.ReadLine();
            for (int i = 0; i < 10; i++)
            {
                timer.Reset(3000);
                Thread.Sleep(200);
            }

            Console.ReadLine();
        }


        private static void DaemonTimerTest()
        {
            DaemonTimer timer = new DaemonTimer();
            while (true)
            {
                timer.Start();
                Thread.Sleep(200);
            }
        }

    }
}

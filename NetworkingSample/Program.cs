using System;
using System.Net.NetworkInformation;

namespace NetworkingSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NetworkChange.NetworkAddressChanged += new
            NetworkAddressChangedEventHandler(AddressChangedCallback);
            Console.WriteLine("Listening for address changes. Press any key to exit.");
            Console.ReadLine();
        }

        static void AddressChangedCallback(object sender, EventArgs e)
        {
            Console.WriteLine("*******************************************");
            Console.WriteLine(DateTime.Now);
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface n in adapters)
            {
                Console.WriteLine("   {0} is {1}", n.Name, n.OperationalStatus);
            }
        }
    }
}

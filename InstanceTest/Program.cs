using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstanceTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Now);
            System.Threading.Thread.Sleep(5000);
            InstanceTest_1.Instance.Log();
            InstanceTest_1.Instance.Log();
            InstanceTest_1.Instance.Log();
            Console.WriteLine("---------------------");
            InstanceTest_2.Instance.Log();
            InstanceTest_2.Instance.Log();
            InstanceTest_2.Instance.Log();
            Console.WriteLine("---------------------");
            InstanceTest_3.Instance.Log();
            InstanceTest_3.Instance.Log();
            InstanceTest_3.Instance.Log();
        }
    }
}

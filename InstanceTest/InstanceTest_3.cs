using System;

namespace InstanceTest
{
    internal class InstanceTest_3
    {
        public static InstanceTest_3 Instance = new Lazy<InstanceTest_3>().Value;

        private int Flag = 0;
        private DateTime CreateTime = new DateTime(1, 1, 1);
        public InstanceTest_3()
        {
            Console.WriteLine("创建 InstanceTest_3");
            Flag = new Random().Next(1, 999);
            CreateTime = DateTime.Now;
        }

        public void Log()
        {
            Console.WriteLine("Hello!" + Flag + CreateTime);
        }
    }
}

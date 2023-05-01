using System;

namespace InstanceTest
{
    internal class InstanceTest_1
    {
        private static InstanceTest_1 instance = null;

        private int Flag = 0;
        private DateTime CreateTime = new DateTime(1, 1, 1);
        static InstanceTest_1()
        {
            instance = new InstanceTest_1();
            Console.WriteLine("创建 InstanceTest_1");
            instance.Flag = new Random().Next(1, 999);
            instance.CreateTime = DateTime.Now;
        }

        public static InstanceTest_1 Instance
        {
            get { return instance; }
        }

        public void Log()
        {
            Console.WriteLine("Hello!" + Flag + CreateTime);
        }
    }
}

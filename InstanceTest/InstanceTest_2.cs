using System;

namespace InstanceTest
{
    internal class InstanceTest_2
    {
        public static InstanceTest_2 Instance => InnerClass.instance;

        private class InnerClass
        {
            internal static readonly InstanceTest_2 instance = new InstanceTest_2();
            static InnerClass() { }
        }

        private int Flag = 0;
        private DateTime CreateTime = new DateTime(1, 1, 1);
        private InstanceTest_2()
        {
            Console.WriteLine("创建 InstanceTest_2");
            Flag = new Random().Next(1, 999);
            CreateTime = DateTime.Now;
        }


        public void Log()
        {
            Console.WriteLine("Hello!" + Flag + CreateTime);
        }
    }
}

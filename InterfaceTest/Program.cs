using System;

namespace InterfaceTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ITestInterface test = new TestClass();
            test.Foo(123, "wer");
            Console.ReadLine();
        }
    }


    interface ITestInterface
    {
        void Foo(int id, string name = null);
    }

    class TestClass : ITestInterface
    {
        public TestClass()
        {
        }

        public void Foo(int id, string name = null)
        {
            Console.WriteLine($"ID:{id}");
            if (name != null)
            {
                Console.WriteLine(name);
            }
        }
    }
}

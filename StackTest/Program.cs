using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PrintStack();
            Console.ReadLine();
        }
        private static void PrintStack()
        {
            var stack = new System.Diagnostics.StackTrace();
            Console.WriteLine(stack);
        }
    }
}

using System;
using System.Collections.Generic;

namespace ForeachTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> testList = new List<int>();
            Console.WriteLine(testList == null);
            foreach (var item in testList)
            {
                Console.WriteLine(item);
            }
            testList.Add(1);
            var resultList = testList.FindAll(t => t > 5);
            Console.WriteLine(resultList == null);
            foreach (var item in resultList)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }
    }
}

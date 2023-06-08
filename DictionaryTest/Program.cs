using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace DictionaryTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var testDic = new ConcurrentDictionary<ushort, List<ushort>>();
            testDic.TryAdd(2, new List<ushort>() { 3, 4 });

            ushort testKey = 1;
            if (testDic.TryGetValue(testKey, out var tempList))
            {
                Console.WriteLine(tempList.Count);
            }
            else
            {
                tempList = new List<ushort> { 2 };
                testDic.TryAdd(testKey, tempList);
            }

            Console.WriteLine(testDic.ContainsKey(testKey));

            testKey = 2;
            if (testDic.TryGetValue(testKey, out var tempList2))
            {
                Console.WriteLine(tempList2.Count);
                tempList2 = new List<ushort> { 5 };
            }
            else
            {
                tempList2 = new List<ushort> { 2 };
                testDic.TryAdd(testKey, tempList2);
            }

            Console.WriteLine(testDic.ContainsKey(testKey));

            testDic[3] = new List<ushort>();

            Console.WriteLine(testDic.Count);
        }
    }
}

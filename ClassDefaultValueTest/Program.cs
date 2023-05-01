using System;
using System.Collections.Concurrent;

namespace ClassDefaultValueTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ConcurrentDictionary<int, Train> test = new ConcurrentDictionary<int, Train>();
            test.AddOrUpdate(1, new Train() { TrainId = 1, Valid = true }, (key, value) => { return new Train() { TrainId = 1, Valid = true }; });

            if (test.TryGetValue(2, out var train2))
            {
                Console.WriteLine(train2 == null);
            }
            if (test.TryGetValue(1, out var train1))
            {
                Console.WriteLine(train1 == null);
            }
        }
    }

    class Train
    {
        public int TrainId { get; set; }

        public bool Valid { get; set; }
    }
}

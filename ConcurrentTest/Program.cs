using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;

namespace ConcurrentTest
{
    internal class Program
    {
        //public static List<test> testList = new List<test>();
        //public static ConcurrentBag<test> testBag = new ConcurrentBag<test>();
        public static ConcurrentDictionary<string, string> dcTest = new ConcurrentDictionary<string, string>();
        static IDatabase db;
        static void Main(string[] args)
        {
            RedisConnection.Init("localhost:56379");
            var redis = RedisConnection.Instance.ConnectionMultiplexer;
            db = redis.GetDatabase(0);
            //for (int i = 1; i < 555555; i++)
            //{
            //    //testList.Add(new test() { number = i, numberStr = i.ToString()});
            //    //testBag.Add(new test() { number = i, numberStr = i.ToString() });
            //    dcTest.AddOrUpdate(i.ToString(), i.ToString(), (key, value) => { return value; });
            //}
            //Thread changeThread = new Thread(new ThreadStart(Change));
            //changeThread.Start();
            Thread addThread1 = new Thread(new ThreadStart(AddThreasd));
            addThread1.Start();
            //Thread addThread2 = new Thread(new ThreadStart(AddThreasd));
            //addThread2.Start();
            //Thread addThread3 = new Thread(new ThreadStart(RedisAdd));
            //addThread3.Start();
            Thread addThread4 = new Thread(new ThreadStart(RedisAdd));
            addThread4.Start();

            while (true)
            {
                //for (int i = 1; i < testList.Count; i++)
                //{
                //    var result = testList.FirstOrDefault(t => t.number == i);
                //    var result = testBag.FirstOrDefault(t => t.number == i);
                //    var result = testBag.Last();
                //    if (result != null)
                //    {
                //        Console.WriteLine(result.numberStr);
                //    }
                //    result = testBag.First();
                //    if (result != null)
                //    {
                //        Console.WriteLine(result.numberStr);
                //    }
                //}
                Thread.Sleep(500);
            }
        }

        public static void Change()
        {
            while (true)
            {
                Random r = new Random();
                //var num = r.Next(1, testList.Count);
                //testList[num] = new test() { number = r.Next(222, 66666), numberStr = r.Next(222, 66666).ToString()};
                //var tempbag = testBag.Last();
                //tempbag = new test() { number = r.Next(222, 66666), numberStr = r.Next(222, 66666).ToString() };
            }
        }

        public static void AddThreasd()
        {
            //while (true)
            //{
            //    Random r = new Random();
            //    //var num = r.Next(1, testList.Count);
            //    //testBag.Add(new test() { number = r.Next(222, 66666), numberStr = r.Next(222, 66666).ToString() });

            //    Thread.Sleep(100);
            //}
            Stopwatch watchDcWrite = new Stopwatch();
            watchDcWrite.Start();
            for (int i = 0; i < 10000; i++)
            {
                string str = "Hello World!" + DateTime.Now.Ticks;
                dcTest.AddOrUpdate(i.ToString(), str, (key, value) => { return value; });
            }
            watchDcWrite.Stop();
            Console.WriteLine("Dictionary Write Time:" + watchDcWrite.ElapsedMilliseconds);
        }

        public static void RedisAdd()
        {
            Stopwatch watchDcWrite = new Stopwatch();
            watchDcWrite.Start();
            for (int i = 0; i < 10000; i++)
            {
                string str = "Hello World!" + DateTime.Now.Ticks;
                db.StringSetAsync(i.ToString(), str);
            }
            watchDcWrite.Stop();
            Console.WriteLine("Redis Write Time:" + watchDcWrite.ElapsedMilliseconds);
        }
    }

    public class test
    {
        public int number;

        public string numberStr;
    }

    public sealed class RedisConnection
    {
        private static Lazy<RedisConnection> lazy = new Lazy<RedisConnection>(() =>
        {
            if (String.IsNullOrEmpty(_settingOption))
            {
                throw new InvalidOperationException("Please call Init() first.");
            }
            return new RedisConnection();
        });

        private static string _settingOption;

        public readonly ConnectionMultiplexer ConnectionMultiplexer;

        public static RedisConnection Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        private RedisConnection()
        {
            ConnectionMultiplexer = ConnectionMultiplexer.Connect(_settingOption);
        }

        public static void Init(string settingOption)
        {
            _settingOption = settingOption;
        }
    }
}

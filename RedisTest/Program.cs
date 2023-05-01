using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace RedisTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RedisConnection.Init("localhost:56379");
            var redis = RedisConnection.Instance.ConnectionMultiplexer;
            var db = redis.GetDatabase(0);
            Stopwatch watchWrite = new Stopwatch();
            watchWrite.Start();
            for (int i = 0; i < 8888888; i++)
            {
                db.StringSetAsync(i.ToString(), "Hello World!" + DateTime.Now.Ticks);
            }
            watchWrite.Stop();
            Console.WriteLine("Write Time:" + watchWrite.ElapsedMilliseconds);
            Random r = new Random();
            Stopwatch watchRead = new Stopwatch();
            watchRead.Start();
            for (int i = 0; i < 10; i++)
            {
                string index = r.Next(1, 8888888).ToString();
                Console.WriteLine(index + " - " + db.StringGet(index));
            }
            watchRead.Stop();
            Console.WriteLine("Read Time:" + watchRead.ElapsedMilliseconds);


            Stopwatch watchDcWrite = new Stopwatch();
            watchDcWrite.Start();
            ConcurrentDictionary<string, string> dcTest = new ConcurrentDictionary<string, string>();
            for (int i = 0; i < 8888888; i++)
            {
                string str = "Hello World!" + DateTime.Now.Ticks;
                dcTest.AddOrUpdate(i.ToString(), str, (key, value) => { return value; });
            }
            watchDcWrite.Stop();
            Console.WriteLine("Dictionary Write Time:" + watchDcWrite.ElapsedMilliseconds);

            Stopwatch watchDcRead = new Stopwatch();
            watchDcRead.Start();
            for (int i = 0; i < 10; i++)
            {
                string index = r.Next(1, 8888888).ToString();
                Console.WriteLine(index + " - " + dcTest[index]);
            }
            watchDcRead.Stop();
            Console.WriteLine("Dictionary Read Time:" + watchDcRead.ElapsedMilliseconds);






            db.StringSet(TCMSKey.alarm.ToString(), "sgfsdrgasfsdr");
            string alarm = db.StringGet(TCMSKey.alarm.ToString());
            Console.WriteLine(alarm);
        }
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

    enum TCMSKey : uint
    {
        drive = 0,
        door = 1,
        alarm = 2
    }
}

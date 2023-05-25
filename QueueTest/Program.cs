using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QueueTest
{
    class Program
    {
        private static Dictionary<String, Queue<SyncObjectTypeEnum>> tcpSyncObjectQueueDictionary =
            new Dictionary<String, Queue<SyncObjectTypeEnum>>();
        static void Main(string[] args)
        {
            for (int i = 0; i < 4; i++)
            {
                var name = "SyncThread" + i;
                tcpSyncObjectQueueDictionary.Add(name, new Queue<SyncObjectTypeEnum>());
            }

            string threadName = "SyncThread0";
            tcpSyncObjectQueueDictionary[threadName].Enqueue(SyncObjectTypeEnum.TodayGraph);
            Console.WriteLine(tcpSyncObjectQueueDictionary.Count);
            Console.WriteLine(tcpSyncObjectQueueDictionary[threadName].Count);


            while (true)
            {
                if (!tcpSyncObjectQueueDictionary.ContainsKey(threadName))
                {
                    //防止初始化未完成就读取队列造成的闪退
                    Thread.Sleep(10);
                    continue;
                }
                else
                {
                    Console.WriteLine(tcpSyncObjectQueueDictionary.Count);
                }

                tcpSyncObjectQueueDictionary[threadName].Dequeue();
                Console.WriteLine(tcpSyncObjectQueueDictionary.Count);

            }

            tcpSyncObjectQueueDictionary[threadName].Clear();
            Console.WriteLine(tcpSyncObjectQueueDictionary[threadName].Count);





        }
    }
    /// <summary>
    /// TCP同步对象的种类
    /// </summary>
    internal enum SyncObjectTypeEnum
    {
        /// <summary>
        /// 当日运行计划
        /// </summary>
        TodayGraph,

        /// <summary>
        /// 当日派班
        /// </summary>
        TodayDispatch,

        /// <summary>
        /// 当日洗车计划
        /// </summary>
        TodayWashTrain,

        /// <summary>
        /// 当日备车计划
        /// </summary>
        TodayReadyTrain,

        /// <summary>
        /// 当日自动调车
        /// </summary>
        TodayAutoDispatch,

        /// <summary>
        /// 以上所有数据种类
        /// </summary>
        AllData
    }
}

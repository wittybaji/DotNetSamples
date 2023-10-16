using System;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPoolTest
{
    class Program
    {
        static void Main()
        {

            Thread testThread = new Thread(() =>
            {
                Console.WriteLine("testThread...");
            })
            { IsBackground = true };
            testThread.Start();
            Thread.Sleep(1000);
            Console.WriteLine(testThread.IsAlive);

            int processorCount = Environment.ProcessorCount;
            Console.WriteLine($"ProcessorCount：{processorCount}");
            bool setMaxThreadsResult = ThreadPool.SetMaxThreads(processorCount, processorCount);
            Console.WriteLine($"SetMaxThreads:{setMaxThreadsResult}");

            ThreadPool.GetMaxThreads(out int workerThreads, out int completionPortThreads);
            Console.WriteLine($"GetMaxThreads < {workerThreads} > < {completionPortThreads} >");
            Thread addTask = new Thread(() =>
            {
                int taskId = 0;
                while (true)
                {
                    taskId++;
                    ThreadPool.GetAvailableThreads(out int availableWorkerThreads, out int availableCompletionPortThreads);
                    Console.WriteLine($"GetAvailableThreads < {availableWorkerThreads} > < {availableCompletionPortThreads} >");
                    bool queueResult;
                    queueResult = ThreadPool.QueueUserWorkItem(p =>
                    {
                        Work(taskId);
                    });
                    //queueResult = ThreadPool.QueueUserWorkItem(async p =>
                    //{
                    //    await WorkAsync(taskId);
                    //});
                    if (queueResult)
                    {
                        Console.WriteLine($"++ {taskId}");
                    }
                    else
                    {
                        Console.WriteLine($"!! {taskId}");
                    }
                    Thread.Sleep(500);
                }
            });
            addTask.Start();
            Console.ReadLine();
        }

        private static async Task WorkAsync(int taskId)
        {
            Console.WriteLine($"-> 启动任务 {taskId} {Thread.CurrentThread.ManagedThreadId}");
            await Task.Delay(10000);
            Console.WriteLine($"<- 结束任务 {taskId} {Thread.CurrentThread.ManagedThreadId}");
        }

        private static void Work(int taskId)
        {
            Console.WriteLine($"-> 启动任务 {taskId} {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(10000);
            Console.WriteLine($"<- 结束任务 {taskId} {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}

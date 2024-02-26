using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskTest
{
    internal class CancellationTask
    {
        /// <summary>
        /// 异步任务取消令牌源
        /// </summary>
        private readonly CancellationTokenSource _cancellationTokenSource;

        /// <summary>
        /// 异步任务取消令牌
        /// </summary>
        private CancellationToken _asyncTaskCancellationToken;

        public CancellationTask()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _asyncTaskCancellationToken = _cancellationTokenSource.Token;
            var asyncTask = new Task(Job, _asyncTaskCancellationToken);
            asyncTask.GetAwaiter().OnCompleted(() =>
            {
                Console.WriteLine("任务完成，释放所有相关资源。");
            });
            asyncTask.Start();
        }

        /// <summary>
        /// 取消异步任务
        /// </summary>
        public void Cancel()
        {
            Console.WriteLine("开始触发结束任务");
            _cancellationTokenSource.Cancel();
        }

        /// <summary>
        /// 任务内容
        /// </summary>
        private void Job()
        {
            if (_asyncTaskCancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("执行任务前已被取消，不进行执行");
                _asyncTaskCancellationToken.ThrowIfCancellationRequested();
            }
            while (true)
            {
                if (_asyncTaskCancellationToken.IsCancellationRequested)
                {
                    //按需释放任务内资源，然后触发任务取消
                    Console.WriteLine("触发释放任务内局部资源");
                    return;
                    //_asyncTaskCancellationToken.ThrowIfCancellationRequested();
                }
                Console.WriteLine($"{DateTime.Now} Working...");
                Thread.Sleep(1000);
            }
        }

        public void Log()
        {
            Console.WriteLine(_cancellationTokenSource.Token.IsCancellationRequested);
        }
    }
}

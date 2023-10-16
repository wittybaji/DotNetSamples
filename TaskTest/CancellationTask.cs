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

        /// <summary>
        /// 异步任务
        /// </summary>
        private readonly Task _asyncTask;

        public CancellationTask()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _asyncTaskCancellationToken = _cancellationTokenSource.Token;
            _asyncTask = new Task(Job, _asyncTaskCancellationToken);
            _asyncTask.GetAwaiter().OnCompleted(() =>
            {
                Console.WriteLine("任务完成，释放所有相关资源。");
            });
        }

        /// <summary>
        /// 开始执行任务
        /// </summary>
        public void Run()
        {
            if (!_asyncTask.IsCompleted)
            {
                _asyncTask.Start();
            }
            else
            {
                Console.WriteLine("任务在执行前已完成");
            }
        }

        /// <summary>
        /// 取消异步任务
        /// </summary>
        public async void Cancel()
        {
            Console.WriteLine("开始触发结束任务");
            _cancellationTokenSource.Cancel();

            try
            {
                await _asyncTask;
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine($"{nameof(OperationCanceledException)} thrown");
            }
            finally
            {
                _cancellationTokenSource.Dispose();
            }
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
                    _asyncTaskCancellationToken.ThrowIfCancellationRequested();
                }
                Console.WriteLine($"{DateTime.Now} Working...");
                Thread.Sleep(1000);
            }
        }

    }
}

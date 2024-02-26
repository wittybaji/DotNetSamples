using System;
using System.Text;
using System.Threading.Tasks;

namespace TaskTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Task线程内未捕获异常处理事件
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;


            //CancellationTask task = new CancellationTask();
            //Thread.Sleep(5000);
            //task.Cancel();
            //Thread.Sleep(2000);
            //task.Log();
            //Console.ReadLine();
        }

        /// <summary>
        /// Task线程内未捕获异常处理函数
        /// </summary>
        private static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            try
            {
                string msg = ExceptionToString(e.Exception, "Task线程");
                e.SetObserved();//设置该异常已察觉（这样处理后就不会引起程序崩溃）
                Console.WriteLine(msg);
            }
            catch (Exception ex)
            {
                string msg = ExceptionToString(ex, "Task线程 处理函数");
                Console.WriteLine(msg);
            }
        }

        /// <summary>
        /// 提取异常信息
        /// </summary>
        private static string ExceptionToString(Exception ex, string info)
        {
            StringBuilder str = new StringBuilder($"{DateTime.Now}, {info}发生了一个错误！{Environment.NewLine}");
            if (ex.InnerException == null)
            {
                str.Append($"【对象名称】：{ex.Source}{Environment.NewLine}");
                str.Append($"【异常类型】：{ex.GetType().Name}{Environment.NewLine}");
                str.Append($"【详细信息】：{ex.Message}{Environment.NewLine}");
                str.Append($"【堆栈调用】：{ex.StackTrace}");
            }
            else
            {
                str.Append($"【对象名称】：{ex.InnerException.Source}{Environment.NewLine}");
                str.Append($"【异常类型】：{ex.InnerException.GetType().Name}{Environment.NewLine}");
                str.Append($"【详细信息】：{ex.InnerException.Message}{Environment.NewLine}");
                str.Append($"【堆栈调用】：{ex.InnerException.StackTrace}");
            }
            return str.ToString();
        }

    }

}

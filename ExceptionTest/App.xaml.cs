﻿using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ExceptionTest
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //UI线程未捕获异常处理事件
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            //Task线程内未捕获异常处理事件
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            //非UI线程未捕获异常处理事件
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;


            Task.Run(() => { throw new Exception(); });

            GC.Collect();
        }


        /// <summary>
        /// UI线程未捕获异常处理函数
        /// </summary>
        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                e.Handled = true;
                string msg = ExceptionToString(e.Exception, "UI线程");
                Log(msg);
            }
            catch (Exception ex)
            {
                string msg = ExceptionToString(ex, "UI线程 处理函数");
                Log(msg);
            }
        }

        /// <summary>
        /// Task线程内未捕获异常处理函数
        /// </summary>
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            try
            {
                string msg = ExceptionToString(e.Exception, "Task线程");
                e.SetObserved();//设置该异常已察觉（这样处理后就不会引起程序崩溃）
                Log(msg);
            }
            catch (Exception ex)
            {
                string msg = ExceptionToString(ex, "Task线程 处理函数");
                Log(msg);
            }
        }

        /// <summary>
        /// 非UI线程未捕获异常处理函数
        /// </summary>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                string msg = e.ExceptionObject is Exception ex ? ExceptionToString(ex, "非UI线程") : $"发生了一个错误！信息:{e.ExceptionObject}";
                Log(msg);
            }
            catch (Exception ex)
            {
                string msg = ExceptionToString(ex, "非UI线程 处理函数");
                Log(msg);
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

        /// <summary>
        /// 打印日志
        /// </summary>
        /// <param name="msg"></param>
        private static void Log(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}

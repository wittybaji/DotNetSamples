using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ProcessTest
{
    class Program
    {
        static void Main(string[] args)
        {
            CloseWindow();
            //RestartExplorer();
            //string processName = "Feishu";
            //string applicationPath = @"C:\Users\Pluto\AppData\Local\Feishu\app\Feishu.exe";
            //Process targetProcess = null;
            //var allProcess = Process.GetProcessesByName(processName);
            //foreach (var process in allProcess)
            //{
            //    using (process)
            //    {
            //        try
            //        {
            //            if (process.MainModule != null)
            //            {
            //                if (!string.IsNullOrEmpty(process.MainModule.FileName))
            //                {
            //                    if (process.MainModule.FileName == applicationPath)
            //                    {
            //                        targetProcess = process;
            //                        break;
            //                    }
            //                }
            //            }

            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine($"IsProcessRunning方法出现异常:{ex.Message}{Environment.NewLine}{ex.StackTrace}");
            //        }
            //    }
            //}

            //Console.WriteLine(targetProcess == null);
            Console.ReadLine();
        }

        private static void CloseWindow()
        {
            int hwCurr = Win32ApiClass.FindWindow(null, "此电脑");
            Console.WriteLine(hwCurr);
            var result = Win32ApiClass.ShowWindow((IntPtr)hwCurr, 0);
            Console.WriteLine(result);
        }


        /// <summary>
        /// 运行资源管理器
        /// 已启动则不再启动
        /// </summary>
        private static void RestartExplorer()
        {
            Task.Run(async () =>
            {
                string explorerProcessName = "explorer";
                ShutdownByProcessName(explorerProcessName);
                Console.WriteLine("开始休眠");
                await Task.Delay(3000);
                Console.WriteLine("准备启动");
                //var allProcess = Process.GetProcessesByName(explorerProcessName);
                //if (allProcess.Length > 0)
                //{
                //    //存在资源管理器，不启动
                //    Console.WriteLine("存在资源管理器，不启动");
                //    foreach (var process in allProcess)
                //    {
                //        process.Dispose();
                //    }
                //}
                //else
                {
                    Console.WriteLine("执行启动");
                    var explorerPath = Path.Combine(Environment.GetEnvironmentVariable("windir"), "explorer.exe");
                    StartProcess(explorerPath);
                }
            });
        }

        /// <summary>
        /// 关闭指定名称的进程
        /// </summary>
        public static void ShutdownByProcessName(string name)
        {
            try
            {
                var allProcess = Process.GetProcessesByName(name);
                foreach (var process in allProcess)
                {
                    using (process)
                    {
                        TryKillProcess(process);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ShutdownByProcessNameException，ProcessName:{name}");
            }
        }

        /// <summary>
        /// 结束进程并记录
        /// </summary>
        /// <param name="process">进程对象</param>
        private static void TryKillProcess(Process process)
        {
            if (process != null)
            {
                try
                {
                    process.Kill();
                    if (process.WaitForExit(3000))
                    {
                        Console.WriteLine($"停止进程 {process.ProcessName} 成功！");
                    }
                    else
                    {
                        Console.WriteLine($"停止进程 {process.ProcessName} 超时！");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"停止进程 {process.ProcessName} 时出现异常");
                }
            }
        }

        /// <summary>
        /// 启动程序
        /// </summary>
        public static void StartProcess(string exePath)
        {
            try
            {
                if (!string.IsNullOrEmpty(exePath) && File.Exists(exePath))
                {
                    // 定义程序启动位置，防止使用相对路径时出现错误
                    if (GetRunStateCode(Path.GetFileNameWithoutExtension(exePath), exePath) == 1)
                    {
                        //程序已经在运行
                        return;
                    }
                    Win32ApiClass.STARTUPINFO sInfo = new Win32ApiClass.STARTUPINFO();
                    Win32ApiClass.PROCESS_INFORMATION pInfo = new Win32ApiClass.PROCESS_INFORMATION();
                    // 定义程序启动位置，防止使用相对路径时出现错误
                    bool ret = Win32ApiClass.CreateProcess(new StringBuilder(exePath), null, null, null, false, 0, null,
                        new StringBuilder(Directory.GetParent(exePath).ToString()), ref sInfo, ref pInfo);
                    if (!ret)
                    {
                        // 0为失败
                        Console.WriteLine($"启动进程: {exePath} 失败！");
                    }
                }
                else
                {
                    Console.WriteLine($"启动程序检查失败，程序不存在，路径：{exePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"启动进程:{exePath} 时出现异常");
            }
        }

        /// <summary>
        /// 获取程序运行状态
        /// 1:正在运行
        /// 2:没有运行
        /// </summary>
        /// <param name="processName">进程名称</param>
        /// <param name="exeFile">可执行文件路径</param>
        /// <returns>状态码</returns>
        public static byte GetRunStateCode(string processName, string exeFile)
        {
            byte result = 2;
            try
            {
                var matchProcess = Process.GetProcessesByName(processName);
                foreach (var process in matchProcess)
                {
                    try
                    {
                        if (process.MainModule.FileName == exeFile)
                        {
                            result = 1;
                            break;
                        }
                    }
                    catch { }
                }
                foreach (var process in matchProcess)
                {
                    process.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("获取程序运行状态异常");
            }
            return result;
        }





    }
}

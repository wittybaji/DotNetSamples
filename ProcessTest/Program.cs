using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string processName = "Feishu";
            string applicationPath = @"C:\Users\Pluto\AppData\Local\Feishu\app\Feishu.exe";
            Process targetProcess = null;
            var allProcess = Process.GetProcessesByName(processName);
            foreach (var process in allProcess)
            {
                using (process)
                {
                    try
                    {
                        if (process.MainModule != null)
                        {
                            if (!string.IsNullOrEmpty(process.MainModule.FileName))
                            {
                                if (process.MainModule.FileName == applicationPath)
                                {
                                    targetProcess = process;
                                    break;
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"IsProcessRunning方法出现异常:{ex.Message}{Environment.NewLine}{ex.StackTrace}");
                    }
                }
            }

            Console.WriteLine(targetProcess == null);
        }
    }
}

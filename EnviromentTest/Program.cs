using Microsoft.Win32;
using System;

namespace EnviromentTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //var tick = System.Environment.TickCount;

            try
            {
                var localMachine = Microsoft.Win32.Registry.LocalMachine;
                var software = localMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true);
                software.SetValue("AutoRestartShell", 0, RegistryValueKind.DWord);
                software.Close();
                localMachine.Close();


                string winlogon = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon";
                string keyName = "AutoRestartShell";
                RegistryKey key = Registry.LocalMachine.OpenSubKey(winlogon, true);
                string value = key.GetValue(keyName).ToString();
                Console.WriteLine(value);
                key.SetValue(keyName, 0, RegistryValueKind.DWord);
                value = key.GetValue(keyName).ToString();
                Console.WriteLine(value);
                key.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AutoRestartShell Set Exception:{ex.Message}");
            }
            Console.ReadLine();
        }
    }
}

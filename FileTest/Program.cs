using System;
using System.IO;
using System.Security.Cryptography;

namespace FileTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var md5 = MD5.Create())
            {
                string path = @"D:\Desktop\344.jpg";
                FileInfo info = new FileInfo(path);
                FileStream stream = info.Create();
                stream.WriteByte(0x11);
                stream.Flush();
                try
                {
                    stream.Position = 0;
                    byte[] hashValue = md5.ComputeHash(stream);
                    Console.Write($"{stream.Name}: {BitConverter.ToString(hashValue)}");
                }
                catch (IOException e)
                {
                    Console.WriteLine($"I/O Exception: {e.Message}");
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine($"Access Exception: {e.Message}");
                }
                stream.Close();
            }


            Console.ReadLine();


            string dllPath = @"D:\TCT\CurrentVersion\CenterApplicationServer\Debug\CenterAppMain.dll";
            var dllFileVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(dllPath).FileVersion;
            var dllProductVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(dllPath).ProductVersion;
            Console.WriteLine(dllFileVersion);
            Console.WriteLine(dllProductVersion);


            Console.ReadLine();
        }
    }
}

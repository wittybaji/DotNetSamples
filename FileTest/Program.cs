using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace FileTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ExtensionTest();
            string current = @"D:\TCT\CurrentVersion";
            var childs = GetChildren(current);
            Console.WriteLine(childs.Count());

            string ext = @"344.jpg";
            string name = Path.GetFileNameWithoutExtension(ext);
            Console.WriteLine(name);
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

        private static IEnumerable<string> GetChildren(string path)
        {
            return Directory.Exists(path) ? Directory.GetFileSystemEntries(path) : (IEnumerable<string>)new List<string>();
        }




        private static void ExtensionTest()
        {
            string currentVersionDirectory = @"D:\TCT\CurrentVersion";
            string lastVersionName = "";

            // 文件夹不为空则备份
            if (currentVersionDirectory != null && Directory.GetFileSystemEntries(currentVersionDirectory).Length != 0)
            {
                var fileInCurrentVersion = Directory.GetFiles(currentVersionDirectory);
                foreach (var file in fileInCurrentVersion)
                {
                    string ext = Path.GetExtension(file);
                    if (ext == "version")
                    {
                        lastVersionName = Path.GetFileNameWithoutExtension(file);
                        break;
                    }
                }
                if (string.IsNullOrEmpty(lastVersionName))
                {
                    //没有找到version文件
                    lastVersionName = $"CurrentVersionBackUp_{DateTime.Now:yyyyMMddHHmmss}";
                    Console.WriteLine(lastVersionName);
                }
                else
                {
                    Console.WriteLine(lastVersionName);
                }
            }
        }




    }
}

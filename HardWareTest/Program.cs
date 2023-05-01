using System;
using Microsoft.DotNet.PlatformAbstractions;

namespace HardWareTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(Environment.ProcessorCount);

            string iniEnvironmentValue = Environment.GetEnvironmentVariable("ini", EnvironmentVariableTarget.User);
            foreach (var path in iniEnvironmentValue.Split(';'))
            {
                Console.WriteLine($"Path: {path}");
            }
            Console.WriteLine(System.IO.Directory.GetParent(ApplicationEnvironment.ApplicationBasePath));

            Console.WriteLine(RuntimeEnvironment.OperatingSystem);


        }
    }
}

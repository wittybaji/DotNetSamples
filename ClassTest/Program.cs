using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClassTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello");
            var user = new User();
            var userSize = Marshal.SizeOf(user);
            Console.WriteLine(userSize);

            //DisplaySizeOf<User>();
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public class User
    {
        public int Age { get; set; }
        public string Name { get; set; }

        public string _Name = "1234" + "abcd";
        public List<string> _Names;
    }

}

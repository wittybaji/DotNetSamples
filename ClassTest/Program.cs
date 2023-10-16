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
            //var user = new User();
            //var userSize = Marshal.SizeOf(user);
            //Console.WriteLine(userSize);



            List<(ushort TrainID, byte ProtectZoneID, byte Direction)> temp = new List<(ushort TrainID, byte ProtectZoneID, byte Direction)>();
            temp.Add((1, 22, 33));

            foreach (var t in temp)
            {
                var type = t.GetType();
                Console.WriteLine(type);
            }

            List<(ushort TrainID, User Driver)> temp1 = new List<(ushort TrainID, User Driver)>();
            temp1.Add((1, new User() { Age = 22, Name = "Bob" }));

            foreach (var t in temp1)
            {
                var type = t.GetType();
                Console.WriteLine(type);
            }


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

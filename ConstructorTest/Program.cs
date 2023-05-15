using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructorTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var p1 = new People()
            {
                Name = "Alice",
                Age = 22
            };
            Console.WriteLine(p1);

            var p2 = new People();
            p2.Name = "Bob";
            p2.Age = 23;
            Console.WriteLine(p2);


            List<People> pList = new List<People>() { p1, p2 };

            Console.WriteLine(pList.Count);

        }
    }

    class People
    {
        public int Age { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return $"Name:{Name} Age:{Age}";
        }
    }

}

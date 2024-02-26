using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {

                List<int> test = null;
                int result = test.FirstOrDefault(x => x == 1);
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

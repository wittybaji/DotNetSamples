using System.Runtime.CompilerServices;

namespace ClassTestV6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var user1 = new User();
            var user2 = new User();
            var user3 = new User();
            Console.WriteLine(user1.Age);
        }
    }
    public class User
    {
        public int Age { get; set; }
        public string Name { get; set; }

        public string _Name = "1234" + "abcd";
        public List<string> _Names;
    }
}
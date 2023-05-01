using System;
using System.Linq;
using System.Reflection;

namespace AssignableTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            var result1 = types.Where(x => x.IsInterface).ToList();
            var result2 = types.Where(x => x.IsInterface && x.IsAssignableFrom(typeof(IPersion<,>))).ToList();
            var result3 = types.Where(x => x.IsInterface && typeof(IPersion<,>).IsAssignableFrom(x)).ToList();
            var result4 = types.Where(x => x.IsInterface && x.IsAssignableFrom(typeof(IStudent<,>))).ToList();
            Console.WriteLine();
        }
    }
    public interface IPersion<T, K> { }
    public interface IStudent<T, K> : IPersion<T, K> { }
}

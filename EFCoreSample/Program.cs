namespace EFCoreSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PizzaService service = new PizzaService();
            service.AddFakeData();
            var allPizza = service.GetAll();
            foreach (var pizza in allPizza)
            {
                Console.WriteLine($"Id:{pizza.Id} Name:{pizza.Name}");
            }
        }
    }
}
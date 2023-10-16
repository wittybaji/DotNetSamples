using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EFCoreSample.Data
{
    internal class DbContextFactory : IDesignTimeDbContextFactory<PizzaContext>
    {
        public PizzaContext CreateDbContext(string[] args)
        {
            if (args.Length > 0)
            {
                string dbFile = args[0];
                if (File.Exists(dbFile))
                {
                    var options = new DbContextOptionsBuilder<PizzaContext>().UseSqlite($"Data Source={dbFile}").Options;
                    return new PizzaContext(options);
                }
                else
                {
                    throw new FileNotFoundException();
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
    }
}

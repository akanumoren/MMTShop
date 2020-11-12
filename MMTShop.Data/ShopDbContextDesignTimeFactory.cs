using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MMTShop.Data
{
    public class ShopDbContextDesignTimeFactory : IDesignTimeDbContextFactory<ShopDbContext>    {        public ShopDbContext CreateDbContext(string[] args)        {            var optionsBuilder = new DbContextOptionsBuilder<ShopDbContext>();            optionsBuilder.UseSqlServer("Data Source=(local);Initial Catalog=MMTShop;Integrated Security=True");            return new ShopDbContext(optionsBuilder.Options);        }    }
}

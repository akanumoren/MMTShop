using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MMTShop.Data;

namespace MMTShop.ServiceExtensions
{
    public static class MMTShopDatabaseServiceExtensions
    {
        public static void AddMMTShopDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var defaultConnection = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ShopDbContext>(builder => builder.UseSqlServer(defaultConnection));

            services.AddScoped<IShopDbContext, ShopDbContext>();
        }
    }
}

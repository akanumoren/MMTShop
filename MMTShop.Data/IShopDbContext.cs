using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MMTShop.Data.Entities;

namespace MMTShop.Data
{
    public interface IShopDbContext
    {
        DbSet<Category> Categories { get; set; }

        DbSet<Product> Products { get; set; }

        Task<IEnumerable<T>> ExecuteSql<T>(DbSet<T> set, string sql, params object[] parameters) where T : class, new();
    }
}

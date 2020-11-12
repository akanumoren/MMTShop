using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MMTShop.Data.Entities;

namespace MMTShop.Data
{
    /// <summary>
    /// Shop context allow access to shop database tables
    /// </summary>
    public class ShopDbContext : DbContext, IShopDbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public ShopDbContext(DbContextOptions options) : base(options)        {        }        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)        {            base.OnConfiguring(optionsBuilder);        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


        public async Task<IEnumerable<T>> ExecuteSql<T>(DbSet<T> set, string sql, params object[] parameters) where  T : class, new()
        {
            // TODO handle errors
            var result = await set.FromSqlRaw(sql, parameters).ToListAsync();

            return result;
        }
    }
}

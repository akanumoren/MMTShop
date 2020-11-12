using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MMTShop.Business.Interfaces;
using MMTShop.Data;
using MMTShop.Data.Entities;

namespace MMTShop.Business.Services
{
    /// <summary>
    /// Product Service, contains methods for fetching producte from the database
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IShopDbContext shopDbContext;

        public ProductService(IShopDbContext shopDbContext)
        {
            this.shopDbContext = shopDbContext;
        }


        /// <summary>
        /// Calls get category products stored procedure passing category id, returns list of products.
        /// </summary>
        /// <param name="categoryId">An integer representing category primary key.</param>
        public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId)
        {
            try
            {
                var categoryIdParam = new SqlParameter("@id", categoryId);

                var products = await shopDbContext.ExecuteSql<Product>(shopDbContext.Products, $"{StoredProcedures.GETCATEGORYPRODUCTS} @id", categoryIdParam);

                return products;
            }
            catch(Exception exc)
            {
                // Log error

                throw new Exception("Request cannot be completed now, please contact Data provider");
             
            }
        }


        /// <summary>
        /// Calls get featured products stored procedure returns list of products.
        /// </summary>
        public async Task<IEnumerable<Product>> GetFeaturedProductsAsync()
        {
            try
            {
                var featuredProducts = await shopDbContext.Products.FromSqlRaw(StoredProcedures.GETFEATUREDPRODUCTS).ToListAsync();
                return featuredProducts;
            }
            catch (Exception exc)
            {
                // Log error

                throw new Exception("Request cannot be completed now, please contact Data provider");


            }
        }
          

    }

}

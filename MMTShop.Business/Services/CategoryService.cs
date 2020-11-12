using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MMTShop.Business.Interfaces;
using MMTShop.Data;
using MMTShop.Data.Entities;

namespace MMTShop.Business.Services
{
    /// <summary>
    /// Category Service, contains methods for fetching categories from the database
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly IShopDbContext shopDbContext;

        public CategoryService(IShopDbContext shopDbContext)
        {
            this.shopDbContext = shopDbContext;
        }

        /// <summary>
        /// Calls get categories stored procedure returns list of categories.
        /// </summary>
        public async Task<IEnumerable<Category>> GetAsync()
        {
            try
            {
                var categories = await shopDbContext.ExecuteSql<Category>(shopDbContext.Categories, $"{StoredProcedures.GETCATEGORIES}");

                return categories;
            }
            catch (Exception exc)
            {
                // Log error

                throw new Exception("Request cannot be completed now, please contact Data provider");


            }
        }
    }

}

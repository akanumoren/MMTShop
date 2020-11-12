using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MMTShop.Data.Entities;

namespace MMTShop.Business.Interfaces
{
    /// <summary>
    /// Interface for product service
    /// </summary>
    public interface IProductService    
    {
        Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId);

        Task<IEnumerable<Product>> GetFeaturedProductsAsync();
    }
}

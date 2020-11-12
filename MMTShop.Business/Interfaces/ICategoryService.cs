using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MMTShop.Data.Entities;

namespace MMTShop.Business.Interfaces
{
    /// <summary>
    /// Interface for category service
    /// </summary>
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAsync();
    }
}

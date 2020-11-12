using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MMTShop.Business.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MMTShop.Controllers
{
    /// <summary>
    /// API to access categories 
    /// </summary>
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;

        public CategoriesController(ICategoryService categoryService, IProductService productService)
        {
            this.categoryService = categoryService;
            this.productService = productService;
        }

        // GET: api/categories
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {            
            var categories = await categoryService.GetAsync();

            if(categories == null)
            {
                return NoContent();
            }

            return Ok(categories);
        }

        // GET: api/categories/id/products
        [HttpGet("{id}/products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProducts([FromRoute] int id)
        {
            var products = await productService.GetByCategoryAsync(id);

            if (products == null)
            {
                return NoContent();
            }

            return Ok(products);
        }
    }
}

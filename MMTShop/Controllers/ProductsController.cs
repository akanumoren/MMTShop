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
    /// API to get featured products
    /// </summary>
    [Route("api/featured-products")]
    public class FeaturedProductsController : Controller
    {
        private readonly IProductService productService;

        public FeaturedProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        // GET: api/featured-products
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var products = await productService.GetFeaturedProductsAsync();

            if (products == null)
            {
                return NoContent();
            }

            return Ok(products);
        }
    }
}

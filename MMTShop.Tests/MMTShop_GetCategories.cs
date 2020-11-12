using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MMTShop.Business.Interfaces;
using MMTShop.Data;
using MMTShop.Data.Entities;
using Moq;
using Xunit;

namespace MMTShop.Tests
{
    public class MMTShop_GetCategories
    {
        [Fact]
        public async Task Get_Categories_Returns_Categories()
        {
            // setup
            var context = new Mock<IShopDbContext>();

            var dbSetMock = new Mock<DbSet<Category>>();

            context.Setup(s => s.Categories).Returns(dbSetMock.Object);

            context.Setup(s => s.ExecuteSql(It.IsAny<DbSet<Category>>(), It.IsAny<string>())).Returns(Task.FromResult(Categories()));

            var categoryService = new Mock<ICategoryService>();

            categoryService.Setup(s => s.GetAsync()).Returns(Task.FromResult( Categories()));

            // Execute
            var categories = await categoryService.Object.GetAsync();

            // Assert
            Assert.True(categories.Count() > 0);
        }

        [Fact]
        public async Task Get_Category_Products_Returns_Products()
        {
            // setup
            var context = new Mock<IShopDbContext>();

            var dbSetMock = new Mock<DbSet<Product>>();

            context.Setup(s => s.Products).Returns(dbSetMock.Object);

            context.Setup(s => s.ExecuteSql(It.IsAny<DbSet<Product>>(), It.IsAny<string>())).Returns(Task.FromResult(Products()));

            var productService = new Mock<IProductService>();

            productService.Setup(s => s.GetByCategoryAsync(It.IsAny<int>())).Returns(Task.FromResult(Products()));

            // Execute
            var products = await productService.Object.GetByCategoryAsync(1);

            // Assert
            Assert.True(products.Count() > 0);
        }

        IEnumerable<Category> Categories()
        {
            return new List<Category> {
                new Category
                {
                    Id = 1,
                    Name = "Home"
                }
            };
        }

        IEnumerable<Product> Products()
        {
            return new List<Product> {
                new Product
                {
                    Id = 1,
                    Name = "Sofa Bed",
                    Description = "Sofa Bed",
                    Price = 500.00M,
                    SKU = "10001"
                },
                new Product
                {
                    Id = 2,
                    Name = "Cabinet",
                    Description = "Cabinet",
                    Price = 500.00M,
                    SKU = "10002"
                }
            };
        }

        IQueryable<Category> GetCategories()
        {
            return Categories().AsQueryable();
        }

        IQueryable<Product> GetProducts()
        {
            return Products().AsQueryable();
        }
    }
}

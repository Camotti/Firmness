using firmness.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firmness.Tests.Services
{
    internal class ProductTest
    {
        public class ProductTests
        {
            [Fact]
            public void Product_ShouldHavePositivePrice()
            {
                var product = new Product { Name = "Bricks", Price = 1000m };

                Assert.True(product.Price > 0, "Product price must be positive");
            }

            [Fact]
            public void Product_Stock_ShouldNotBeNegative()
            {
                var product = new Product { Stock = -5 };

                Assert.True(product.Stock >= 0, "Product stock cannot be negative");
            }
        }
    }
}

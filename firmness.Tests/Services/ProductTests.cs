using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using firmness.Domain.Entities;

namespace firmness.Tests.Services
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
                

                Assert.Throws<ArgumentException>(() => new Product { Stock = -5 });
            }
        }
    
}

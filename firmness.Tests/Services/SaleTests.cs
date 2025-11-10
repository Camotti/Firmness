using Xunit;
using System.Collections.Generic;
using firmness.Domain.Entities;

namespace firmness.Tests.Services
{
    public class SaleTests
    {
        [Fact]
        public void Sale_ShouldCalculateSubtotalCorrectly()
        {
            var sale = new Sale
            {
                SaleDetails = new List<SaleDetail>
                {
                    new SaleDetail { Quantity = 2, UnitPrice = 10m },
                    new SaleDetail { Quantity = 1, UnitPrice = 5m }
                }
            };

            var subtotal = sale.SaleDetails.Sum(d => d.Quantity * d.UnitPrice);

            Assert.Equal(25m, subtotal);
        }

        [Fact]
        public void SaleDetail_Subtotal_ShouldBeComputed()
        {
            var detail = new SaleDetail { Quantity = 3, UnitPrice = 10m };
            Assert.Equal(30m, detail.Subtotal);
        }
    }
}
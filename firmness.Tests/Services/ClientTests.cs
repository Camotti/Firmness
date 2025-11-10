using firmness.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using firmness.Domain.Entities;

namespace firmness.Tests.Services
{
    internal class ClientTests
    {
        public class ClientTest
        {
            [Fact]
            public void CreateClient_ShouldStoreBasicInfo()
            {
                // Arrange
                var client = new Client
                {
                    Name = "John Doe",
                    Email = "john@example.com",
                    Document = "123456789",
                    Phone = "987654321"
                };

                // Assert
                Assert.Equal("John Doe", client.Name);
                Assert.Contains("@", client.Email);
                Assert.Equal("123456789", client.Document);
            }

            [Fact]
            public void Client_Email_ShouldBeValidFormat()
            {
                var client = new Client { Email = "invalid_email" };

                bool isValid = client.Email.Contains("@");

                Assert.False(isValid, "Email without '@' should be invalid");
            }
        }
    }
}

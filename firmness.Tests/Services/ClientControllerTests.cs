using firmness.Application.DTOs;
using firmness.Application.Interfaces;
using firmness.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace firmness.Tests.Services;

public class ClientControllerTests
{
    [Fact]
    public async Task GetAll_ShouldReturnOKWithClients()
    {
        // Arrange
        var mockService = new Mock<IClientService>();

        mockService
            .Setup(s => s.GetAllAsync())
            .ReturnsAsync(new List<ClientDto>
            {
                new ClientDto { Id = 1, Name = "Kratos" }
            });

        var controller = new ClientController(mockService.Object);

        // Act
        var result = await controller.GetAll();

        // Assert
        var ok = Assert.IsType<OkObjectResult>(result);
        var clients = Assert.IsAssignableFrom<List<ClientDto>>(ok.Value);
        Assert.Single(clients);
    }
}
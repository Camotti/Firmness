using firmness.Application.DTOs;
using firmness.Application.Interfaces;
using firmness.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace firmness.Tests.Controllers;

public class ProductsControllerTests
{
    [Fact]
    public async Task GetAll_ShouldReturnOkWithProducts()
    {
        var mockService = new Mock<IProductService>();

        mockService.Setup(s => s.GetAllAsync())
            .ReturnsAsync(new List<ProductDto>
            {
                new ProductDto { Id = 1, Name = "Axe" }
            });

        var controller = new ProductsController(mockService.Object);

        var result = await controller.GetAllAsync();

        var ok = Assert.IsType<OkObjectResult>(result);
        var products = Assert.IsAssignableFrom<List<ProductDto>>(ok.Value);

        Assert.Single(products);
    }

    [Fact]
    public async Task Create_ShouldReturnOk_WhenSuccess()
    {
        var dto = new CreateProductDto { Name = "Blade" };

        var mockService = new Mock<IProductService>();
        mockService.Setup(s => s.CreateAsync(dto))
            .ReturnsAsync((true, "Created"));

        var controller = new ProductsController(mockService.Object);

        var result = await controller.Create(dto);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Create_ShouldReturnBadRequest_WhenFailed()
    {
        var dto = new CreateProductDto { Name = "Shield" };

        var mockService = new Mock<IProductService>();
        mockService.Setup(s => s.CreateAsync(dto))
            .ReturnsAsync((false, "Error"));

        var controller = new ProductsController(mockService.Object);

        var result = await controller.Create(dto);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Update_ShouldReturnBadRequest_WhenIdMismatch()
    {
        var controller = new ProductsController(new Mock<IProductService>().Object);

        var result = await controller.Update(9, new UpdateProductDto { Id = 10 });

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Delete_ShouldReturnNotFound_WhenFail()
    {
        var mockService = new Mock<IProductService>();
        mockService.Setup(s => s.DeleteAsync(1))
            .ReturnsAsync((false, "Error"));

        var controller = new ProductsController(mockService.Object);

        var result = await controller.Delete(1);

        Assert.IsType<NotFoundObjectResult>(result);
    }
}

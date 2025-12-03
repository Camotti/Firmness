using firmness.Application.DTOs;
using firmness.Application.Interfaces;
using firmness.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace firmness.Tests.Services;

public class SalesControllerTests
{
    [Fact]
    public async Task GetAll_ShouldReturnOkWithSales()
    {
        var mockService = new Mock<ISalesService>();
        mockService.Setup(s => s.GetAllSalesAsync())
            .ReturnsAsync(new List<SaleDto>
            {
                new SaleDto
                {
                    SaleId = 1,
                    SaleDate = DateTime.Now,
                    ClientId = "",
                    EmployeeId = "",
                    Details = new List<SaleDetailDto>()
                }
            });

        var controller = new SalesController(mockService.Object);

        var result = await controller.GetAll();

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.IsAssignableFrom<List<SaleDto>>(ok.Value);
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenNull()
    {
        var mockService = new Mock<ISalesService>();
        mockService.Setup(s => s.GetSaleByIdAsync(1))
            .ReturnsAsync((SaleDto?)null);

        var controller = new SalesController(mockService.Object);

        var result = await controller.GetById(1);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task Purchase_ShouldReturnOk_WhenSuccess()
    {
        var dto = new CreateSaleDto();

        var mockService = new Mock<ISalesService>();
        mockService.Setup(s => s.CreateSaleAsync(dto))
            .ReturnsAsync(true);

        var controller = new SalesController(mockService.Object);

        var result = await controller.Purchase(dto);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Purchase_ShouldReturnBadRequest_WhenFailure()
    {
        var dto = new CreateSaleDto();

        var mockService = new Mock<ISalesService>();
        mockService.Setup(s => s.CreateSaleAsync(dto))
            .ReturnsAsync(false);

        var controller = new SalesController(mockService.Object);

        var result = await controller.Purchase(dto);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Update_ShouldReturnBadRequest_WhenIdMismatch()
    {
        var controller = new SalesController(new Mock<ISalesService>().Object);

        var result = await controller.Update(9, new UpdateSaleDto { Id = 10 });

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Delete_ShouldReturnNotFound_WhenFail()
    {
        var mockService = new Mock<ISalesService>();
        mockService.Setup(s => s.DeleteSaleAsync(1))
            .ReturnsAsync(false);

        var controller = new SalesController(mockService.Object);

        var result = await controller.Delete(1);

        Assert.IsType<NotFoundObjectResult>(result);
    }
}
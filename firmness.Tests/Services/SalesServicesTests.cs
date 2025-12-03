using firmness.Application.DTOs;
using firmness.Application.Services;
using firmness.Domain.Entities;
using firmness.Application.Interfaces.Repositories;
using AutoMapper;
using firmness.Application.Interfaces;
using Moq;

namespace firmness.Tests.Services;

public class SalesServiceTests
{
    private readonly Mock<ISalesRepository> _repo = new();
    private readonly Mock<IMapper> _mapper = new();
    private readonly Mock<IEmailService> _email = new();

    [Fact]
    public async Task CreateSaleAsync_ShouldFail_WhenInvalid()
    {
        var service = new SalesService(_repo.Object, _mapper.Object, _email.Object);

        var dto = new CreateSaleDto { ClientId = "", EmployeeId = "", Details = new List<CreateSaleDetailDto>() };

        var result = await service.CreateSaleAsync(dto);

        Assert.False(result);
    }

    [Fact]
    public async Task DeleteSaleAsync_ShouldFail_WhenNotExists()
    {
        _repo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Sale)null);

        var service = new SalesService(_repo.Object, _mapper.Object, _email.Object);

        var result = await service.DeleteSaleAsync(1);

        Assert.False(result);
    }
}
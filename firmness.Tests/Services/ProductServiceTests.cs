using firmness.Application.DTOs;
using firmness.Infrastructure.Repositories; // ← CORRECTO
using firmness.Application.Services;
using firmness.Domain.Entities;
using AutoMapper;
using Moq;

namespace firmness.Tests.Services;

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _repo = new();
    private readonly Mock<IMapper> _mapper = new();

    [Fact]
    public async Task CreateAsync_ShouldReturnFalse_WhenNameEmpty()
    {
        var service = new ProductService(_repo.Object, _mapper.Object);

        var result = await service.CreateAsync(new CreateProductDto { Name = "" });

        Assert.False(result.Success);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnTrue_WhenValid()
    {
        var dto = new CreateProductDto { Name = "Axe", Price = 10, Stock = 5 };
        var product = new Product();

        _mapper.Setup(m => m.Map<Product>(dto)).Returns(product);

        var service = new ProductService(_repo.Object, _mapper.Object);

        var result = await service.CreateAsync(dto);

        Assert.True(result.Success);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnFalse_WhenNotExists()
    {
        _repo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Product)null);

        var service = new ProductService(_repo.Object, _mapper.Object);

        var result = await service.DeleteAsync(1);

        Assert.False(result.Success);
    }
}
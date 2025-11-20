using AutoMapper;
using firmness.Application.DTOs;
using firmness.Application.Services;
using firmness.Infrastructure.Repositories;
using Moq;

namespace firmness.Tests.Services;

public class ClientServiceTests
{
    private readonly Mock<IClientRepository> _mockRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly ClientService _service;

    public ClientServiceTests()
    {
        _mockRepository = new Mock<IClientRepository>();
        _mockMapper = new Mock<IMapper>();

        _service = new ClientService(_mockRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnError_WhenNameIsEmpty()
    {
        var dto = new CreateClientDto
        {
            Name = "",
            Document = "123456789",
            Email = "test@gmail.com"
        };

        var result = await _service.CreateAsync(dto);
        
        Assert.False(result.Success);
        Assert.Equal("The name and the Document fields must not be empty.", result.Message);



    }
}
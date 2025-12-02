using AutoMapper;
using firmness.Application.DTOs;
using firmness.Application.Services;
using firmness.Domain.Entities;
using firmness.Tests.TestHelpers;
using Microsoft.AspNetCore.Identity;
using MockQueryable;
using MockQueryable.Moq; // ← Asegúrate de tener esto
using Moq;
using Xunit;

namespace firmness.Tests.Services;

public class ClientServiceTests
{
    private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
    private readonly Mock<IMapper> _mockMapper;
    private readonly ClientService _service;

    public ClientServiceTests()
    {
        _mockUserManager = UserManagerMockHelper.CreateMockUserManager<ApplicationUser>();
        _mockMapper = new Mock<IMapper>();
        _service = new ClientService(_mockUserManager.Object, _mockMapper.Object);
    }

    #region CreateAsync Tests

    [Fact]
    public async Task CreateAsync_ShouldReturnError_WhenNameIsEmpty()
    {
        // Arrange
        var dto = new CreateClientDto
        {
            Name = "",
            Document = "123456789",
            Email = "test@gmail.com",
            Password = "Test123!"
        };

        // Act
        var result = await _service.CreateAsync(dto);
        
        // Assert
        Assert.False(result.Success);
        Assert.Equal("The name and the Document fields must not be empty.", result.Message);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnError_WhenNameIsWhitespace()
    {
        // Arrange
        var dto = new CreateClientDto
        {
            Name = "   ",
            Document = "123456789",
            Email = "test@gmail.com",
            Password = "Test123!"
        };

        // Act
        var result = await _service.CreateAsync(dto);
        
        // Assert
        Assert.False(result.Success);
        Assert.Equal("The name and the Document fields must not be empty.", result.Message);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnError_WhenDocumentIsEmpty()
    {
        // Arrange
        var dto = new CreateClientDto
        {
            Name = "Pietro",
            Document = "",
            Email = "test@gmail.com",
            Password = "Test123!"
        };

        // Act
        var result = await _service.CreateAsync(dto);
        
        // Assert
        Assert.False(result.Success);
        Assert.Equal("The name and the Document fields must not be empty.", result.Message);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnError_WhenDocumentIsWhitespace()
    {
        // Arrange
        var dto = new CreateClientDto
        {
            Name = "Pietro",
            Document = "   ",
            Email = "test@gmail.com",
            Password = "Test123!"
        };

        // Act
        var result = await _service.CreateAsync(dto);
        
        // Assert
        Assert.False(result.Success);
        Assert.Equal("The name and the Document fields must not be empty.", result.Message);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnError_WhenEmailIsInvalid()
    {
        // Arrange
        var dto = new CreateClientDto
        {
            Name = "Pietro",
            Document = "123456789",
            Email = "invalidemail",
            Password = "Test123!"
        };

        // Act
        var result = await _service.CreateAsync(dto);
        
        // Assert
        Assert.False(result.Success);
        Assert.Equal("El correo electrónico no es válido.", result.Message);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnError_WhenEmailAlreadyExists()
    {
        // Arrange
        var dto = new CreateClientDto
        {
            Name = "Pietro",
            Document = "123456789",
            Email = "existing@gmail.com",
            Password = "Test123!"
        };

        var existingUser = new ApplicationUser
        {
            Id = "user-123",
            Email = "existing@gmail.com",
            UserName = "existing@gmail.com"
        };

        _mockUserManager
            .Setup(um => um.FindByEmailAsync(dto.Email))
            .ReturnsAsync(existingUser);

        // Act
        var result = await _service.CreateAsync(dto);
        
        // Assert
        Assert.False(result.Success);
        Assert.Equal("Ya existe un usuario con este correo electrónico.", result.Message);
        _mockUserManager.Verify(um => um.FindByEmailAsync(dto.Email), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnSuccess_WhenDataIsValid()
    {
        // Arrange
        var dto = new CreateClientDto
        {
            Name = "Pietro",
            LastName = "Rossi",
            Document = "123456789",
            Email = "pietro@gmail.com",
            Phone = "1234567890",
            Address = "Calle 123",
            Password = "Test123!"
        };

        _mockUserManager
            .Setup(um => um.FindByEmailAsync(dto.Email))
            .ReturnsAsync((ApplicationUser?)null);

        _mockUserManager
            .Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

        _mockUserManager
            .Setup(um => um.AddToRoleAsync(It.IsAny<ApplicationUser>(), "Client"))
            .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _service.CreateAsync(dto);
        
        // Assert
        Assert.True(result.Success);
        Assert.Equal("Cliente creado correctamente.", result.Message);
        
        _mockUserManager.Verify(um => um.FindByEmailAsync(dto.Email), Times.Once);
        _mockUserManager.Verify(um => um.CreateAsync(It.IsAny<ApplicationUser>(), dto.Password), Times.Once);
        _mockUserManager.Verify(um => um.AddToRoleAsync(It.IsAny<ApplicationUser>(), "Client"), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnError_WhenUserCreationFails()
    {
        // Arrange
        var dto = new CreateClientDto
        {
            Name = "Pietro",
            Document = "123456789",
            Email = "pietro@gmail.com",
            Password = "Test123!"
        };

        _mockUserManager
            .Setup(um => um.FindByEmailAsync(dto.Email))
            .ReturnsAsync((ApplicationUser?)null);

        var identityErrors = new[]
        {
            new IdentityError { Description = "Password too weak" }
        };

        _mockUserManager
            .Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Failed(identityErrors));

        // Act
        var result = await _service.CreateAsync(dto);
        
        // Assert
        Assert.False(result.Success);
        Assert.Contains("Error al crear usuario", result.Message);
        Assert.Contains("Password too weak", result.Message);
    }

    [Fact]
    public async Task CreateAsync_ShouldUseDefaultPassword_WhenPasswordIsNull()
    {
        // Arrange
        var dto = new CreateClientDto
        {
            Name = "Pietro",
            Document = "123456789",
            Email = "pietro@gmail.com",
            Password = null
        };

        _mockUserManager
            .Setup(um => um.FindByEmailAsync(dto.Email))
            .ReturnsAsync((ApplicationUser?)null);

        _mockUserManager
            .Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), "DefaultPassword123!"))
            .ReturnsAsync(IdentityResult.Success);

        _mockUserManager
            .Setup(um => um.AddToRoleAsync(It.IsAny<ApplicationUser>(), "Client"))
            .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _service.CreateAsync(dto);
        
        // Assert
        Assert.True(result.Success);
        _mockUserManager.Verify(um => um.CreateAsync(It.IsAny<ApplicationUser>(), "DefaultPassword123!"), Times.Once);
    }

    #endregion

    #region UpdateAsync Tests

    [Fact]
    public async Task UpdateAsync_ShouldReturnError_WhenUserNotFound()
    {
        // Arrange
        var dto = new UpdateClientDto
        {
            Id = "non-existent-id",
            Name = "Pietro",
            Document = "123456789",
            Email = "pietro@gmail.com"
        };

        _mockUserManager
            .Setup(um => um.FindByIdAsync(dto.Id))
            .ReturnsAsync((ApplicationUser?)null);

        // Act
        var result = await _service.UpdateAsync(dto);
        
        // Assert
        Assert.False(result.Success);
        Assert.Equal("Cliente no encontrado.", result.Message);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnError_WhenUserIsNotClient()
    {
        // Arrange
        var dto = new UpdateClientDto
        {
            Id = "admin-id",
            Name = "Pietro",
            Document = "123456789",
            Email = "pietro@gmail.com"
        };

        var adminUser = new ApplicationUser
        {
            Id = "admin-id",
            Email = "admin@gmail.com"
        };

        _mockUserManager
            .Setup(um => um.FindByIdAsync(dto.Id))
            .ReturnsAsync(adminUser);

        _mockUserManager
            .Setup(um => um.GetRolesAsync(adminUser))
            .ReturnsAsync(new List<string> { "Admin" });

        // Act
        var result = await _service.UpdateAsync(dto);
        
        // Assert
        Assert.False(result.Success);
        Assert.Equal("El usuario no es un cliente.", result.Message);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnSuccess_WhenDataIsValid()
    {
        // Arrange
        var dto = new UpdateClientDto
        {
            Id = "client-123",
            Name = "Pietro Updated",
            LastName = "Rossi Updated",
            Document = "987654321",
            Email = "updated@gmail.com",
            Phone = "9876543210",
            Address = "Nueva Calle 456"
        };

        var existingUser = new ApplicationUser
        {
            Id = "client-123",
            Name = "Pietro",
            LastName = "Rossi",
            Email = "old@gmail.com"
        };

        _mockUserManager
            .Setup(um => um.FindByIdAsync(dto.Id))
            .ReturnsAsync(existingUser);

        _mockUserManager
            .Setup(um => um.GetRolesAsync(existingUser))
            .ReturnsAsync(new List<string> { "Client" });

        _mockUserManager
            .Setup(um => um.UpdateAsync(It.IsAny<ApplicationUser>()))
            .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _service.UpdateAsync(dto);
        
        // Assert
        Assert.True(result.Success);
        Assert.Equal("Cliente actualizado correctamente.", result.Message);
        
        _mockUserManager.Verify(um => um.UpdateAsync(It.Is<ApplicationUser>(u =>
            u.Name == dto.Name &&
            u.LastName == dto.LastName &&
            u.Email == dto.Email &&
            u.PhoneNumber == dto.Phone &&
            u.Document == dto.Document &&
            u.Address == dto.Address
        )), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnError_WhenUpdateFails()
    {
        // Arrange
        var dto = new UpdateClientDto
        {
            Id = "client-123",
            Name = "Pietro",
            Document = "123456789",
            Email = "pietro@gmail.com"
        };

        var existingUser = new ApplicationUser { Id = "client-123" };

        _mockUserManager
            .Setup(um => um.FindByIdAsync(dto.Id))
            .ReturnsAsync(existingUser);

        _mockUserManager
            .Setup(um => um.GetRolesAsync(existingUser))
            .ReturnsAsync(new List<string> { "Client" });

        var identityErrors = new[]
        {
            new IdentityError { Description = "Email already taken" }
        };

        _mockUserManager
            .Setup(um => um.UpdateAsync(It.IsAny<ApplicationUser>()))
            .ReturnsAsync(IdentityResult.Failed(identityErrors));

        // Act
        var result = await _service.UpdateAsync(dto);
        
        // Assert
        Assert.False(result.Success);
        Assert.Contains("Error al actualizar", result.Message);
        Assert.Contains("Email already taken", result.Message);
    }

    #endregion

    #region DeleteAsync Tests (string id)

    [Fact]
    public async Task DeleteAsync_WithStringId_ShouldReturnError_WhenUserNotFound()
    {
        // Arrange
        var userId = "non-existent-id";

        _mockUserManager
            .Setup(um => um.FindByIdAsync(userId))
            .ReturnsAsync((ApplicationUser?)null);

        // Act
        var result = await _service.DeleteAsync(userId);
        
        // Assert
        Assert.False(result.Success);
        Assert.Equal("Cliente no encontrado.", result.Message);
    }

    [Fact]
    public async Task DeleteAsync_WithStringId_ShouldReturnError_WhenUserIsNotClient()
    {
        // Arrange
        var userId = "admin-id";
        var adminUser = new ApplicationUser { Id = userId };

        _mockUserManager
            .Setup(um => um.FindByIdAsync(userId))
            .ReturnsAsync(adminUser);

        _mockUserManager
            .Setup(um => um.GetRolesAsync(adminUser))
            .ReturnsAsync(new List<string> { "Admin" });

        // Act
        var result = await _service.DeleteAsync(userId);
        
        // Assert
        Assert.False(result.Success);
        Assert.Equal("El usuario no es un cliente.", result.Message);
    }

    [Fact]
    public async Task DeleteAsync_WithStringId_ShouldReturnSuccess_WhenClientIsDeleted()
    {
        // Arrange
        var userId = "client-123";
        var clientUser = new ApplicationUser { Id = userId, Name = "Pietro" };

        _mockUserManager
            .Setup(um => um.FindByIdAsync(userId))
            .ReturnsAsync(clientUser);

        _mockUserManager
            .Setup(um => um.GetRolesAsync(clientUser))
            .ReturnsAsync(new List<string> { "Client" });

        _mockUserManager
            .Setup(um => um.DeleteAsync(clientUser))
            .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _service.DeleteAsync(userId);
        
        // Assert
        Assert.True(result.Success);
        Assert.Equal("Cliente eliminado correctamente.", result.Message);
        _mockUserManager.Verify(um => um.DeleteAsync(clientUser), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WithStringId_ShouldReturnError_WhenDeleteFails()
    {
        // Arrange
        var userId = "client-123";
        var clientUser = new ApplicationUser { Id = userId };

        _mockUserManager
            .Setup(um => um.FindByIdAsync(userId))
            .ReturnsAsync(clientUser);

        _mockUserManager
            .Setup(um => um.GetRolesAsync(clientUser))
            .ReturnsAsync(new List<string> { "Client" });

        var identityErrors = new[]
        {
            new IdentityError { Description = "Cannot delete active user" }
        };

        _mockUserManager
            .Setup(um => um.DeleteAsync(clientUser))
            .ReturnsAsync(IdentityResult.Failed(identityErrors));

        // Act
        var result = await _service.DeleteAsync(userId);
        
        // Assert
        Assert.False(result.Success);
        Assert.Contains("Error al eliminar", result.Message);
        Assert.Contains("Cannot delete active user", result.Message);
    }

    #endregion

    #region DeleteAsync Tests (int id)

    [Fact]
    public async Task DeleteAsync_WithIntId_ShouldConvertToStringAndDelete()
    {
        // Arrange
        var userId = 123;
        var clientUser = new ApplicationUser { Id = "123", Name = "Pietro" };

        _mockUserManager
            .Setup(um => um.FindByIdAsync("123"))
            .ReturnsAsync(clientUser);

        _mockUserManager
            .Setup(um => um.GetRolesAsync(clientUser))
            .ReturnsAsync(new List<string> { "Client" });

        _mockUserManager
            .Setup(um => um.DeleteAsync(clientUser))
            .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _service.DeleteAsync(userId);
        
        // Assert
        Assert.True(result.Success);
        Assert.Equal("Cliente eliminado correctamente.", result.Message);
        _mockUserManager.Verify(um => um.FindByIdAsync("123"), Times.Once);
    }

    #endregion

    #region GetAllAsync Tests

    [Fact]
    public async Task GetAllAsync_ShouldReturnOnlyClientsWithCorrectMapping()
    {
        // Arrange
        var users = new List<ApplicationUser>
        {
            new ApplicationUser { Id = "1", Name = "Client1", Email = "client1@gmail.com" },
            new ApplicationUser { Id = "2", Name = "Admin", Email = "admin@gmail.com" },
            new ApplicationUser { Id = "3", Name = "Client2", Email = "client2@gmail.com" }
        };

        // ✅ Usar MockQueryable para que ToListAsync() funcione
        var mockUsers = users.AsQueryable().BuildMock();
        
        _mockUserManager
            .Setup(um => um.Users)
            .Returns(mockUsers);

        _mockUserManager
            .Setup(um => um.GetRolesAsync(users[0]))
            .ReturnsAsync(new List<string> { "Client" });

        _mockUserManager
            .Setup(um => um.GetRolesAsync(users[1]))
            .ReturnsAsync(new List<string> { "Admin" });

        _mockUserManager
            .Setup(um => um.GetRolesAsync(users[2]))
            .ReturnsAsync(new List<string> { "Client" });

        var expectedClientDtos = new List<ClientDto>
        {
            new ClientDto { Id = "1", Name = "Client1", Email = "client1@gmail.com" },
            new ClientDto { Id = "3", Name = "Client2", Email = "client2@gmail.com" }
        };

        _mockMapper
            .Setup(m => m.Map<List<ClientDto>>(It.IsAny<List<ApplicationUser>>()))
            .Returns(expectedClientDtos);

        // Act
        var result = await _service.GetAllAsync();
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.All(result, dto => Assert.NotNull(dto));
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoClientsExist()
    {
        // Arrange
        var users = new List<ApplicationUser>
        {
            new ApplicationUser { Id = "1", Name = "Admin", Email = "admin@gmail.com" }
        };

        // ✅ Usar MockQueryable para que ToListAsync() funcione
        var mockUsers = users.AsQueryable().BuildMock();
        
        _mockUserManager
            .Setup(um => um.Users)
            .Returns(mockUsers);

        _mockUserManager
            .Setup(um => um.GetRolesAsync(users[0]))
            .ReturnsAsync(new List<string> { "Admin" });

        _mockMapper
            .Setup(m => m.Map<List<ClientDto>>(It.IsAny<List<ApplicationUser>>()))
            .Returns(new List<ClientDto>());

        // Act
        var result = await _service.GetAllAsync();
        
        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    #endregion
}
using Microsoft.AspNetCore.Identity;
using Moq;

namespace firmness.Tests.TestHelpers;

/// <summary>
/// Helper para crear mocks de UserManager en tests unitarios
/// </summary>
public static class UserManagerMockHelper
{
    /// <summary>
    /// Crea un mock de UserManager<TUser> listo para usar en tests
    /// </summary>
    public static Mock<UserManager<TUser>> CreateMockUserManager<TUser>() where TUser : class
    {
        var userStoreMock = new Mock<IUserStore<TUser>>();
        
        var mockUserManager = new Mock<UserManager<TUser>>(
            userStoreMock.Object,
            null, // IOptions<IdentityOptions>
            null, // IPasswordHasher<TUser>
            null, // IEnumerable<IUserValidator<TUser>>
            null, // IEnumerable<IPasswordValidator<TUser>>
            null, // ILookupNormalizer
            null, // IdentityErrorDescriber
            null, // IServiceProvider
            null  // ILogger<UserManager<TUser>>
        );

        return mockUserManager;
    }
}
// csharp
using System.Threading.Tasks;
using firmness.Api.Controllers;
using firmness.Application.DTOs;
using firmness.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using RegisterRequest = firmness.Api.Controllers.RegisterRequest;

namespace firmness.Tests.Services;

public class AuthControllerTests
{
    private Mock<UserManager<ApplicationUser>> MockUserManager()
    {
        return new Mock<UserManager<ApplicationUser>>(
            Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null
        );
    }

    private Mock<SignInManager<ApplicationUser>> MockSignInManager(Mock<UserManager<ApplicationUser>> userManager)
    {
        return new Mock<SignInManager<ApplicationUser>>(
            userManager.Object,
            Mock.Of<Microsoft.AspNetCore.Http.IHttpContextAccessor>(),
            Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(),
            null, null, null, null
        );
    }

    [Fact]
    public async Task Login_ShouldReturnUnauthorized_WhenUserNotFound()
    {
        var userManager = MockUserManager();
        userManager.Setup(u => u.FindByEmailAsync("email@test.com"))
            .ReturnsAsync((ApplicationUser?)null);

        var controller = new AuthController(userManager.Object, MockSignInManager(userManager).Object, new ConfigurationBuilder().Build());

        var loginDto = new AuthController.LoginRequest { Email = "email@test.com", Password = "123" };
        var result = await controller.Login(loginDto);

        Assert.IsType<UnauthorizedObjectResult>(result);
    }

    [Fact]
    public async Task Register_ShouldReturnBadRequest_WhenUserExists()
    {
        var userManager = MockUserManager();
        userManager.Setup(u => u.FindByEmailAsync("test@gmail.com"))
            .ReturnsAsync(new ApplicationUser());

        var controller = new AuthController(userManager.Object, MockSignInManager(userManager).Object, new ConfigurationBuilder().Build());

        var registerDto = new RegisterRequest { Email = "test@gmail.com", Password = "pass" };
        var result = await controller.Register(registerDto);

        Assert.IsType<BadRequestObjectResult>(result);
    }
}

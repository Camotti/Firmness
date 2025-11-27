using System.Threading.Tasks;
using firmness.Api.Controllers;
using firmness.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;

namespace firmness.Tests.Controllers;

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

        var result = await controller.Login("email@test.com", "123");

        Assert.IsType<UnauthorizedObjectResult>(result);
    }

    [Fact]
    public async Task Register_ShouldReturnBadRequest_WhenUserExists()
    {
        var userManager = MockUserManager();
        userManager.Setup(u => u.FindByEmailAsync("test@gmail.com"))
            .ReturnsAsync(new ApplicationUser());

        var controller = new AuthController(userManager.Object, MockSignInManager(userManager).Object, new ConfigurationBuilder().Build());

        var result = await controller.Register("test@gmail.com", "pass");

        Assert.IsType<BadRequestObjectResult>(result);
    }
}
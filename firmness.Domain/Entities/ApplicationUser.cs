using Microsoft.AspNetCore.Identity;
namespace firmness.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullUserName { get; set; }
    }
}

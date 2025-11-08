using Microsoft.AspNetCore.Identity;
namespace firmness.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string? FullUserName { get; set; }
    }
}

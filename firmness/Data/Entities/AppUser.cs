using Microsoft.AspNetCore.Identity;

namespace firmness.Data.Entities
{
    public class AppUser : IdentityUser
    {
        public string? FullUserName { get; set; }
    }
}

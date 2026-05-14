using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.System
{
    public class ApplicationUserLogin : IdentityUserLogin<string>
    {
        public ApplicationUser User { get; set; } = null!;
    }
}

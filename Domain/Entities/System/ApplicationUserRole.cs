using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.System
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public ApplicationUser User { get; set; } = null!;
        public ApplicationRole Role { get; set; } = null!;
    }

}

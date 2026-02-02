using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.System
{
    public class ApplicationRoleClaim : IdentityRoleClaim<string>
    {
        public ApplicationRole Role { get; set; } = null!;

    }
}

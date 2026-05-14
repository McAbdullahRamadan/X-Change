using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.System
{
    public class ApplicationUserToken : IdentityUserToken<string>
    {
        public ApplicationUser User { get; set; } = null!;
    }

}

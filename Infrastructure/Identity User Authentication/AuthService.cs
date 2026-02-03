using Domain.Entities.System;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity_User_Authentication
{
    public class AuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        // لاحقًا: Login / JWT / RefreshToken
    }
}

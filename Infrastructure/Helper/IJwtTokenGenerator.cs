using Domain.Entities.System;
using System.Security.Claims;

namespace Infrastructure.Helper
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateTokenAsync(ApplicationUser user);
        Task<(string accessToken, RefreshToken refreshToken)> GenerateRefreshTokensAsync(ApplicationUser user);
        Task<List<Claim>> GetClaims(ApplicationUser user);
    }
}

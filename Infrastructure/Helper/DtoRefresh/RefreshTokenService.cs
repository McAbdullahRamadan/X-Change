using Domain.Entities.System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Helper.DtoRefresh
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly ApplicationDbContext _context;
        private readonly IJwtTokenGenerator _jwtService;
        private readonly UserManager<ApplicationUser> _userManager;

        public RefreshTokenService(
            ApplicationDbContext context,
            IJwtTokenGenerator jwtService,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _jwtService = jwtService;
            _userManager = userManager;
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(string refreshToken)
        {
            var existingToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(x => x.Token == refreshToken);

            if (existingToken == null || existingToken.IsExpired || existingToken.IsRevoked)
                throw new UnauthorizedAccessException("Invalid refresh token");

            // 🔥 Get user from token نفسه (مش من بره)
            var user = await _userManager.FindByIdAsync(existingToken.UserId);

            if (user == null)
                throw new UnauthorizedAccessException("User not found");

            // 🔥 ROTATION STEP 1 — Revoke Old Token
            existingToken.IsRevoked = true;
            existingToken.RevokedAt = DateTime.UtcNow;

            // 🔥 ROTATION STEP 2 — Generate New Tokens
            var (newAccessToken, refreshTokenEntity) =
     await _jwtService.GenerateRefreshTokensAsync(user);

            var newRefreshToken = refreshTokenEntity.Token;

            var refreshTokenEntityes = new RefreshToken
            {
                TokenSting = newRefreshToken,
                ExpireAt = DateTime.UtcNow.AddDays(7),
                UserName = user.Id
            };

            _context.RefreshTokens.Add(refreshTokenEntity);
            await _context.SaveChangesAsync();

            return new AuthResponseDto
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }


        public async Task RevokeTokenAsync(string refreshToken)
        {
            var token = await _context.RefreshTokens
                .FirstOrDefaultAsync(x => x.Token == refreshToken);

            if (token == null)
                return;

            token.IsRevoked = true;
            token.RevokedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }
}

using Infrastructure.Helper.DtoRefresh;

namespace Infrastructure.Helper
{
    public interface IRefreshTokenService
    {
        Task<AuthResponseDto> RefreshTokenAsync(string refreshToken);
        Task RevokeTokenAsync(string refreshToken);
    }
}

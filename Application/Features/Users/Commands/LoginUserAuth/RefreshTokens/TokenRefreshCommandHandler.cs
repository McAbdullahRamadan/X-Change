using Infrastructure.Helper;
using Infrastructure.Helper.DtoRefresh;
using MediatR;

namespace Application.Features.Users.Commands.LoginUserAuth.RefreshTokens
{
    public class TokenRefreshCommandHandler : IRequestHandler<TokenRefreshCommand, AuthResponseDto>
    {
        private readonly IRefreshTokenService _refreshTokenService;

        public TokenRefreshCommandHandler(IRefreshTokenService refreshTokenService)
        {
            _refreshTokenService = refreshTokenService;
        }

        public async Task<AuthResponseDto> Handle(
            TokenRefreshCommand request,
            CancellationToken cancellationToken)
        {
            return await _refreshTokenService.RefreshTokenAsync(request.RefreshToken);
        }
    }
}

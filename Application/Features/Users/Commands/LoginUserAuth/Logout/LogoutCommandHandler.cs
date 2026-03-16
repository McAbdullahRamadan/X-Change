using Infrastructure.Helper;
using MediatR;

namespace Application.Features.Users.Commands.LoginUserAuth.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Unit>
    {
        private readonly IRefreshTokenService _refreshTokenService;

        public LogoutCommandHandler(IRefreshTokenService refreshTokenService)
        {
            _refreshTokenService = refreshTokenService;
        }

        public async Task<Unit> Handle(
            LogoutCommand request,
            CancellationToken cancellationToken)
        {
            await _refreshTokenService.RevokeTokenAsync(request.RefreshToken);

            return Unit.Value;
        }
    }
}

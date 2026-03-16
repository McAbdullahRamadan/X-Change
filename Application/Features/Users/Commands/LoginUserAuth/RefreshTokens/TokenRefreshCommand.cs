using Infrastructure.Helper.DtoRefresh;
using MediatR;

namespace Application.Features.Users.Commands.LoginUserAuth.RefreshTokens
{
    public record TokenRefreshCommand(string RefreshToken)
     : IRequest<AuthResponseDto>;
}

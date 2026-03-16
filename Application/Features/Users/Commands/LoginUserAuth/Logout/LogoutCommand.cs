using MediatR;

namespace Application.Features.Users.Commands.LoginUserAuth.Logout
{
    public record LogoutCommand(string RefreshToken)
        : IRequest<Unit>;
}

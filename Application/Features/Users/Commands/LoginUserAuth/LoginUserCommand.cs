using Application.DTOs.LoginUserDto;
using Application.Responses;
using MediatR;

namespace Application.Features.Users.Commands.LoginUserAuth
{
    public class LoginUserCommand : IRequest<DataResponse<AuthResultDto>>
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}

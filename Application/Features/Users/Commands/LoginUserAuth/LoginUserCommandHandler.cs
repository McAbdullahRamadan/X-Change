using Application.DTOs.LoginUserDto;
using Application.Interfaces;
using Application.Responses;
using MediatR;

namespace Application.Features.Users.Commands.LoginUserAuth
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, DataResponse<AuthResultDto>>
    {
        private readonly IAuthService _authService;

        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<DataResponse<AuthResultDto>> Handle(
            LoginUserCommand request,
            CancellationToken cancellationToken)
        {
            return await _authService.LoginAsync(request.Email, request.Password);
        }
    }

}

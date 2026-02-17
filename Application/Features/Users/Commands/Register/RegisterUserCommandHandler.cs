using Application.Interfaces;
using Application.Responses;
using MediatR;

namespace Application.Features.Users.Commands.Register
{
    public class RegisterUserCommandHandler
     : IRequestHandler<RegisterUserCommand, DataResponse<string>>
    {
        private readonly IAuthService _authService;

        public RegisterUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }


        public async Task<DataResponse<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {

            return await _authService.RegisterAsync(request);
        }
    }
}

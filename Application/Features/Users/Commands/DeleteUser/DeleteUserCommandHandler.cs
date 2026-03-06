using Application.Interfaces;
using Application.Responses;
using MediatR;

namespace Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DataResponse<string>>
    {
        private readonly IAuthService _authService;

        public DeleteUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<DataResponse<string>> Handle(
            DeleteUserCommand request,
            CancellationToken cancellationToken)
        {
            return await _authService.DeleteUserAsync(request.UserId);
        }
    }
}


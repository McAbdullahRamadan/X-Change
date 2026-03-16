using Application.Interfaces;
using Application.Responses;
using MediatR;

namespace Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, DataResponse<string>>
    {
        private readonly IAuthService _authService;

        public UpdateUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<DataResponse<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            // التحقق من أن request مش null
            if (request == null)
                return DataResponse<string>.BadRequest([""]);

            // التحقق من وجود Id
            if (string.IsNullOrEmpty(request.Id))
                return DataResponse<string>.BadRequest([""]);

            return await _authService.UpdateUserAsync(request);
        }
    }
}

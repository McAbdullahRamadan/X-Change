using Application.Interfaces;
using Application.Responses;
using MediatR;

namespace Application.Features.Users.Query.ConfirmEmail
{
    public class ConfirmEmailQueryHandle : IRequestHandler<ConfirmEmailQuery, DataResponse<string>>
    {
        private readonly IAuthService _authService;
        public ConfirmEmailQueryHandle(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<DataResponse<string>> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
        {
            var confirmEmail = await _authService.ConfirmEmail(request.userId, request.code);
            if (confirmEmail == "ErrorWhenConfirmEmail")
                return DataResponse<string>.BadRequest(new[] { "Error When Confirm Email" });
            return DataResponse<string>.Success("The email was successfully confirmed.");

        }
    }
}

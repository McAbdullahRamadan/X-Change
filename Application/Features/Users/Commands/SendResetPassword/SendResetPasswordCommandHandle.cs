using Application.Interfaces;
using Application.Responses;
using MediatR;

namespace Application.Features.Users.Commands.SendResetPassword
{
    public class SendResetPasswordCommandHandle : IRequestHandler<SendResetPasswordCommand, DataResponse<string>>
    {
        private readonly IAuthService _authService;
        public SendResetPasswordCommandHandle(IAuthService authService)
        {
            _authService = authService;

        }

        public async Task<DataResponse<string>> Handle(SendResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _authService.SendResetPasswordCode(request.Email);
            switch (result)

            {
                case "UserNotFound": return DataResponse<string>.BadRequest(new[] { "User Not Found" });
                case "ErrorInUpdateUser": return DataResponse<string>.BadRequest(new[] { "Erro rIn Update User" });
                case "Failed": return DataResponse<string>.BadRequest(new[] { "Failed" });
                case "Success": return DataResponse<string>.Success("User Send Code successfully.");
                default: return DataResponse<string>.BadRequest(new[] { "Please Try Again" });

            }
        }
    }
}

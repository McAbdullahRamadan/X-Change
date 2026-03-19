using Application.Interfaces;
using Application.Responses;
using MediatR;

namespace Application.Features.Users.Query.ConfirmResetPassword
{
    public class ConfirmResetPasswordHandle : IRequestHandler<ConfirmResetPasswordQuery, DataResponse<string>>
    {
        private readonly IAuthService _authService;
        public ConfirmResetPasswordHandle(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));

        }
        public async Task<DataResponse<string>> Handle(ConfirmResetPasswordQuery request, CancellationToken cancellationToken)
        {
            // Validate request
            if (request == null)
                return DataResponse<string>.BadRequest(new[] { "Invalid request" });

            var result = await _authService.ConfirmResetPasswordCode(request.Code, request.Email);

            switch (result)
            {
                case "UserNotFound":
                    return DataResponse<string>.BadRequest(new[] { "User not found" });

                case "NoCodeFound":
                    return DataResponse<string>.BadRequest(new[] { "No reset code found for this user" });

                case "Failed":
                    return DataResponse<string>.BadRequest(new[] { "Invalid or expired code" });

                case "Success":
                    return DataResponse<string>.Success("Reset code verified successfully. You can now reset your password.");

                case "EmailRequired":
                    return DataResponse<string>.BadRequest(new[] { "Email is required" });

                case "CodeRequired":
                    return DataResponse<string>.BadRequest(new[] { "Reset code is required" });

                default:
                    return DataResponse<string>.BadRequest(new[] { "An error occurred" });
            }
        }
    }
}

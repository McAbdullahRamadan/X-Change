using Application.Interfaces;
using Application.Responses;
using MediatR;

namespace Application.Features.Users.Commands.ResetPassword
{
    public class ResetPasswordCommandHandle : IRequestHandler<ResetPasswordCommand, DataResponse<string>>
    {
        private readonly IAuthService _authService;
        public ResetPasswordCommandHandle(IAuthService authService)
        {
            _authService = authService;

        }
        public async Task<DataResponse<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _authService.ResetPassword(request.Email, request.Password);
            switch (result)

            {
                case "UserNotFound": return DataResponse<string>.BadRequest(new[] { "User Not Found" });
                case "Failed": return DataResponse<string>.BadRequest(new[] { "Invalid Code" });
                case "Success": return DataResponse<string>.Success("successfully");
                default: return DataResponse<string>.BadRequest(new[] { "Invalid Code" });

            }
        }
    }
}

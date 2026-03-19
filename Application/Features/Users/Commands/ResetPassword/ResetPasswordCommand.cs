using Application.Responses;
using MediatR;

namespace Application.Features.Users.Commands.ResetPassword
{
    public class ResetPasswordCommand : IRequest<DataResponse<string>>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}

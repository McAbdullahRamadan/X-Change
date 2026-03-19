using Application.Responses;
using MediatR;

namespace Application.Features.Users.Commands.SendResetPassword
{
    public class SendResetPasswordCommand : IRequest<DataResponse<string>>
    {
        public string Email { get; set; }
    }
}

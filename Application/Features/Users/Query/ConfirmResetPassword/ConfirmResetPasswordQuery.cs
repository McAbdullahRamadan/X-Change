using Application.Responses;
using MediatR;

namespace Application.Features.Users.Query.ConfirmResetPassword
{
    public class ConfirmResetPasswordQuery : IRequest<DataResponse<string>>
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}

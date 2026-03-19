using Application.Responses;
using MediatR;

namespace Application.Features.Users.Query.ConfirmEmail
{
    public class ConfirmEmailQuery : IRequest<DataResponse<string>>
    {
        public string userId { get; set; }
        public string code { get; set; }
    }
}

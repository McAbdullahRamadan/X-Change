using Application.DTOs.UserById;
using Application.Responses;
using MediatR;

namespace Application.Features.Users.Query
{
    public class GetUserByIdQuery : IRequest<DataResponse<UserDto>>
    {
        public string UserId { get; set; }

        public GetUserByIdQuery(string userId)
        {
            UserId = userId;
        }
    }
}

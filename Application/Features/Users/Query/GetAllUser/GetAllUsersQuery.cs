using Application.DTOs.UserById;
using Application.Models;
using Application.Responses;
using MediatR;

namespace Application.Features.Users.Query.GetAllUser
{
    public class GetAllUsersQuery : IRequest<DataResponse<PaginatedList<UserDto>>>
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string? Search { get; set; }
    }
}

using Application.DTOs.UserById;
using Application.Interfaces;
using Application.Models;
using Application.Responses;
using MediatR;

namespace Application.Features.Users.Query.GetAllUser
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, DataResponse<PaginatedList<UserDto>>>
    {
        private readonly IAuthService _authService;

        public GetAllUsersQueryHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<DataResponse<PaginatedList<UserDto>>> Handle(
            GetAllUsersQuery request,
            CancellationToken cancellationToken)
        {
            return await _authService.GetAllUsersAsync(
           request.Page,
           request.PageSize);
        }
    }
}

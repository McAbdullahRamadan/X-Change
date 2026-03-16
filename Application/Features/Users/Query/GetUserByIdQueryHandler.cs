using Application.DTOs.UserById;
using Application.Interfaces;
using Application.Responses;
using MediatR;

namespace Application.Features.Users.Query
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, DataResponse<UserDto>>
    {
        private readonly IAuthService _authService;
        public GetUserByIdQueryHandler(IAuthService authService)
        {
            _authService = authService;

        }
        public async Task<DataResponse<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _authService.GetUserByIdAsync(request.UserId);
        }
    }
}

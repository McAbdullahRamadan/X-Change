using Application.DTOs.LoginUserDto;
using Application.Features.Users.Commands.Register;
using Application.Responses;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<DataResponse<string>> RegisterAsync(RegisterUserCommand request);
        Task<DataResponse<AuthResultDto>> LoginAsync(string email, string password);
    }
}

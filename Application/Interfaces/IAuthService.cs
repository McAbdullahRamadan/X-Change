using Application.DTOs.LoginUserDto;
using Application.DTOs.UserById;
using Application.Features.Users.Commands.Register;
using Application.Features.Users.Commands.UpdateUser;
using Application.Models;
using Application.Responses;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<DataResponse<PaginatedList<UserDto>>> GetAllUsersAsync(int page, int pageSize);
        Task<DataResponse<UserDto>> GetUserByIdAsync(string userId);

        Task<DataResponse<string>> UpdateUserAsync(UpdateUserCommand command);
        public Task<string> SendResetPasswordCode(string Email);
        public Task<string> ResetPassword(string email, string Password);
        public Task<string> ConfirmEmail(string? userId, string? code);
        public Task<string> ConfirmResetPasswordCode(string code, string email);
        Task<DataResponse<string>> DeleteUserAsync(string userId);
        Task<DataResponse<string>> RegisterAsync(RegisterUserCommand request);
        Task<DataResponse<AuthResultDto>> LoginAsync(string email, string password);
    }
}

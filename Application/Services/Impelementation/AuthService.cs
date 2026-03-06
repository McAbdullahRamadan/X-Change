using Application.DTOs.LoginUserDto;
using Application.DTOs.UserById;
using Application.Features.Users.Commands.Register;
using Application.Features.Users.Commands.UpdateUser;
using Application.Interfaces;
using Application.Models;
using Application.Responses;
using Domain.Entities.System;
using Infrastructure.Helper;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Impelementation
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(UserManager<ApplicationUser> userManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<DataResponse<string>> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return DataResponse<string>.NotFound("User not found");

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
                return DataResponse<string>.BadRequest(result.Errors.Select(e => e.Description));

            return DataResponse<string>.Deleted();
        }

        public async Task<DataResponse<PaginatedList<UserDto>>> GetAllUsersAsync(int page, int pageSize)
        {
            var query = _userManager.Users
       .Select(x => new UserDto
       {
           Id = x.Id,
           PhoneNumber = x.PhoneNumber,
           University = x.University,
           Major = x.Major,
           UserName = x.UserName,
           Email = x.Email,
           FirstName = x.FirstName,
           LastName = x.LastName,
           City = x.City,
           Country = x.Country
       });

            var paginatedUsers = await PaginatedList<UserDto>
                .CreateAsync(query, page, pageSize);

            return DataResponse<PaginatedList<UserDto>>
                .Success(paginatedUsers, "");
        }

        public async Task<DataResponse<UserDto>> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return DataResponse<UserDto>.NotFound("User not found");

            var dto = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                City = user.City,
                Country = user.Country,
                University = user.University,
                Major = user.Major
            };

            return DataResponse<UserDto>.Success(dto, "");
        }

        public async Task<DataResponse<AuthResultDto>> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return DataResponse<AuthResultDto>.Unauthorized(
                    new[] { "The email address is not available." });

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);

            if (!isPasswordValid)
                return DataResponse<AuthResultDto>.Unauthorized(
                    new[] { "Incorrect your password" });
            var (accessToken, refreshToken) = await _jwtTokenGenerator.GenerateRefreshTokensAsync(user);

            user.RefreshTokens.Add(refreshToken);
            await _userManager.UpdateAsync(user);

            var result = new AuthResultDto
            {
                Token = accessToken,
                RefreshToken = refreshToken.Token,
                Email = user.Email!,
                UserName = user.UserName!
            };

            return DataResponse<AuthResultDto>.Success(result, "Login successfully ");
        }

        public async Task<DataResponse<string>> RegisterAsync(RegisterUserCommand request)
        {
            {

                var emailExists = await _userManager.FindByEmailAsync(request.Email);
                if (emailExists != null)
                    return DataResponse<string>.BadRequest(new[]
                    {
                "Email is already exist."
            });


                var userNameExists = await _userManager.FindByNameAsync(request.UserName);
                if (userNameExists != null)
                    return DataResponse<string>.BadRequest(new[]
                    {
                "Username is already exist."
            });


                var user = new ApplicationUser
                {
                    UserName = request.UserName,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PersonGender = request.PersonGender,
                    DateOfBirth = request.DateOfBirth,
                    City = request.City,
                    Country = request.Country,
                    NationalId = request.NationalId,
                    University = request.University,
                    Major = request.Major,
                    IsActive = true
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    return DataResponse<string>.BadRequest(
                        result.Errors.Select(e => e.Description)
                    );
                }



                return DataResponse<string>.Created("User registered successfully.");
            }
        }

        public async Task<DataResponse<string>> UpdateUserAsync(UpdateUserCommand command)
        {
            var user = await _userManager.FindByIdAsync(command.Id);

            if (user == null)
                return DataResponse<string>.NotFound("User not found");
            user.FirstName = command.FirstName;
            user.LastName = command.LastName;
            user.Email = command.Email;
            user.UserName = command.UserName;
            user.DateOfBirth = command.DateOfBirth;
            user.PersonGender = command.PersonGender;
            user.PhoneNumber = command.PhoneNumber;
            user.City = command.City;
            user.Country = command.Country;
            user.University = command.University;
            user.Major = command.Major;


            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return DataResponse<string>.BadRequest(result.Errors.Select(e => e.Description));

            return DataResponse<string>.Success("User updated successfully");
        }
    }
}

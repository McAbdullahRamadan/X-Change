using Application.Features.Users.Commands.Register;
using Application.Interfaces;
using Application.Responses;
using Domain.Entities.System;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Impelementation
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<DataResponse<string>> RegisterAsync(RegisterUserCommand request)
        {
            {

                var emailExists = await _userManager.FindByEmailAsync(request.Email);
                if (emailExists != null)
                    return DataResponse<string>.Failure(new[]
                    {
                "Email is already exist."
            });


                var userNameExists = await _userManager.FindByNameAsync(request.UserName);
                if (userNameExists != null)
                    return DataResponse<string>.Failure(new[]
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
                    return DataResponse<string>.Failure(
                        result.Errors.Select(e => e.Description)
                    );
                }



                return DataResponse<string>.Created("User registered successfully.");
            }
        }
    }
}

using Application.DTOs;
using Domain.Entities.System;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Users.Commands.Register
{
    public class RegisterUserCommandHandler
     : IRequestHandler<RegisterUserCommand, AuthResultDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        public async Task<AuthResultDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {

            var emailExists = await _userManager.FindByEmailAsync(request.Email);
            if (emailExists != null)
            {
                return AuthResultDto.Failure("Email is already registered.");
            }
            var userNameExists = await _userManager.FindByNameAsync(request.UserName);
            if (userNameExists != null)
            {
                return AuthResultDto.Failure("Username is already taken.");
            }
            var dto = request;
            if (dto.Email != dto.ConfirmEmail)
                throw new ValidationException("Emails do not match");

            var user = new ApplicationUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PersonGender = dto.PersonGender,
                DateOfBirth = dto.DateOfBirth,
                City = dto.City,
                Country = dto.Country,
                NationalId = dto.NationalId,
                University = dto.University,
                Major = dto.Major,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return AuthResultDto.Failure(errors);
            }

            return AuthResultDto.Success();
        }
    }
}

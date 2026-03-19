using Application.DTOs.LoginUserDto;
using Application.DTOs.UserById;
using Application.Features.Users.Commands.Register;
using Application.Features.Users.Commands.UpdateUser;
using Application.Interfaces;
using Application.Models;
using Application.Responses;
using Domain.Entities.System;
using Infrastructure;
using Infrastructure.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Impelementation
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;
        private readonly IEmailsService _emailsService;
        private readonly IUrlHelper _urlHelper;

        public AuthService(UserManager<ApplicationUser> userManager, IJwtTokenGenerator jwtTokenGenerator, ApplicationDbContext context, IEmailsService emailsService, IHttpContextAccessor httpContextAccesso, IUrlHelper urlHelper)
        {
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _context = context;
            _emailsService = emailsService;
            _httpContextAccessor = httpContextAccesso;
            _urlHelper = urlHelper;
        }

        public async Task<string> ConfirmEmail(string? userId, string? code)
        {
            if (userId == null || code == null)
                return "ErrorWhenConfirmEmail";
            var user = await _userManager.FindByIdAsync(userId);
            var confirmEmail = await _userManager.ConfirmEmailAsync(user, code);
            if (!confirmEmail.Succeeded)
                return "ErrorWhenConfirmEmail";
            return "Success";
        }

        public async Task<string> ConfirmResetPasswordCode(string code, string email)
        {
            // Validate input
            if (string.IsNullOrEmpty(email))
                return "EmailRequired";

            if (string.IsNullOrEmpty(code))
                return "CodeRequired";

            // Get User
            var user = await _userManager.FindByEmailAsync(email);

            // User Not Exist => not found
            if (user == null)
                return "UserNotFound";

            // Check if user has a reset code
            if (string.IsNullOrEmpty(user.Code))
                return "NoCodeFound";

            // Compare codes
            if (user.Code == code)
            {
                // Clear the code after successful verification (security best practice)
                user.Code = null;
                await _userManager.UpdateAsync(user);

                return "Success";
            }

            return "Failed"; // Fixed spelling
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
                return DataResponse<AuthResultDto>.NotFound("The email address is not available.");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);

            if (!isPasswordValid)
                return DataResponse<AuthResultDto>.BadRequest(
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
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var requestAccessor = _httpContextAccessor.HttpContext.Request;
                var returnUrl = requestAccessor.Scheme + "://" + requestAccessor.Host +
                    _urlHelper.Action("ConfirmEmail", "Authentication", new { userId = user.Id, code = code });
                var messsage = $"Click Link ConfirmEmail : <a href='{returnUrl}'></a>";

                await _emailsService.SendEmails(user.Email, returnUrl, "Confirm Email");


                return DataResponse<string>.Created("User registered successfully.");
            }
        }

        public async Task<string> ResetPassword(string email, string Password)
        {
            var trans = await _context.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                    return "UserNotFound";
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, Password);
                await trans.CommitAsync();
                return "Success";

            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return "Falied";
            }
        }

        public async Task<string> SendResetPasswordCode(string email)
        {
            var trans = await _context.Database.BeginTransactionAsync();
            try
            {
                //Get User
                var user = await _userManager.FindByEmailAsync(email);
                //User Not exist =>Not Found
                if (user == null)
                    return "UserNotFound";
                //Generate Random Number
                Random Generator = new Random();
                string randomnumber = Generator.Next(0, 1000000).ToString("D6");
                //Update Database
                user.Code = randomnumber;
                var updatedata = await _userManager.UpdateAsync(user);
                if (!updatedata.Succeeded)
                    return "ErrorInUpdateUser";
                var message = "XChange Code To Reset Password : " + user.Code + " Please do not share the code to protect your data.";
                //Send Code To Email
                await _emailsService.SendEmails(user.Email, message, "Reset Password");
                await trans.CommitAsync();
                //Success
                return "Success";

            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return "Failed";
            }
        }

        public async Task<DataResponse<string>> UpdateUserAsync(UpdateUserCommand command)
        {
            if (command == null)
                return DataResponse<string>.BadRequest([""]);

            var user = await _userManager.FindByIdAsync(command.Id);

            if (user == null)
                return DataResponse<string>.NotFound("User not found");

            // تحديث البيانات مع التحقق من null
            user.FirstName = command.FirstName ?? user.FirstName;
            user.LastName = command.LastName ?? user.LastName;
            user.Email = command.Email ?? user.Email;
            user.UserName = command.UserName ?? user.UserName;
            user.PhoneNumber = command.PhoneNumber ?? user.PhoneNumber;
            user.City = command.City ?? user.City;
            user.Country = command.Country ?? user.Country;
            user.University = command.University ?? user.University;
            user.Major = command.Major ?? user.Major;

            // الحقول الإضافية


            if (command.DateOfBirth.HasValue)
                user.DateOfBirth = command.DateOfBirth.Value;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return DataResponse<string>.BadRequest(result.Errors.Select(e => e.Description));

            return DataResponse<string>.Success("User updated successfully");
        }
    }
}

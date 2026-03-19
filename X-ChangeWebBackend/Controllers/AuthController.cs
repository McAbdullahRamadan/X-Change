using Application.Features.Users.Commands.DeleteUser;
using Application.Features.Users.Commands.LoginUserAuth;
using Application.Features.Users.Commands.LoginUserAuth.Logout;
using Application.Features.Users.Commands.LoginUserAuth.RefreshTokens;
using Application.Features.Users.Commands.Register;
using Application.Features.Users.Commands.ResetPassword;
using Application.Features.Users.Commands.SendResetPassword;
using Application.Features.Users.Commands.UpdateUser;
using Application.Features.Users.Query;
using Application.Features.Users.Query.ConfirmEmail;
using Application.Features.Users.Query.ConfirmResetPassword;
using Application.Features.Users.Query.GetAllUser;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using X_ChangeWebBackend.BaseControl;

namespace X_ChangeWebBackend.Controllers
{


    public class AuthController : BaseController
    {

        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] GetAllUsersQuery query)
        {
            var result = await Mediator.Send(query);

            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var result = await Mediator.Send(new GetUserByIdQuery(id));
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUserCommand command)
        {

            if (command == null)
                return BadRequest("Command cannot be null");

            if (string.IsNullOrEmpty(command.Id))
                return BadRequest("User Id is required");

            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await Mediator.Send(new DeleteUserCommand(id));
            return Ok(result);
        }
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _authService.GetUserByIdAsync(userId);

            return Ok(user);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("SendResetPasswordCommand")]
        public async Task<IActionResult> SendResetPassword([FromQuery] SendResetPasswordCommand Command)
        {
            var response = await Mediator.Send(Command);
            return Ok(response);
        }
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordCommand Command)
        {
            var response = await Mediator.Send(Command);
            return Ok(response);
        }
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailQuery Query)
        {
            var response = await Mediator.Send(Query);
            return Ok(response);
        }
        [HttpGet("ConfirmResetPassword")]
        public async Task<IActionResult> ConfirmResetPasswordCode([FromQuery] ConfirmResetPasswordQuery Query)
        {
            var response = await Mediator.Send(Query);
            return Ok(response);
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] string refreshToken)
        {
            var result = await Mediator.Send(new TokenRefreshCommand(refreshToken));
            return Ok(result);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] string refreshToken)
        {
            await Mediator.Send(new LogoutCommand(refreshToken));
            return Ok();
        }

    }
}

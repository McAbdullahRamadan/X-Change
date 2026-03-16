using Application.Features.Users.Commands.DeleteUser;
using Application.Features.Users.Commands.LoginUserAuth;
using Application.Features.Users.Commands.LoginUserAuth.Logout;
using Application.Features.Users.Commands.LoginUserAuth.RefreshTokens;
using Application.Features.Users.Commands.Register;
using Application.Features.Users.Commands.UpdateUser;
using Application.Features.Users.Query;
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
            // تأكد إن البيانات مش null
            if (command == null)
                return BadRequest("Command cannot be null");

            // تأكد من وجود Id
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

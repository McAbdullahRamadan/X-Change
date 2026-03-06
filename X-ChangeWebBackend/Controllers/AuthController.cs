using Application.Features.Users.Commands.DeleteUser;
using Application.Features.Users.Commands.LoginUserAuth;
using Application.Features.Users.Commands.LoginUserAuth.Logout;
using Application.Features.Users.Commands.LoginUserAuth.RefreshTokens;
using Application.Features.Users.Commands.Register;
using Application.Features.Users.Commands.UpdateUser;
using Application.Features.Users.Query;
using Application.Features.Users.Query.GetAllUser;
using Microsoft.AspNetCore.Mvc;
using X_ChangeWebBackend.BaseControl;

namespace X_ChangeWebBackend.Controllers
{


    public class AuthController : BaseController
    {

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserCommand command)
        {
            var result = await Mediator.Send(command);
            return HandleResult(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] GetAllUsersQuery query)
        {
            var result = await Mediator.Send(query);

            return HandleResult(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var result = await Mediator.Send(new GetUserByIdQuery(id));
            return HandleResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUserCommand command)
        {
            var result = await Mediator.Send(command);
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await Mediator.Send(new DeleteUserCommand(id));
            return HandleResult(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            var result = await Mediator.Send(command);
            return HandleResult(result);
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

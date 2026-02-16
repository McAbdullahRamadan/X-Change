using Application.Features.Users.Commands.Register;
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
    }
}

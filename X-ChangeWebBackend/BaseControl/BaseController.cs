using Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace X_ChangeWebBackend.BaseControl
{

    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        private IMediator _mediatorInstance;

        protected IMediator Mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();

        public IActionResult HandleResult<T>(DataResponse<T> result)
        {
            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    DataResponse<string>.Failure(new[] { "An unexpected error occurred." }));

            return result.Status switch
            {
                ResultStatus.Success =>
                    Ok(result),

                ResultStatus.Created =>
                    Created(string.Empty, result),

                ResultStatus.BadRequest =>
                    BadRequest(result),

                ResultStatus.UnprocessableEntity =>
                    UnprocessableEntity(result),

                ResultStatus.Unauthorized =>
                    Unauthorized(result),

                ResultStatus.NotFound =>
                    NotFound(result),

                ResultStatus.Deleted =>
                    Ok(result),

                _ =>
                    StatusCode(StatusCodes.Status500InternalServerError, result)
            };
        }
    }
}

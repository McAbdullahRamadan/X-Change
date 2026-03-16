using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace X_ChangeWebBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetTodos()
        {
            var todos = new[]
            {
                new { Id = 1, Task = "Buy groceries", IsCompleted = false },
                new { Id = 2, Task = "Walk the dog", IsCompleted = true },
                new { Id = 3, Task = "Finish project", IsCompleted = false }
            };
            return Ok(todos);
        }
    }
}

namespace Application.DTOs
{
    public class AuthResultDto
    {
        public bool IsSuccess { get; private set; }
        public IEnumerable<string> Errors { get; private set; } = [];

        public static AuthResultDto Success()
            => new() { IsSuccess = true };

        public static AuthResultDto Failure(string error)
            => new() { IsSuccess = false, Errors = [error] };

        public static AuthResultDto Failure(IEnumerable<string> errors)
            => new() { IsSuccess = false, Errors = errors };
    }
}

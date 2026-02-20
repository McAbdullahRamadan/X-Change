namespace Application.DTOs.LoginUserDto
{
    public class AuthResultDto
    {
        public string Token { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string UserName { get; set; } = default!;

    }
}


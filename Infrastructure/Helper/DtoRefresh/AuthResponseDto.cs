namespace Infrastructure.Helper.DtoRefresh
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
    }
}

namespace Infrastructure.Helper.DtoRefresh
{
    public class JwtAuthResult
    {
        public string AccessToken { get; set; }
        public RefreshToken refreshToken { get; set; }

    }
    public class RefreshToken
    {
        public string UserName { get; set; }
        public string TokenSting { get; set; }

        public DateTime ExpireAt { get; set; }


    }
}

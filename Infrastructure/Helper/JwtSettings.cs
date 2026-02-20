namespace Infrastructure.Helper
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string audience { get; set; }
        public bool validateAudience { get; set; }
        public bool validateIssuer { get; set; }
        public bool validateLifetime { get; set; }
        public bool validateIssuerSigninKey { get; set; }
        public int AccessTokenExpireDate { get; set; }
        public int RefreshTokenExpireDate { get; set; }
    }
}

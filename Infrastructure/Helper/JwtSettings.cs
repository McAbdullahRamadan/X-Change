namespace Infrastructure.Helper
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }

        public bool ValidateAudience { get; set; }
        public bool ValidateIssuer { get; set; }
        public bool ValidateLifetime { get; set; }
        public bool ValidateIssuerSigninKey { get; set; }

        public int AccessTokenExpireDate { get; set; }
        public int RefreshTokenExpireDate { get; set; }
    }
}

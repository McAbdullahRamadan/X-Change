namespace Domain.Entities.System
{
    public class RefreshToken
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Token { get; set; } = string.Empty;

        public DateTime Expires { get; set; }

        public bool IsExpired => DateTime.UtcNow >= Expires;

        public bool IsRevoked { get; set; }

        public DateTime? RevokedAt { get; set; }

        public string? ReplacedByToken { get; set; }

        public string UserId { get; set; } = string.Empty;
    }
}

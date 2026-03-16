using Domain.Entities.System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Identity
{
    public class UserRefreshToken
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }

        public string? JwtId { get; set; }
        public bool IsUsed { get; set; }


        public bool IsRevoked { get; set; }
        public DateTime AddedTime { get; set; }
        public DateTime ExpiryDate { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(ApplicationUser.RefreshToken))]
        public virtual ApplicationUser? user { get; set; }
    }
}

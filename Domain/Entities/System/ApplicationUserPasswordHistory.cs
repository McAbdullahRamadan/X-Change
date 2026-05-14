using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.System
{
    public class ApplicationUserPasswordHistory : BaseAuditableEntity<Guid>
    {
        public string UserId { get; set; } = null!;
        [NotMapped]
        public ApplicationUser User { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}

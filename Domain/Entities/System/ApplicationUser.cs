using Domain.Entities.Business;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.System
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public Gender PersonGender { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string? NationalId { get; set; }
        public string? University { get; set; }
        public string? Major { get; set; }
        public string? RefreshToken { get; set; } = null!;
        public DateTime? RefreshTokenExpiration { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<ApplicationUserClaim> Claims { get; set; } = new List<ApplicationUserClaim>();
        public ICollection<ApplicationUserRole> UserRoles { get; set; } = new List<ApplicationUserRole>();
        public ICollection<ApplicationUserLogin> Logins { get; set; } = new List<ApplicationUserLogin>();
        public ICollection<ApplicationUserToken> Tokens { get; set; } = new List<ApplicationUserToken>();
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<ApplicationUserPasswordHistory> PasswordHistories { get; set; } = new List<ApplicationUserPasswordHistory>();

    }
    public enum Gender { Male, Female, Other }

}

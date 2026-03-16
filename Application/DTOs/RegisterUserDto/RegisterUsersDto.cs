using Domain.Entities.System;

namespace Application.DTOs.RegisterUserDto
{
    public class RegisterUsersDto
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string ConfirmEmail { get; set; } = null!;
        public Gender PersonGender { get; set; }

        public string Password { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }

        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;

        public string? NationalId { get; set; }

        public string? University { get; set; }
        public string? Major { get; set; }

    }
}

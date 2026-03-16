using Application.Responses;
using MediatR;

namespace Application.Features.Users.Commands.UpdateUser
{

    public class UpdateUserCommand : IRequest<DataResponse<string>>
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string University { get; set; }
        public string Major { get; set; }
        public string PersonGender { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}

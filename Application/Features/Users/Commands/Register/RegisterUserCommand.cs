using Application.Responses;
using Domain.Entities.System;
using MediatR;

namespace Application.Features.Users.Commands.Register
{
    public record RegisterUserCommand(
         string UserName,
         string Email,
         string ConfirmEmail,
         Gender PersonGender,
         string Password,
         string PhoneNumber,
         string FirstName,
         string LastName,
         DateTime DateOfBirth,
         string City,
         string Country,
         string? NationalId,
         string? University,
         string? Major) : IRequest<DataResponse<string>>
    { }
}

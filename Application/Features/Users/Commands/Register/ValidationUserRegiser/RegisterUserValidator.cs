using Application.Features.Users.Commands.Register;
using FluentValidation;

namespace Application.Features.Users.Commands.Register.ValidationUserRegiser
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Your UserName is Empty").MinimumLength(5).MaximumLength(25);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Your Email is Empty").EmailAddress().WithMessage("Email is Invalid");

            RuleFor(x => x.ConfirmEmail)
                .Equal(x => x.Email)
                .WithMessage("Email and Confirm Email must match");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Your Password is Empty")
                .MinimumLength(8).MaximumLength(16).Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                                                   .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter"); ;

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Your Phone Number is Empty");

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateTime.Now);

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("Your City is Empty");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Your Country is Empty");
        }
    }
}

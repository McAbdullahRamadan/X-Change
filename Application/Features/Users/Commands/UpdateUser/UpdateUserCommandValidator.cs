using FluentValidation;

namespace Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.UserName)
                   .NotEmpty().WithMessage("Your UserName is Empty").MinimumLength(5).MaximumLength(25);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Your Email is Empty").EmailAddress().WithMessage("Email is Invalid");

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

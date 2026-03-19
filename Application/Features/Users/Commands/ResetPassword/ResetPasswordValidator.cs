using FluentValidation;

namespace Application.Features.Users.Commands.ResetPassword
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
    {

        #region Construtors
        public ResetPasswordValidator()
        {



            ApplayValidationRule();


        }
        #endregion
        #region Actions
        public void ApplayValidationRule()
        {

            //Email Validation
            RuleFor(x => x.Email)
           .NotEmpty().WithMessage("Your Email Is Empty")
           .NotNull().WithMessage("Your Email Is Requierd")
           .EmailAddress().WithMessage("Your Email Is Invalid");

            //Password Validation
            RuleFor(x => x.Password)
           .NotEmpty().WithMessage("Your Password Is Empty")
           .NotNull().WithMessage("Your Password Is Null")
           .MinimumLength(8).WithMessage("Your Password Is MinimumLength 8")
           .MaximumLength(16).WithMessage("Your Password Is MaximumLength 16");
            //ConfirmPassword Validation
            RuleFor(x => x.ConfirmPassword)

      .Equal(x => x.Password).WithMessage("Password Not Equal Confirm Password");

        }

        #endregion
    }
}

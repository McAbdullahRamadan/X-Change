using Application.Features.Users.Query.ConfirmResetPassword;
using FluentValidation;

namespace Application.Features.Users.Query.Validation
{
    public class ResetPasswordValidator : AbstractValidator<ConfirmResetPasswordQuery>
    {





        public ResetPasswordValidator()
        {



            ApplayValidationRule();


        }

        public void ApplayValidationRule()
        {


            RuleFor(x => x.Code)
                 .NotEmpty().WithMessage("User Code Is Empty")
                 .NotNull().WithMessage("User Code Is Null");

            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage("User Email Is Empty")
                 .NotNull().WithMessage("User Email Is Null");


        }

    }
}
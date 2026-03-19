using Application.Features.Users.Query.ConfirmEmail;
using FluentValidation;

namespace Application.Features.Users.Query.Validation
{
    public class ConfirmEmailQueryValidator : AbstractValidator<ConfirmEmailQuery>
    {


        #region Constructors
        public ConfirmEmailQueryValidator()
        {



            ApplayValidationRule();


        }
        #endregion
        #region Actions
        public void ApplayValidationRule()
        {
            RuleFor(x => x.userId)
                .NotEmpty().WithMessage("User Id Is Empty")
                .NotNull().WithMessage("User Id Is Null");

            RuleFor(x => x.code)
                 .NotEmpty().WithMessage("User Code Is Empty")
                 .NotNull().WithMessage("User Code is Null");

        }

        #endregion
    }
}

using FluentValidation;

namespace Application.Features.Users.Commands.SendResetPassword
{
    public class SendResetPasswordValidator : AbstractValidator<SendResetPasswordCommand>
    {
        #region Fields



        #endregion
        #region Construtors
        public SendResetPasswordValidator()
        {



            ApplayValidationRule();


        }
        #endregion
        #region Actions
        public void ApplayValidationRule()
        {

            //Email Validation
            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email Is Empty")
            .NotNull().WithMessage("Email is null");




        }


        #endregion
    }
}

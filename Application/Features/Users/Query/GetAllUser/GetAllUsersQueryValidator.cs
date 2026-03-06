using FluentValidation;

namespace Application.Features.Users.Query.GetAllUser
{
    public class GetAllUsersQueryValidator : AbstractValidator<GetAllUsersQuery>
    {
        public GetAllUsersQueryValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0);


        }
    }
}

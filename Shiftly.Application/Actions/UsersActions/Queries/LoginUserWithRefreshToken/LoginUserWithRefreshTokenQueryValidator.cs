using FluentValidation;
using Shiftly.Application.Actions.UsersActions.Queries.LoginUserWithRefreshToken;

namespace Shiftly.Application.Actions.UsersActions.Queries.LoginUserWithRefreshToken;

public class LoginUserWithRefreshTokenQueryValidator : AbstractValidator<LoginUserWithRefreshTokenQuery>
{
    public LoginUserWithRefreshTokenQueryValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .WithMessage("Refresh token is required.");
    }
} 
using FluentValidation;
using Shiftly.Application.Actions.AuthActions.Queries.LoginAccountWithRefreshToken;

namespace Shiftly.Application.Actions.AuthActions.Queries.LoginAccountWithRefreshToken;

public class LoginAccountWithRefreshTokenQueryValidator : AbstractValidator<LoginAccountWithRefreshTokenQuery>
{
    public LoginAccountWithRefreshTokenQueryValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .WithMessage("Refresh token is required.");
    }
}

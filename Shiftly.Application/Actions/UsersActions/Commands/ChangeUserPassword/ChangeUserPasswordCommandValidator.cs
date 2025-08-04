using FluentValidation;

namespace Shiftly.Application.Actions.UsersActions.Commands.ChangeUserPassword;

public class ChangeUserPasswordCommandValidator : AbstractValidator<ChangeUserPasswordCommand>
{
    public ChangeUserPasswordCommandValidator()
    {
        RuleFor(x => x.UserEmail)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Valid email address is required.");

        RuleFor(x => x.OldPassword)
            .NotEmpty()
            .WithMessage("Old password is required.");

        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .MinimumLength(6)
            .WithMessage("New password must be at least 6 characters long.");

        RuleFor(x => x.NewPassword)
            .NotEqual(x => x.OldPassword)
            .WithMessage("New password must be different from the old password.");
    }
} 
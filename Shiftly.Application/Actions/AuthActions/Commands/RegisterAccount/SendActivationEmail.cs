namespace Shiftly.Application.Actions.AuthActions.Commands.RegisterAccount;

public record SendActivationEmail
{
    public Guid UserId { get; init; }
    public string Email { get; init; } = default!;
    public string FirstName { get; init; } = default!;
};

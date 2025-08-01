namespace Shiftly.Application.Actions.UsersActions.Commands.RegisterUser;

public record SendActivationEmail
{
    public Guid UserId { get; init; }
    public string Email { get; init; } = default!;
};
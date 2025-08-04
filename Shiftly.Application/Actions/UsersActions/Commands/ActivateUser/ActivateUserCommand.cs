using Shiftly.Application.Common.Abstraction.Messaging;

namespace Shiftly.Application.Actions.UsersActions.Commands.ActivateUser;

public class ActivateUserCommand : ICommand
{
    public Guid UserId { get; set; }
    public string ActivationToken { get; set; } = string.Empty;
}
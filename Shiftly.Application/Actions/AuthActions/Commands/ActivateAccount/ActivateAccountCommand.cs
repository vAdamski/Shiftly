using Shiftly.Application.Common.Abstraction.Messaging;

namespace Shiftly.Application.Actions.AuthActions.Commands.ActivateAccount;

public class ActivateAccountCommand : ICommand
{
    public Guid UserId { get; set; }
    public string ActivationToken { get; set; } = string.Empty;
}

using Shiftly.Application.Common.Abstraction.Messaging;

namespace Shiftly.Application.Actions.AuthActions.Commands.ChangeAccountPassword;

public class ChangeAccountPasswordCommand : ICommand<Guid>
{
    public string UserEmail { get; set; } = string.Empty;
    public string OldPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}

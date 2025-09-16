using Shiftly.Application.Actions.AuthActions.Commands.RegisterAccount;

namespace Shiftly.Application.Common.Interfaces.Application.Handlers;

public interface ISendActivationEmailHandler
{
    Task HandleAsync(SendActivationEmail message, CancellationToken cancellationToken);
}
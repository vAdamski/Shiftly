using Shiftly.Application.Actions.UsersActions.Commands.RegisterUser;

namespace Shiftly.Application.Common.Interfaces.Application.Handlers;

public interface ISendActivationEmailHandler
{
    Task HandleAsync(SendActivationEmail message, CancellationToken cancellationToken);
}
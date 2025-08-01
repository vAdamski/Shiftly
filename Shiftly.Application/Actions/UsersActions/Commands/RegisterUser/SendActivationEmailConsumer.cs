using MassTransit;
using Shiftly.Application.Common.Interfaces.Application.Handlers;

namespace Shiftly.Application.Actions.UsersActions.Commands.RegisterUser;

public class SendActivationEmailConsumer(ISendActivationEmailHandler handler)
    : IConsumer<SendActivationEmail>
{
    public async Task Consume(ConsumeContext<SendActivationEmail> context)
    {
        await handler.HandleAsync(context.Message, context.CancellationToken);
    }
}
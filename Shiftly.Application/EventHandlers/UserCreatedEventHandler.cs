using Microsoft.Extensions.DependencyInjection;
using JasperFx.Events.Daemon;
using JasperFx.Events.Projections;
using Marten;
using Marten.Subscriptions;
using Shiftly.Application.Actions.AuthActions.Commands.RegisterAccount;
using Shiftly.Application.Common.Interfaces.Application.Handlers;
using Shiftly.Domain.Events.User;

namespace Shiftly.Application.EventHandlers;

public class UserCreatedEventHandler : SubscriptionBase
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public UserCreatedEventHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;

		Name = "UserCreated";
		IncludeType<UserCreated>();
		Options.BatchSize = 10;
		Options.MaximumHopperSize = 1000;
		Options.SubscribeFromPresent();
    }

    public override async Task<IChangeListener> ProcessEventsAsync(EventRange page, ISubscriptionController controller, IDocumentOperations operations,
        CancellationToken cancellationToken)
    {
        foreach (var @event in page.Events)
        {
            if (@event.Data is UserCreated userCreatedEvent)
            {
                Handle(userCreatedEvent, cancellationToken).GetAwaiter().GetResult();
            }
        }

        return NullChangeListener.Instance;
    }
    
    private async Task Handle(UserCreated @event, CancellationToken cancellationToken)
    {
        var sendActivationEmail = new SendActivationEmail
        {
            UserId = @event.Id,
            Email = @event.Email,
            FirstName = @event.FirstName
        };

		using var scope = _serviceScopeFactory.CreateScope();
		var handler = scope.ServiceProvider.GetRequiredService<ISendActivationEmailHandler>();
		await handler.HandleAsync(sendActivationEmail, cancellationToken);
    }
}

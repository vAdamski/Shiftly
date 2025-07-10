using System.Text;
using Shiftly.Application.Common.Interfaces.Application.Services;
using Shiftly.Application.Common.Interfaces.Infrastructure.Services;
using Shiftly.Application.Providers;
using Shiftly.Domain.Common;

namespace Shiftly.Application.Services;

public class EventPublisher(IMessagePublisher messagePublisher) : IEventPublisher
{
    public async Task PublishAsync(EventBase @event, CancellationToken cancellationToken = default)
    {
        var queueName = QueueEventNameProvider.GetQueueName(@event);
        
        var serializedEvent = @event.Serialize();
        var body = Encoding.UTF8.GetBytes(serializedEvent);

        await messagePublisher.PublishAsync(body, queueName ,cancellationToken);
    }
}
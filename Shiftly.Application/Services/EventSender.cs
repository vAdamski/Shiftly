using System.Text;
using Shiftly.Application.Common.Interfaces.Application.Services;
using Shiftly.Application.Common.Interfaces.Infrastructure.Services;
using Shiftly.Application.Providers;
using Shiftly.Domain.Events.Common;

namespace Shiftly.Application.Services;

public class EventSender(IMessagePublisher messagePublisher) : IEventSender
{
    public async Task PublishAsync(EventQueue @event, CancellationToken cancellationToken = default)
    {
        var queueName = QueueEventNameProvider.GetQueueName(@event);
        
        var serializedEvent = @event.Serialize();
        var body = Encoding.UTF8.GetBytes(serializedEvent);
    
        await messagePublisher.PublishAsync(body, queueName ,cancellationToken);
    }
}
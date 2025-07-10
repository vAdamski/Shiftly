using Shiftly.Domain.Common;

namespace Shiftly.Application.Common.Interfaces.Application.Services;

public interface IEventPublisher
{
    Task PublishAsync(EventBase @event, CancellationToken cancellationToken = default);
}
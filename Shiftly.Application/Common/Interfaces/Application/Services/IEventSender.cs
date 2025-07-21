using Shiftly.Domain.Common;
using Shiftly.Domain.Events.Common;

namespace Shiftly.Application.Common.Interfaces.Application.Services;

public interface IEventSender
{
    Task PublishAsync(EventQueue @event, CancellationToken cancellationToken = default);
}
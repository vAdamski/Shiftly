using Shiftly.Domain.Common;

namespace Shiftly.Application.Common.Interfaces.Infrastructure.Services;

public interface IMessagePublisher
{
    Task PublishAsync(byte[] body, string queueName, CancellationToken cancellationToken = default);
}
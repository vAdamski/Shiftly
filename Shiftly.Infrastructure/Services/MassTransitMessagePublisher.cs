using MassTransit;
using Shiftly.Application.Common.Interfaces.Infrastructure.Services;
using Shiftly.Domain.Common;

namespace Shiftly.Infrastructure.Services;

public class MassTransitMessagePublisher(IPublishEndpoint publishEndpoint) : IMessagePublisher
{
    public async Task PublishAsync(byte[] body, string queueName, CancellationToken cancellationToken = default)
    {
        // Assuming you have a message contract that accepts a byte[] payload
        var message = new BytePayloadMessage
        {
            Payload = body
        };

        await publishEndpoint.Publish(message, cancellationToken);
    }
}
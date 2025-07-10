using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using Shiftly.Application.Common.Interfaces.Infrastructure.Services;


namespace Shiftly.Infrastructure.Services;

public class RabbitMqMessagePublisher(IConfiguration configuration) : IMessagePublisher
{
    private readonly string _hostName = configuration.GetValue<string>("RabbitMQ:Host") ??
                                        throw new ArgumentNullException(nameof(RabbitMqMessagePublisher));

    private readonly int _port = configuration.GetValue<int>("RabbitMq:Port");

    public async Task PublishAsync(byte[] body, string queueName, CancellationToken cancellationToken = default)
    {
        var factory = new ConnectionFactory()
        {
            HostName = _hostName,
            Port = _port,
        };

        using var connection = await factory.CreateConnectionAsync(cancellationToken);
        using var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);
        
        var queueDeclareOk = await channel.QueueDeclareAsync(
            queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: cancellationToken);

        await channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: queueName,
            body: body,
            cancellationToken: cancellationToken);
    }
}
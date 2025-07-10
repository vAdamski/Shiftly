using Shiftly.Domain.Events.User;

namespace Shiftly.Application.Providers;

public static class QueueEventNameProvider
{
    public static string GetQueueName(object @event)
    {
        return @event switch
        {
            UserRegistered => "email.sender.queue",
            _ => throw new ArgumentException($"No queue mapping defined for {@event.GetType().Name}")
        };
    }
}
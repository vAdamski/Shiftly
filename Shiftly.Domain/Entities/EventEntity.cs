using Newtonsoft.Json;
using Shiftly.Domain.Common;

namespace Shiftly.Domain.Entities;

public class EventEntity
{
    public Guid Id { get; private set; }
    public Guid StreamId { get; private set; }
    public string Type { get; private set; }
    public int Version { get; private set; }
    public string Data { get; private set; }
    public DateTime TimestampUtc { get; private set; } = DateTime.UtcNow;
    
    
    
    private EventEntity()
    {
    }

    public static EventEntity Create<T>(T @event) where T : EventBase
    {
        if (@event == null)
        {
            throw new ArgumentNullException(nameof(@event), "Event object cannot be null.");
        }

        return new EventEntity
        {
            Id = Guid.NewGuid(),
            StreamId = @event.StreamId,
            Type = @event.GetType().Name,
            Version = @event.Version,
            Data = JsonConvert.SerializeObject(@event),
            TimestampUtc = DateTime.UtcNow
        };
    }
}
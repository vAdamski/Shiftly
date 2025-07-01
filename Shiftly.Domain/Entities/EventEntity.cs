using System.Text.Json;
using Shiftly.Domain.Common;

namespace Shiftly.Domain.Entities;

public class EventEntity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid StreamId { get; private set; } = default!;
    public string Type { get; private set; } = default!;
    public int Version { get; private set; }
    public string Data { get; private set; } = default!;
    public DateTime TimestampUtc { get; private set; } = DateTime.UtcNow;
    
    public EventEntity()
    {
    }
    
    public static EventEntity Create(Guid streamId, IDomainEvent domainEvent)
    {
        return new EventEntity
        {
            StreamId = streamId,
            Type = domainEvent.GetType().Name,
            Version = domainEvent.Version,
            Data = JsonSerializer.Serialize(domainEvent),
        };
    }
}
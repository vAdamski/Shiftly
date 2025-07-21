namespace Shiftly.Domain.Events.Common;

public abstract class Event
{
    public abstract Guid StreamId { get; }
}
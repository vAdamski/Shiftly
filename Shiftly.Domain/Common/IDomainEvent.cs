namespace Shiftly.Domain.Common;

public interface IDomainEvent
{
    Guid StreamId { get; }
    int Version { get; }
}
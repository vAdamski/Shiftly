using Shiftly.Domain.Events.Common;

namespace Shiftly.Domain.Events.User;

public abstract record UserEvent(Guid StreamId) : Event(StreamId);
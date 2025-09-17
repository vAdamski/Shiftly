using Shiftly.Domain.Events.Common;

namespace Shiftly.Domain.Events.RefreshToken;

public abstract record RefreshTokenEvent(Guid StreamId) : Event(StreamId);
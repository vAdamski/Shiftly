using Shiftly.Domain.Events.Common;

namespace Shiftly.Domain.Events.User;

public interface IUserEvent : IEvent
{
    Guid UserId { get; }
}
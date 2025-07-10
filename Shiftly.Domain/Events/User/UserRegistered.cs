using Shiftly.Domain.Common;

namespace Shiftly.Domain.Events.User;

public class UserRegistered(Guid userId, string firstName, string email) : EventBase
{
    public Guid UserId { get; private set; } = userId;
    public string FirstName { get; private set; } = firstName;
    public string Email { get; private set; } = email;
    public override Guid StreamId => UserId;
    public override int Version => 1;
}
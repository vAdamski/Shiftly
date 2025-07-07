using Shiftly.Domain.Common;

namespace Shiftly.Domain.Events.User;

public class UserPasswordChanged : EventBase
{
    public Guid UserId { get; set; }
    public string PasswordHash { get; set; } = string.Empty;

    public override Guid StreamId => UserId;
    public override int Version => 1;

    public UserPasswordChanged(Guid userId, string passwordHash)
    {
        UserId = userId;
        PasswordHash = passwordHash;
    }
}
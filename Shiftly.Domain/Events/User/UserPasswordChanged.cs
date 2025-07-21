using Shiftly.Domain.Common;
using Shiftly.Domain.Events.Common;

namespace Shiftly.Domain.Events.User;

public class UserPasswordChanged : Event
{
    public Guid UserId { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public override Guid StreamId => UserId;
}
using Shiftly.Domain.Events.Common;

namespace Shiftly.Domain.Events.User;

public class UserCreated : Event
{
    public Guid UserId { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public override Guid StreamId => UserId;
}
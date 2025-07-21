using Shiftly.Domain.Common;

namespace Shiftly.Domain.Events.User;

public class UserPasswordChanged : IUserEvent
{
    public Guid UserId { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
}
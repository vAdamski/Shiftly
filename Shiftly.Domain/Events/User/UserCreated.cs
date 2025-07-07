using System.Text.Json.Serialization;
using Shiftly.Domain.Common;

namespace Shiftly.Domain.Events.User;

public class UserCreated(string firstName, string lastName, string email, string passwordHash)
    : EventBase
{
    public Guid UserId { get; private set; } = Guid.NewGuid();
    public string FirstName { get; private set; } = firstName;
    public string LastName { get; private set; } = lastName;
    public string Email { get; private set; } = email;
    public string PasswordHash { get; private set; } = passwordHash;
    public override Guid StreamId => UserId;
    public override int Version => 1;
}
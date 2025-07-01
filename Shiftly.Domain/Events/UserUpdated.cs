using Shiftly.Domain.Common;
using Shiftly.Domain.Entities;

namespace Shiftly.Domain.Events;

public class UserUpdated : IDomainEvent
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    public Guid StreamId => UserId;
    public int Version => 1;

    public UserUpdated(Guid userId, string firstName, string lastName, string email, string passwordHash)
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
    }
}
using Shiftly.Domain.Common;
using Shiftly.Domain.Events.User;

namespace Shiftly.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public bool IsActive { get; set; } = false;
    public string? ActivationToken { get; set; }
    public DateTime? ActivationTokenExpiration { get; set; }


    public User()
    {

    }

    public void Apply(UserCreated userCreated)
    {
        if (userCreated == null)
        {
            throw new ArgumentNullException(nameof(userCreated), "UserCreated event cannot be null.");
        }

        Id = userCreated.UserId;
        FirstName = userCreated.FirstName;
        LastName = userCreated.LastName;
        Email = userCreated.Email;
        PasswordHash = userCreated.PasswordHash;
    }

    public void Apply(UserPasswordChanged userPasswordChanged)
    {
        if (userPasswordChanged == null)
        {
            throw new ArgumentNullException(nameof(userPasswordChanged), "UserPasswordChanged event cannot be null.");
        }

        PasswordHash = userPasswordChanged.PasswordHash;
    }
}
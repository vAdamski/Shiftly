using Marten.Events.Aggregation;
using Shiftly.Domain.Entities;
using Shiftly.Domain.Events.User;

namespace Shiftly.Domain.Projections;

public class UserProjection : SingleStreamProjection<User, Guid>
{
    public void Apply(UserCreated userCreated, User user)
    {
        if (userCreated == null)
        {
            throw new ArgumentNullException(nameof(userCreated), "UserCreated event cannot be null.");
        }

        user.Id = userCreated.Id;
        user.FirstName = userCreated.FirstName;
        user.LastName = userCreated.LastName;
        user.Email = userCreated.Email;
        user.PasswordHash = userCreated.PasswordHash;
    }

    public void Apply(UserPasswordChanged userPasswordChanged, User user)
    {
        if (userPasswordChanged == null)
        {
            throw new ArgumentNullException(nameof(userPasswordChanged), "UserPasswordChanged event cannot be null.");
        }

        user.PasswordHash = userPasswordChanged.PasswordHash;
    }
    
    public void Apply(UserActivationTokenGenerated userActivationTokenGenerated, User user)
    {
        if (userActivationTokenGenerated == null)
        {
            throw new ArgumentNullException(nameof(userActivationTokenGenerated), "UserActivationTokenGenerated event cannot be null.");
        }

        user.ActivationToken = userActivationTokenGenerated.ActivationToken.ToString();
        user.ActivationTokenExpiration = userActivationTokenGenerated.ExpiresInUtc;
    }
    
    public void Apply(UserActivated userActivated, User user)
    {
        if (userActivated == null)
        {
            throw new ArgumentNullException(nameof(userActivated), "UserActivationTokenUsed event cannot be null.");
        }
        
        user.IsActive = true;
    }
}
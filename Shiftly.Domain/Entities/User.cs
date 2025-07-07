using Shiftly.Domain.Common;
using Shiftly.Domain.Events.User;

namespace Shiftly.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    
    private List<EventEntity> _events = new();
    public IReadOnlyCollection<EventEntity> Events => _events;


    public User()
    {
		
    }

    public void Apply(EventBase @event)
    {
        if (@event == null)
        {
            throw new ArgumentNullException(nameof(@event), "Event cannot be null.");
        }

        switch (@event)
        {
            case UserCreated userCreated:
                Apply(userCreated);
                break;
            case UserPasswordChanged userPasswordChanged:
                Apply(userPasswordChanged);
                break;
            default:
                throw new InvalidOperationException($"Unknown event type: {@event.GetType().Name}");
        }
        
        _events.Add(EventEntity.Create(@event));
    }

    private void Apply(UserCreated userCreated)
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

    private void Apply(UserPasswordChanged userPasswordChanged)
    {
        if (userPasswordChanged == null)
        {
            throw new ArgumentNullException(nameof(userPasswordChanged), "UserPasswordChanged event cannot be null.");
        }

        PasswordHash = userPasswordChanged.PasswordHash;
    }
}
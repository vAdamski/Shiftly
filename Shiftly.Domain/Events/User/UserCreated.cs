namespace Shiftly.Domain.Events.User;

public record UserCreated(Guid Id, string FirstName, string LastName, string Email, string PasswordHash) : UserEvent(Id);
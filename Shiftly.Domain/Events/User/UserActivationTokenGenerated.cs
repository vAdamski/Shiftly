namespace Shiftly.Domain.Events.User;

public record UserActivationTokenGenerated(Guid UserId, Guid ActivationToken, DateTime ExpiresInUtc) : UserEvent(UserId);
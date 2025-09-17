namespace Shiftly.Domain.Events.User;

public record UserActivated(Guid UserId, DateTime ActivatedAtUtc) : UserEvent(UserId);
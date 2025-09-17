namespace Shiftly.Domain.Events.User;

public record ActivationEmailSent(Guid UserId, string Email, DateTime SentAt) : UserEvent(UserId);
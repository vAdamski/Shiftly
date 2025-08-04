namespace Shiftly.Domain.Events.User;

public class ActivationEmailSent(Guid userId, string email, DateTime sentAt) : UserEvent
{
    public string Email { get; init; } = email;
    public DateTime SentAt { get; init; } = sentAt;
    public override Guid StreamId => userId;
}
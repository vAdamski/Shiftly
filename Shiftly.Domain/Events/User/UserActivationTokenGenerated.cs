namespace Shiftly.Domain.Events.User;

public class UserActivationTokenGenerated(Guid userId, DateTime expirationTimeInUtc) : UserEvent
{
    public Guid UserId { get; set; } = userId;
    public Guid ActivationToken { get; set; } = Guid.NewGuid();
    public DateTime ExpiresInUtc { get; set; } = expirationTimeInUtc;
    public override Guid StreamId => UserId;
}
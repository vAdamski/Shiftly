namespace Shiftly.Domain.Events.User;

public class UserActivated(Guid userId, DateTime dateTimeUtc) : UserEvent
{
    public Guid UserId { get; set; } = userId;
    public DateTime ActivatedAtUtc { get; set; } = DateTime.UtcNow;
    
    public override Guid StreamId => UserId;
}
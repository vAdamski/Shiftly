namespace Shiftly.Domain.Events.User;

public class UserActivated(Guid userId, DateTime dateTimeUtc) : UserEvent
{
    public Guid UserId { get; set; } = userId;
    public DateTime ActivatedAtUtc { get; set; } = dateTimeUtc;
    
    public override Guid StreamId => UserId;
}
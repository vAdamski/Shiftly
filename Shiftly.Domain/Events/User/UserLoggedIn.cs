namespace Shiftly.Domain.Events.User;

public class UserLoggedIn(Guid userId, string token, string refreshToken) : UserEvent
{
    public Guid UserId { get; } = userId;
    public string Token { get; } = token;
    public string RefreshToken { get; } = refreshToken;
    public DateTime LoggedInAt { get; } = DateTime.UtcNow;
    
    public override Guid StreamId => UserId;
}
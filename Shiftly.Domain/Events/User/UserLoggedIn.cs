namespace Shiftly.Domain.Events.User;

public record UserLoggedIn(Guid UserId, string Token, string RefreshToken, DateTime LoggedInAt) : UserEvent(UserId);
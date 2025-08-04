namespace Shiftly.Application.Providers.RefreshToken;

public record RefreshTokenDto(Guid UserId, string Token, DateTime ExpiresAtInUtc);
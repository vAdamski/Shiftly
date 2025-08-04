using System.Security.Cryptography;

namespace Shiftly.Domain.Events.RefreshToken;

public class RefreshTokenCreated(Guid userId, string refreshToken, DateTime expiresAtInUtc) : RefreshTokenEvent
{
    public Guid Id { get; } = Guid.CreateVersion7();
    public Guid UserId { get; } = userId;
    public string Token { get; } = refreshToken;
    public DateTime ExpiresAtInUtc { get; } = expiresAtInUtc;

    public override Guid StreamId => Id;
}
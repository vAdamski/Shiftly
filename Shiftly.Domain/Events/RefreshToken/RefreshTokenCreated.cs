using System.Security.Cryptography;

namespace Shiftly.Domain.Events.RefreshToken;

public record RefreshTokenCreated(Guid Id, Guid UserId, string Token, DateTime ExpiresAtInUtc) : RefreshTokenEvent(Id);
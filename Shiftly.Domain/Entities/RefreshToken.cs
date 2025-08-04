using System.Security.Cryptography;
using Shiftly.Domain.Events.RefreshToken;

namespace Shiftly.Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Token { get; set; }
    public DateTime ExpiresTime { get; set; }
    
    public void Apply(RefreshTokenCreated refreshTokenCreated)
    {
        if (refreshTokenCreated == null)
        {
            throw new ArgumentNullException(nameof(refreshTokenCreated), "RefreshTokenCreated event cannot be null.");
        }

        Id = refreshTokenCreated.Id;
        UserId = refreshTokenCreated.UserId;
        Token = refreshTokenCreated.Token;
        ExpiresTime = refreshTokenCreated.ExpiresAtInUtc;
    }
}
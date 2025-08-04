using Marten.Events.Aggregation;
using Shiftly.Domain.Entities;
using Shiftly.Domain.Events.RefreshToken;

namespace Shiftly.Domain.Projections;

public class RefreshTokenProjection : SingleStreamProjection<RefreshToken, Guid>
{
    public void Apply(RefreshTokenCreated refreshTokenCreated, RefreshToken refreshToken)
    {
        if (refreshTokenCreated == null)
        {
            throw new ArgumentNullException(nameof(refreshTokenCreated), "RefreshTokenCreated event cannot be null.");
        }

        refreshToken.Id = refreshTokenCreated.Id;
        refreshToken.UserId = refreshTokenCreated.UserId;
        refreshToken.Token = refreshTokenCreated.Token;
        refreshToken.ExpiresTime = refreshTokenCreated.ExpiresAtInUtc;
    }
}
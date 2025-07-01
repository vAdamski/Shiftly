using Shiftly.Domain.Entities;

namespace Shiftly.Application.Common.Interfaces.Persistence.Repositories;

public interface IRefreshTokenRepository
{
	Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);
}
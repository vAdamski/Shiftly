using Shiftly.Domain.Entities;
using Shiftly.Domain.Events.RefreshToken;

namespace Shiftly.Application.Common.Interfaces.Persistence.Repositories;

public interface IRefreshTokenRepository
{
	Task AddAsync(RefreshTokenEvent @event, CancellationToken cancellationToken = default);
	Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);
	Task<RefreshToken?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
	Task UpdateAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);
	Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
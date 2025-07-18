using Shiftly.Application.Common.Interfaces.Persistence;
using Shiftly.Application.Common.Interfaces.Persistence.Repositories;
using Shiftly.Domain.Entities;

namespace Shiftly.Persistence.Repositories;

public class RefreshTokenRepository() : IRefreshTokenRepository
{
	public async Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException("This method is not implemented yet.");
	}
}
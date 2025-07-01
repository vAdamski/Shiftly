using Shiftly.Application.Common.Interfaces.Persistence;
using Shiftly.Application.Common.Interfaces.Persistence.Repositories;
using Shiftly.Domain.Entities;

namespace Shiftly.Persistence.Repositories;

public class RefreshTokenRepository(IAppDbContext ctx) : IRefreshTokenRepository
{
	public async Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
	{
		await ctx.RefreshTokens.AddAsync(refreshToken, cancellationToken);
		await ctx.SaveChangesAsync(cancellationToken);
	}
}
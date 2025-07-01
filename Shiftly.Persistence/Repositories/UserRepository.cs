using Microsoft.EntityFrameworkCore;
using Shiftly.Application.Common.Interfaces.Persistence;
using Shiftly.Application.Common.Interfaces.Persistence.Repositories;
using Shiftly.Domain.Entities;

namespace Shiftly.Persistence.Repositories;

public class UserRepository(IAppDbContext ctx) : IUserRepository
{
	public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
	{
		return await ctx.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
	}

	public async Task AddAsync(User user, CancellationToken cancellationToken = default)
	{
		await ctx.Users.AddAsync(user, cancellationToken);
		await ctx.SaveChangesAsync(cancellationToken);
	}

	public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		return ctx.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
	}
}
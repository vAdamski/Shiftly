using Microsoft.EntityFrameworkCore;
using Shiftly.Application.Common.Interfaces.Persistence;
using Shiftly.Application.Common.Interfaces.Persistence.Repositories;
using Shiftly.Domain.Common;
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

	public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		return await ctx.Users
			.AsNoTracking()
			.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
	}

	public async Task AppendEventAsync(EventBase @event, CancellationToken cancellationToken)
	{
		if (@event == null)
		{
			throw new ArgumentNullException(nameof(@event), "Event cannot be null.");
		}

		var user = await ctx.Users
			.Include(x => x.Events)
			.FirstOrDefaultAsync(x => x.Id == @event.StreamId, cancellationToken);
		
		if (user == null)
		{
			user = new();
			
			user.Apply(@event);
			ctx.Users.Add(user);
		}
		else
		{
			user.Apply(@event);
			ctx.Users.Update(user);
		}
		
		await ctx.SaveChangesAsync(cancellationToken);
	}
}
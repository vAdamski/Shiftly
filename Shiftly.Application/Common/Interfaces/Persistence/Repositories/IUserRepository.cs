using Shiftly.Domain.Common;
using Shiftly.Domain.Entities;

namespace Shiftly.Application.Common.Interfaces.Persistence.Repositories;

public interface IUserRepository
{
	Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
	Task AddAsync(User user, CancellationToken cancellationToken = default);
	Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
	Task AppendEventAsync(EventBase @event, CancellationToken cancellationToken);
}
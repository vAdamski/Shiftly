using Shiftly.Domain.Entities;
using Shiftly.Domain.Events.User;

namespace Shiftly.Application.Common.Interfaces.Persistence.Repositories;

public interface IUserRepository
{
	Task<bool> IsExistsAsync(Guid id, CancellationToken cancellationToken = default);
	Task<bool> IsExistsAsync(string email, CancellationToken cancellationToken = default);
	Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
	Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
	Task AddUserEventAsync(IUserEvent userEvent, CancellationToken cancellationToken = default);
}
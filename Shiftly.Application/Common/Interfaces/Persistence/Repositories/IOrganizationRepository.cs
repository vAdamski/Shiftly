using Shiftly.Domain.Entities;
using Shiftly.Domain.Events.Organization;

namespace Shiftly.Application.Common.Interfaces.Persistence.Repositories;

public interface IOrganizationRepository
{
	Task<bool> IsExistsAsync(Guid id, CancellationToken cancellationToken = default);
	Task<Organization?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
	Task AddOrganizationEventAsync(OrganizationEvent @event, CancellationToken cancellationToken = default);
}



using Marten;
using Shiftly.Application.Common.Interfaces.Persistence.Repositories;
using Shiftly.Domain.Entities;
using Shiftly.Domain.Events.Organization;

namespace Shiftly.Persistence.Repositories;

public class OrganizationRepository(IQuerySession querySession, IDocumentStore documentStore) : IOrganizationRepository
{
	public async Task<bool> IsExistsAsync(Guid id, CancellationToken cancellationToken = default)
	{
		var organization = await querySession.Query<Organization>().Where(x => x.Id == id)
			.SingleOrDefaultAsync(cancellationToken);

		return organization is not null;
	}

	public Task<Organization?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
	{
		return querySession.Query<Organization>().Where(x => x.Id == id)
			.SingleOrDefaultAsync(cancellationToken);
	}

	public async Task AddOrganizationEventAsync(OrganizationEvent @event, CancellationToken cancellationToken = default)
	{
		await using var session = documentStore.LightweightSession();

		if (!await IsExistsAsync(@event.StreamId, cancellationToken))
		{
			session.Events.StartStream<Organization>(@event.StreamId, @event);
		}
		else
		{
			session.Events.Append(@event.StreamId, @event);
		}

		await session.SaveChangesAsync(cancellationToken);
	}
}



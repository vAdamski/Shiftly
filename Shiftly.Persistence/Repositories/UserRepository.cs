using Marten;
using Shiftly.Application.Common.Interfaces.Persistence.Repositories;
using Shiftly.Domain.Common;
using Shiftly.Domain.Entities;
using Shiftly.Domain.Events.Common;
using Shiftly.Domain.Events.User;

namespace Shiftly.Persistence.Repositories;

public class UserRepository(IQuerySession querySession, IDocumentStore documentStore) : IUserRepository
{
    public async Task<bool> IsExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await querySession.Query<User>().Where(x => x.Id == id)
            .SingleOrDefaultAsync(cancellationToken);

        return user is not null;
    }

    public async Task<bool> IsExistsAsync(string email, CancellationToken cancellationToken = default)
    {
        var user = await querySession.Query<User>().Where(x => x.Email == email)
            .SingleOrDefaultAsync(cancellationToken);

        return user is not null;
    }

    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return querySession.Query<User>().Where(x => x.Email == email)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return querySession.Query<User>().Where(x => x.Id == id)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task AddUserEventAsync(UserEvent @event, CancellationToken cancellationToken = default)
    {
        await using var session = documentStore.LightweightSession();
        
        if (!await IsExistsAsync(@event.StreamId, cancellationToken))
        {
            session.Events.StartStream<User>(@event.StreamId, @event);
        }
        else
        {
            session.Events.Append(@event.StreamId, @event);
        }

        await session.SaveChangesAsync(cancellationToken);
    }
}
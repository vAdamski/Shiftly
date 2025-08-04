using Marten;
using Shiftly.Application.Common.Interfaces.Persistence.Repositories;
using Shiftly.Domain.Entities;
using Shiftly.Domain.Events.RefreshToken;

namespace Shiftly.Persistence.Repositories;

public class RefreshTokenRepository(IQuerySession querySession, IDocumentStore documentStore) : IRefreshTokenRepository
{
    public async Task AddAsync(RefreshTokenEvent @event, CancellationToken cancellationToken = default)
    {
        await using var session = documentStore.LightweightSession();
        
        // Check if refresh token stream already exists
        var existingToken = await querySession.Query<RefreshToken>()
            .Where(x => x.Token == GetTokenFromEvent(@event))
            .SingleOrDefaultAsync(cancellationToken);
            
        if (existingToken is null)
        {
            session.Events.StartStream<RefreshToken>(@event.StreamId, @event);
        }
        else
        {
            session.Events.Append(@event.StreamId, @event);
        }

        await session.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        return await querySession.Query<RefreshToken>()
            .Where(x => x.Token == token)
            .SingleOrDefaultAsync(cancellationToken);
    }
    
    public async Task<RefreshToken?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await querySession.Query<RefreshToken>()
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.ExpiresTime)
            .FirstOrDefaultAsync(cancellationToken);
    }
    
    public async Task UpdateAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
    {
        await using var session = documentStore.LightweightSession();
        
        session.Store(refreshToken);
        await session.SaveChangesAsync(cancellationToken);
    }
    
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await using var session = documentStore.LightweightSession();
        
        session.Delete<RefreshToken>(id);
        await session.SaveChangesAsync(cancellationToken);
    }
    
    private static string GetTokenFromEvent(RefreshTokenEvent @event)
    {
        return @event switch
        {
            RefreshTokenCreated created => created.Token,
            _ => throw new ArgumentException($"Unsupported event type: {@event.GetType().Name}")
        };
    }
}
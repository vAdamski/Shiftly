using Microsoft.EntityFrameworkCore;
using Shiftly.Domain.Entities;

namespace Shiftly.Application.Common.Interfaces.Persistence;

public interface IAppDbContext
{
    DbSet<EventEntity> Events { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<RefreshToken> RefreshTokens { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    void Dispose();
}
using Microsoft.EntityFrameworkCore;

namespace Shiftly.Persistence;

public class ShiftlyDbContextFactory : DesignTimeDbContextFactoryBase<AppDbContext>
{
    protected override AppDbContext CreateNewInstance(DbContextOptions<AppDbContext> options)
    {
        return new AppDbContext(options);
    }
}
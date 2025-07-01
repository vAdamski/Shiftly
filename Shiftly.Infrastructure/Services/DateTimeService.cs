using Shiftly.Application.Common.Interfaces.Infrastructure.Services;

namespace Shiftly.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
}
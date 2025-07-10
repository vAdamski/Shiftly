using Microsoft.Extensions.DependencyInjection;
using Shiftly.Application.Common.Interfaces.Infrastructure.Services;
using Shiftly.Infrastructure.Services;

namespace Shiftly.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IMessagePublisher, RabbitMqMessagePublisher>();

        return services;
    }
}
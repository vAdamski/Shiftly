using Microsoft.Extensions.DependencyInjection;
using Shiftly.Application.Common.Interfaces.Infrastructure.Services;
using Shiftly.Application.Common.Interfaces.Infrastructure.Services.EmailSender;
using Shiftly.Infrastructure.Services;
using Shiftly.Infrastructure.Services.EmailSender;

namespace Shiftly.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IMessagePublisher, RabbitMqMessagePublisher>();
        
        services.AddSingleton<IEmailSenderConfiguration, EmailSenderConfiguration>();
        services.AddTransient<IEmailSenderService, EmailSenderService>();

        return services;
    }
}
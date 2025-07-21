using MassTransit;
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
        services.AddTransient<IMessagePublisher, MassTransitMessagePublisher>();
        
        services.AddSingleton<IEmailSenderConfiguration, EmailSenderConfiguration>();
        services.AddTransient<IEmailSenderService, EmailSenderService>();
        
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });
        });

        return services;
    }
}
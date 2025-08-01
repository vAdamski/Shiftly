using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Shiftly.Application.Actions.UsersActions.Commands.RegisterUser;
using Shiftly.Application.Common.Interfaces.Application.Services.Emails;
using Shiftly.Application.Common.Interfaces.Infrastructure.Services;
using Shiftly.Application.Common.Interfaces.Infrastructure.Services.EmailSender;
using Shiftly.Infrastructure.Services;
using Shiftly.Infrastructure.Services.EmailSender;
using SmtpService = Shiftly.Infrastructure.Services.EmailSender.SmtpService;

namespace Shiftly.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IDateTime, DateTimeService>();

        services.AddSingleton<ISmtpConfiguration, SmtpConfiguration>();
        services.AddTransient<ISmtpService, SmtpService>();
        services.AddTransient<IEmailSenderService, EmailSenderSender>();

        services.AddMassTransit(x =>
        {
            x.AddConsumer<SendActivationEmailConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint("send-activation-email-queue", e =>
                {
                    e.ConfigureConsumer<SendActivationEmailConsumer>(context);
                });
            });
        });

        return services;
    }
}
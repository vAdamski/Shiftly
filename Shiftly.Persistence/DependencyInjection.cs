
using JasperFx;
using JasperFx.Events.Projections;
using Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Shiftly.Application.Common.Interfaces.Persistence.Repositories;
using Shiftly.Domain.Projections;
using Shiftly.Persistence.Repositories;

namespace Shiftly.Persistence;

public static class DependencyInjection
{
    private const string PostgresConnectionKey = "PostgresConnection";
    private const string RabbitMqConnectionKey = "RabbitMqConnection";
    private const string QueueName = "shiftly-queue";
    private const string SagasTableName = "sagas";
    private const string SagasIndexTableName = "sagas_index";

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        
        RegisterRepositories(services);
        ConfigureMarten(services, configuration);
        ConfigureRebus(services, configuration);

        return services;
    }

    private static void RegisterRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
    }

    private static void ConfigureMarten(IServiceCollection services, IConfiguration configuration)
    {
        var postgresConnection = configuration.GetConnectionString(PostgresConnectionKey) 
            ?? throw new InvalidOperationException($"Connection string '{PostgresConnectionKey}' not found.");

        services.AddMarten(options =>
        {
            options.Connection(postgresConnection);
            options.UseSystemTextJsonForSerialization();
            options.Projections.Add<UserProjection>(ProjectionLifecycle.Inline);

            if (IsRunningInDevelopment())
            {
                options.AutoCreateSchemaObjects = AutoCreate.All;
            }
        });
    }

    private static void ConfigureRebus(IServiceCollection services, IConfiguration configuration)
    {
        var postgresConnection = configuration.GetConnectionString(PostgresConnectionKey) 
            ?? throw new InvalidOperationException($"Connection string '{PostgresConnectionKey}' not found.");
        var rabbitMqConnection = configuration.GetConnectionString(RabbitMqConnectionKey)
            ?? throw new InvalidOperationException($"Connection string '{RabbitMqConnectionKey}' not found.");

        services.AddRebus(rebus =>
            rebus.Routing(r => r.TypeBased())
                .Transport(t => t.UseRabbitMq(rabbitMqConnection, QueueName))
                .Sagas(s => s.StoreInPostgres(postgresConnection, SagasTableName, SagasIndexTableName)));
    }

    private static bool IsRunningInDevelopment() => 
        Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
}
using JasperFx;
using Marten;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shiftly.Application.Common.Interfaces.Persistence;
using Shiftly.Application.Common.Interfaces.Persistence.Repositories;
using Shiftly.Persistence.Repositories;

namespace Shiftly.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        services.AddMarten(options =>
        {
            options.Connection(configuration.GetConnectionString("PostgresConnection"));
            
            options.UseSystemTextJsonForSerialization();
            
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                options.AutoCreateSchemaObjects = AutoCreate.All;
            }
        });
        
        return services;
    }
}
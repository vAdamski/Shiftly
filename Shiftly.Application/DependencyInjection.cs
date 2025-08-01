using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shiftly.Application.Actions.UsersActions.Commands.RegisterUser;
using Shiftly.Application.Common.Behaviours;
using Shiftly.Application.Common.Interfaces.Application;
using Shiftly.Application.Common.Interfaces.Application.Providers;
using Shiftly.Application.Common.Interfaces.Application.Services;
using Shiftly.Application.Common.Interfaces.Application.Services.Emails;
using Shiftly.Application.Providers;
using Shiftly.Application.Services;
using Shiftly.Application.Services.Emails;

namespace Shiftly.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true);
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        
        services.AddTransient<IPasswordHasher, PasswordHasher>();
        services.AddTransient<ITokenProvider, TokenProvider>();
        
        services.AddTransient<IEmailContextProvider, EmailContextProvider>();
        services.AddTransient<IActivationEmailService, ActivationEmailService>();
        
        return services;
    }
}
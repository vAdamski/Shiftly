using Microsoft.Extensions.DependencyInjection.Extensions;
using Shiftly.Api.Services;
using Shiftly.Application.Common.Interfaces.Api;

namespace Shiftly.Api.Configurations;

public static class DependencyInjection
{
	public static IServiceCollection AddApi(this IServiceCollection services)
	{
		services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
		services.TryAddScoped(typeof(ICurrentUserService), typeof(CurrentUserService));
		
		return services;
	}
}
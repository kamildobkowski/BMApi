using Application.Common.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Common.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		services.AddMediatR(cfg =>
		{
			cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
			cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
		});
		services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
		return services;
	}
}
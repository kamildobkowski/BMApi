using Application.Common.Repositories;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
	{
		services.AddDbContext<ItemsDbContext>();
		services.AddScoped<IItemRepository, ItemRepository>();
		services.AddScoped<IUnitOfWork, UnitOfWork>();
		return services;
	}
}
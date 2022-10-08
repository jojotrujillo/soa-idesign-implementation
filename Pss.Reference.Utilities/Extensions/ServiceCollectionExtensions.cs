using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pss.Reference.Common;

namespace Pss.Reference.Utilities.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddDistributedCache(this IServiceCollection services, IConfiguration configuration)
	{
		_ = configuration;  // Discard to appease the compiler.

		if (EnvironmentHelper.IsLocal())
		{
			services.AddDistributedMemoryCache();
		}
		//else
		//{
		//	services.AddStackExchangeRedisCache(options =>
		//	{
		//		// TODO: Be sure to update these constants with your Azure configuration values for Redis.
		//		options.Configuration = configuration[Constants.RedisConfiguration];
		//		options.InstanceName = configuration[Constants.RedisInstanceName];
		//	});
		//}

		return services;
	}
}

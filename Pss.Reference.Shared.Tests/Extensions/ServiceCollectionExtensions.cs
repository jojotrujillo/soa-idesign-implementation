using Pss.Reference.Common.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Pss.Reference.Shared.Tests.Extensions;

public static class ServiceCollectionExtensions
{
	public static void RegisterOverride<TService>(this ServiceCollection serviceCollection, TService instance)
		where TService : class, IServiceComponent
	{
		serviceCollection.RegisterOverride(instance, false);
	}

	public static IServiceProvider RegisterOverride<TService>(this ServiceCollection serviceCollection, TService instance, bool isBuildServiceProvider)
		where TService : class, IServiceComponent
	{
		serviceCollection.RemoveAll<TService>();
		serviceCollection.AddSingleton(instance);

		if (!isBuildServiceProvider)
			return null;

		return serviceCollection.BuildServiceProvider();
	}
}

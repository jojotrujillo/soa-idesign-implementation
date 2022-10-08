using Microsoft.Extensions.DependencyInjection;
using Pss.Reference.Common;
using Pss.Reference.Common.Contracts;
using Pss.Reference.Utilities.Contracts;

namespace Pss.Reference.Utilities;

public sealed class UtilityFactory : FactoryBase
{
	public UtilityFactory(AmbientContext context, IServiceProvider serviceProvider)
		: base(context, serviceProvider)
	{
	}

	public static void RegisterTypes(IServiceCollection serviceCollection)
	{
		RegisterType<ICacheUtility, DistributedCacheUtility>(serviceCollection);
		RegisterType<IHttpUtility, HttpUtility>(serviceCollection);
		RegisterType<IQueueUtility, NetMqQueueUtility>(serviceCollection);

		// HttpClients
		HttpUtility.AddHttpClient(serviceCollection);
	}

	public TService Create<TService>() where TService : class, IServiceComponent
	{
		var utility = Resolve<TService>();

		if (utility is not UtilityBase utilityBase)
			throw new InvalidOperationException($"{typeof(TService).Name} does not implement UtilityBase.");

		utilityBase.Context = Context;
		utilityBase.Factory = this;

		return utility;
	}
}

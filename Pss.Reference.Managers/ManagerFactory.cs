using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pss.Reference.Accessors;
using Pss.Reference.Common;
using Pss.Reference.Common.Contracts;
using Pss.Reference.Engines;
using Pss.Reference.Managers.Contracts;
using Pss.Reference.Utilities;

namespace Pss.Reference.Managers;

public sealed class ManagerFactory : FactoryBase
{
	public ManagerFactory(AmbientContext context, IServiceProvider serviceProvider)
		: base(context, serviceProvider)
	{
	}

	public static void RegisterTypes(IServiceCollection serviceCollection, IConfiguration configuration)
	{
		_ = configuration; // For future use.

		RegisterType<IProductManager, ProductManager>(serviceCollection);
		RegisterType<INotificationManager, NotificationManager>(serviceCollection);
		EngineFactory.RegisterTypes(serviceCollection);
		UtilityFactory.RegisterTypes(serviceCollection);
		AccessorFactory.RegisterTypes(serviceCollection);
	}

	public TService Create<TService>() where TService : class, IServiceComponent
	{
		return Create<TService>(null, null, null);
	}

	public TService Create<TService>(EngineFactory engineFactory, AccessorFactory accessorFactory, UtilityFactory utilityFactory)
		where TService : class, IServiceComponent
	{
		if (Context == null)
			throw new InvalidOperationException("Context cannot be null.");

		var manager = Resolve<TService>();

		if (manager is not ManagerBase managerBase)
			throw new InvalidOperationException($"{typeof(TService).Name} does not implement ManagerBase.");

		managerBase.Context = Context;
		managerBase.Factory = this;
		managerBase.EngineFactory = engineFactory ?? new EngineFactory(Context, ServiceProvider);
		managerBase.UtilityFactory = utilityFactory ?? new UtilityFactory(Context, ServiceProvider);
		managerBase.AccessorFactory = accessorFactory ?? new AccessorFactory(Context, ServiceProvider);

		return manager;
	}
}

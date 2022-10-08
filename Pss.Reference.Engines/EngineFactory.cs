using Microsoft.Extensions.DependencyInjection;
using Pss.Reference.Accessors;
using Pss.Reference.Common;
using Pss.Reference.Common.Contracts;
using Pss.Reference.Engines.Contracts;
using Pss.Reference.Utilities;

namespace Pss.Reference.Engines;

public sealed class EngineFactory : FactoryBase
{
	public EngineFactory(AmbientContext context, IServiceProvider serviceProvider)
		: base(context, serviceProvider)
	{
	}

	public static void RegisterTypes(IServiceCollection serviceCollection)
	{
		RegisterType<IValidationEngine, ValidationEngine>(serviceCollection);
	}

	public TService Create<TService>() where TService : class, IServiceComponent
	{
		return Create<TService>(null, null);
	}

	public TService Create<TService>(AccessorFactory accessorFactory, UtilityFactory utilityFactory) where TService : class, IServiceComponent
	{
		if (Context == null)
			throw new InvalidOperationException("Context cannot be null.");

		var engine = Resolve<TService>();

		if (engine is not EngineBase engineBase)
			throw new InvalidOperationException($"{typeof(TService).Name} does not implement EngineBase.");

		engineBase.Context = Context;
		engineBase.Factory = this;
		engineBase.AccessorFactory = accessorFactory ?? new AccessorFactory(Context, ServiceProvider);
		engineBase.UtilityFactory = utilityFactory ?? new UtilityFactory(Context, ServiceProvider);

		return engine;
	}
}

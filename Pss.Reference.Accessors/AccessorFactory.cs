using Microsoft.Extensions.DependencyInjection;
using Pss.Reference.Accessors.Contracts;
using Pss.Reference.Common;
using Pss.Reference.Common.Contracts;
using Pss.Reference.Utilities;

namespace Pss.Reference.Accessors;

public sealed class AccessorFactory : FactoryBase
{
	public AccessorFactory(AmbientContext context, IServiceProvider serviceProvider)
		: base(context, serviceProvider)
	{
	}

	public static void RegisterTypes(IServiceCollection serviceCollection)
	{
		RegisterType<IProductAccessor, SqlProductAccessor>(serviceCollection);
	}

	public TService Create<TService>() where TService : class, IServiceComponent
	{
		return Create<TService>(null);
	}

	public TService Create<TService>(UtilityFactory utilityFactory) where TService : class, IServiceComponent
	{
		if (Context == null)
			throw new InvalidOperationException("Context cannot be null.");

		var accessor = Resolve<TService>();

		if (accessor is not AccessorBase accessorBase)
			throw new InvalidOperationException($"{typeof(TService).Name} does not implement AccessorBase.");

		accessorBase.Context = Context;
		accessorBase.Factory = this;
		accessorBase.UtilityFactory = utilityFactory ?? new UtilityFactory(Context, ServiceProvider);

		return accessor;
	}
}

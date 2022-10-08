using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Pss.Reference.Common.Contracts;

namespace Pss.Reference.Common;

public abstract class FactoryBase
{
	public AmbientContext Context { get; }

	protected IServiceProvider ServiceProvider { get; }

	protected FactoryBase(AmbientContext context, IServiceProvider serviceProvider)
	{
		ServiceProvider = serviceProvider;
		Context = context ?? new AmbientContext();
	}

	public static void RegisterType<TInterface, TImplementation>(IServiceCollection serviceCollection)
		where TInterface : IServiceComponent
		where TImplementation : IServiceComponent, new()
	{
		RegisterType(serviceCollection, typeof(TInterface), typeof(TImplementation));
	}

	public bool IsTypeRegistered<TInterface>() where TInterface : IServiceComponent
	{
		return ServiceProvider.GetService<TInterface>() != null;
	}

	protected static void RegisterType(IServiceCollection serviceCollection, Type contract, Type implementation)
	{
		ValidateRegisterTypeArguments(contract, implementation);
		serviceCollection.TryAddTransient(contract, implementation);
	}

	private static void ValidateRegisterTypeArguments(Type contract, Type implementation)
	{
		if (contract == null)
			throw new InvalidOperationException($"The supplied '{nameof(contract)}' type is null and must be specified.");

		if (implementation == null)
			throw new InvalidOperationException($"The supplied '{nameof(implementation)}' type is null and must be specified.");

		if (!typeof(IServiceComponent).IsAssignableFrom(contract))
			throw new InvalidOperationException($"Type contract '{contract.FullName}' must implement '{typeof(IServiceComponent).FullName}'");

		if (!typeof(IServiceComponent).IsAssignableFrom(implementation))
			throw new InvalidOperationException($"Type implementation '{contract.FullName}' must inherit '{typeof(IServiceComponent).FullName}'");

		if (!contract.IsAssignableFrom(implementation))
			throw new InvalidOperationException($"The type '{implementation.FullName}' must implement '{contract.FullName}'");

		if (implementation.GetConstructor(Type.EmptyTypes) == null)
			throw new InvalidOperationException($"Type '{implementation.FullName}' cannot be used because it does not have a parameterless constructor.");
	}

	protected TService Resolve<TService>() where TService : class, IServiceComponent
	{
		return (TService)Resolve(typeof(TService));
	}

	public TService ResolveRequiredService<TService>() where TService : class
	{
		return ServiceProvider.GetRequiredService<TService>();
	}

	private object Resolve(Type serviceType)
	{
		if (serviceType == null)
			throw new ArgumentNullException(nameof(serviceType));

		var instance = ServiceProvider.GetService(serviceType);

		if (instance == null)
			throw new InvalidOperationException($"The type '{serviceType.Name}' has not been registered in this factory.");

		if (!serviceType.IsInstanceOfType(instance))
			throw new InvalidOperationException($"Unexpected type returned.  Expected '{serviceType}' but received '{instance.GetType()}'.");

		return instance;
	}
}

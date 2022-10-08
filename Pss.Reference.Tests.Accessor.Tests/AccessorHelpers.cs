using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Pss.Reference.Accessors;
using Pss.Reference.Common.Contracts;
using Pss.Reference.Shared.Tests;
using Pss.Reference.Utilities;

namespace Pss.Reference.Accessor.Tests;

internal static class AccessorHelpers
{
	public static TAccessor GetAccessor<TAccessor>() where TAccessor : class, IServiceComponent
	{
		var factory = new AccessorFactory(new AmbientContext(), AssemblyInitialize.ServiceProvider);
		var accessor = factory.Create<TAccessor>();
		return accessor;
	}

	public static TAccessor GetAccessorForUnitTests<TAccessor, TDbContext>(TDbContext context, IServiceProvider serviceProvider = null)
		where TDbContext : DbContext where TAccessor : AccessorBase
	{
		BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
		serviceProvider ??= AssemblyInitialize.ServiceProvider;
		var factory = new AccessorFactory(new AmbientContext(), serviceProvider);
		var utilityFactory = new UtilityFactory(factory.Context, serviceProvider);
		var accessor = (TAccessor)Activator.CreateInstance(typeof(TAccessor), flags, null, new object[] { DbContextHelpers.GetDbContextFactoryMock(context).Object }, null);
		accessor.Factory = factory;
		accessor.UtilityFactory = utilityFactory;
		return accessor;
	}
}

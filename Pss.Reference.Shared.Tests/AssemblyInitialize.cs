using Pss.Reference.Common;
using Pss.Reference.Managers;
using Pss.Reference.Utilities.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace Pss.Reference.Shared.Tests;

public class AssemblyInitialize
{
	public static ServiceCollection ServiceCollection { get; private set; }
	public static ServiceProvider ServiceProvider { get; private set; }
	public static IConfiguration Configuration { get; private set; }

	protected static void InitializeAssembly()
	{
		Environment.SetEnvironmentVariable(Constants.Environments.AspNetCoreEnvironment, Constants.Environments.Local);

		var builder = new ConfigurationBuilder()
			.AddEnvironmentVariables()
			//.AddAzureAppConfiguration()
			;

		Configuration = builder.Build();

		var serviceCollection = new ServiceCollection();
		serviceCollection.AddLogging(b => b.AddDebug());
		serviceCollection.AddSingleton(typeof(IConfiguration), Configuration);
		serviceCollection.AddDistributedCache(Configuration);

		ManagerFactory.RegisterTypes(serviceCollection, Configuration);

		ServiceCollection = serviceCollection;
		ServiceProvider = serviceCollection.BuildServiceProvider();
	}

	/// Call this Method to create a copy of the ServiceCollection.
	/// AssemblyInitialize.ServiceCollection should not be passed into a class derived from FactoryBase that then calls RegisterOverride.
	/// The RegisterOverride method modifies the static ServiceCollection property for all subsequent tests .
	public static ServiceCollection CopyServiceCollection()
	{
		var serviceCollection = new ServiceCollection();
		foreach (var item in ServiceCollection)
		{
			serviceCollection.Add(item);
		}

		return serviceCollection;
	}
}

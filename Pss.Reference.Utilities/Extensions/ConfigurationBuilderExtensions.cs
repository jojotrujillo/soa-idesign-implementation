using System.Diagnostics;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Pss.Reference.Common;

namespace Pss.Reference.Utilities.Extensions;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public static class ConfigurationBuilderExtensions
{
	public static IConfigurationBuilder AddAzureAppConfiguration(this IConfigurationBuilder configurationBuilder)
	{
		if (configurationBuilder == null)
			return null;

		configurationBuilder
			.AddEnvironmentVariables()
			.AddJsonFile("appsettings.json", false, false);

		var configuration = configurationBuilder.Build();

		configurationBuilder.AddAzureAppConfiguration(o => AddApplicationKeysAppConfiguration(o, configuration));
		var configWithAppKeys = configurationBuilder.Build();

		// adds key vault configuration values ahead of importing global values from AppConfig to avoid errors
		configurationBuilder.AddAzureAppConfiguration(o => AddServiceLevelAppConfiguration(o, configWithAppKeys));

		var configurationWithServiceLevelConfig = configurationBuilder.Build();
		configurationBuilder.AddAzureAppConfiguration(o => AddGlobalAppConfiguration(o, configurationWithServiceLevelConfig));

		return configurationBuilder;
	}

	private static void AddApplicationKeysAppConfiguration(AzureAppConfigurationOptions options, IConfigurationRoot configuration)
	{
		var prefix = Constants.AppConfig.AppPrefix;
		options.Connect(configuration[Constants.ConnectionStrings.AppConfig])
			   .Select($"{prefix}AzureAd").TrimKeyPrefix(prefix)
			   .Select($"{prefix}AzureAd", GetEnvironment()).TrimKeyPrefix(prefix);
	}

	private static void AddGlobalAppConfiguration(AzureAppConfigurationOptions options, IConfigurationRoot configuration)
	{
		AddServiceLevelAppConfiguration(Constants.AppConfig.GlobalPrefix, options, configuration);
	}

	private static void AddServiceLevelAppConfiguration(AzureAppConfigurationOptions options, IConfiguration configuration)
	{
		AddServiceLevelAppConfiguration(Constants.AppConfig.AppPrefix, options, configuration);
	}

	private static void AddServiceLevelAppConfiguration(string prefix, AzureAppConfigurationOptions options, IConfiguration configuration)
	{
		options.Connect(configuration[Constants.ConnectionStrings.AppConfig])
			   .Select($"{prefix}*").TrimKeyPrefix(prefix)
			   .Select($"{prefix}*", GetEnvironment()).TrimKeyPrefix(prefix)
			   .ConfigureKeyVault(kv =>
			   {
				   Debug.Assert(!string.IsNullOrWhiteSpace(configuration[Constants.AzureAd.TenantId]));

				   kv.SetCredential(new ClientSecretCredential(configuration[Constants.AzureAd.TenantId],
					   configuration[Constants.AzureAd.ClientId],
					   configuration[Constants.AzureAd.ClientSecret]));
			   })
			   .ConfigureRefresh(refresh =>
			   {
				   refresh.Register(Constants.AppConfig.Sentinel, true);
				   refresh.SetCacheExpiration(TimeSpan.FromMinutes(5));
			   });
	}

	private static string GetEnvironment()
	{
#if DEBUG
		return EnvironmentHelper.GetEnvironment() ?? Constants.Environments.Local;
#else
			return EnvironmentHelper.GetEnvironment();
#endif
	}
}

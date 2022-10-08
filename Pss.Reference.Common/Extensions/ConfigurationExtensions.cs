using Microsoft.Extensions.Configuration;

namespace Pss.Reference.Common.Extensions;

public static class ConfigurationExtensions
{
	public static int? GetInt(this IConfiguration configuration, string key, int? defaultValue = null)
	{
		if (configuration == null)
			throw new ArgumentNullException(nameof(configuration));

		if (string.IsNullOrWhiteSpace(key))
			throw new ArgumentNullException(nameof(key));

		var value = defaultValue;

		var rawValue = configuration[key];
		if (!string.IsNullOrWhiteSpace(rawValue))
		{
			if (int.TryParse(rawValue, out var parsedValue))
				value = parsedValue;
		}

		return value;
	}

	public static bool? GetBool(this IConfiguration configuration, string key, bool? defaultValue = null)
	{
		if (configuration == null)
			throw new ArgumentNullException(nameof(configuration));

		if (string.IsNullOrWhiteSpace(key))
			throw new ArgumentNullException(nameof(key));

		var value = defaultValue;

		var rawValue = configuration[key];
		if (!string.IsNullOrWhiteSpace(rawValue))
		{
			if (bool.TryParse(rawValue, out var parsedValue))
				value = parsedValue;
		}

		return value;
	}
}

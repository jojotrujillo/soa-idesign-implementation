using System.Diagnostics;

namespace Pss.Reference.Common;

public static class EnvironmentHelper
{
	public static bool IsLocal()
	{
		if (Debugger.IsAttached)
			return true;

		return IsEnvironment(Constants.Environments.Local);
	}

	public static bool IsDevelopment()
	{
		return IsEnvironment(Constants.Environments.Development);
	}

	public static bool IsQA()
	{
		return IsEnvironment(Constants.Environments.QA);
	}

	public static bool IsStaging()
	{
		return IsEnvironment(Constants.Environments.Staging);
	}

	public static bool IsProduction()
	{
		return IsEnvironment(Constants.Environments.Production);
	}

	public static bool IsEnvironment(string environmentName)
	{
		return Environment.GetEnvironmentVariable(Constants.Environments.AspNetCoreEnvironment).Equals(environmentName, StringComparison.OrdinalIgnoreCase);
	}

	public static string GetEnvironment()
	{
		string environmentVariable = Environment.GetEnvironmentVariable(Constants.Environments.AspNetCoreEnvironment);

		if (string.IsNullOrWhiteSpace(environmentVariable))
		{
			throw new Exception($"Missing EnvironmentVariable: {Constants.Environments.AspNetCoreEnvironment}");
		}

		return environmentVariable;
	}
}

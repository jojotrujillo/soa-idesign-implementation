using Pss.Reference.Common.Contracts;
using Pss.Reference.Common.Extensions;
using Pss.Reference.Managers;
using Pss.Reference.Shared.Tests;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Pss.Reference.Common.Tests.UnitTests;

[TestClass]
[TestCategory(Constants.Testing.UnitTest)]
public class ConfigurationExtensionsTests
{
	#region GetInt
	[TestMethod]
	public void GetInt_WithNullConfiguration_ThrowsException()
	{
		// ARRANGE
		IConfiguration configuration = null;

		// ACT && ASSERT
		Assert.ThrowsException<ArgumentNullException>(() => configuration.GetInt(""));
	}

	[TestMethod]
	public void GetInt_WithNullKey_ThrowsException()
	{
		// ARRANGE
		IConfiguration configuration = null;

		// ACT && ASSERT
		Assert.ThrowsException<ArgumentNullException>(() => configuration.GetInt(null));
	}

	[TestMethod]
	public void GetInt_WithValidKeyAndValueInConfiguration_ReturnsConfiguredValueAsInt()
	{
		// ARRANGE
		var mockConfiguration = new Mock<IConfiguration>();
		var expectedConfigValueAsInt = 1;

		mockConfiguration.SetupGet(c => c[It.IsAny<string>()])
			.Returns(expectedConfigValueAsInt.ToString());

		var configuration = GetConfiguration((serviceCollection) =>
		{
			serviceCollection.AddSingleton(mockConfiguration.Object);
		});

		// ACT
		var actual = configuration.GetInt("key");

		// ASSERT
		Assert.AreEqual(expectedConfigValueAsInt, actual.Value);
	}

	[TestMethod]
	public void GetInt_WithValidKeyAndNoValueInConfiguration_ReturnsNull()
	{
		// ARRANGE
		var mockConfiguration = new Mock<IConfiguration>();

		mockConfiguration.SetupGet(c => c[It.IsAny<string>()])
			.Returns((string)null);

		var configuration = GetConfiguration((serviceCollection) =>
		{
			serviceCollection.AddSingleton(mockConfiguration.Object);
		});

		// ACT
		var actual = configuration.GetInt("key");

		// ASSERT
		Assert.IsNull(actual);
	}

	[TestMethod]
	public void GetInt_WithValidKeyAndNoValueInConfigurationSpecifyingDefault_ReturnsSpecifiedDefaultValueAsInt()
	{
		// ARRANGE
		var mockConfiguration = new Mock<IConfiguration>();
		var defaultValue = 1;

		mockConfiguration.SetupGet(c => c[It.IsAny<string>()])
			.Returns((string)null);

		var configuration = GetConfiguration((serviceCollection) =>
		{
			serviceCollection.AddSingleton(mockConfiguration.Object);
		});

		// ACT
		var actual = configuration.GetInt("key", defaultValue);

		// ASSERT
		Assert.AreEqual(defaultValue, actual);
	}

	[TestMethod]
	public void GetInt_WithValidKeyAndValueInConfigurationSpecifyingDefault_ReturnsConfiguredValueAsInt()
	{
		// ARRANGE
		var mockConfiguration = new Mock<IConfiguration>();
		var defaultValue = 1;
		var expectedConfigValueAsInt = 99;


		mockConfiguration.SetupGet(c => c[It.IsAny<string>()])
			.Returns(expectedConfigValueAsInt.ToString);

		var configuration = GetConfiguration((serviceCollection) =>
		{
			serviceCollection.AddSingleton(mockConfiguration.Object);
		});

		// ACT
		var actual = configuration.GetInt("key", defaultValue);

		// ASSERT
		Assert.AreEqual(expectedConfigValueAsInt, actual);
	}
	#endregion

	#region GetBool
	[TestMethod]
	public void GetBool_WithNullConfiguration_ThrowsException()
	{
		// ARRANGE
		IConfiguration configuration = null;

		// ACT && ASSERT
		Assert.ThrowsException<ArgumentNullException>(() => configuration.GetBool(""));
	}

	[TestMethod]
	public void GetBool_WithNullKey_ThrowsException()
	{
		// ARRANGE
		IConfiguration configuration = null;

		// ACT && ASSERT
		Assert.ThrowsException<ArgumentNullException>(() => configuration.GetBool(null));
	}

	[TestMethod]
	public void GetBool_WithValidKeyAndValueInConfiguration_ReturnsConfiguredValueAsBool()
	{
		// ARRANGE
		var mockConfiguration = new Mock<IConfiguration>();
		var expectedConfigValueAsBool = true;

		mockConfiguration.SetupGet(c => c[It.IsAny<string>()])
			.Returns(expectedConfigValueAsBool.ToString());

		var configuration = GetConfiguration((serviceCollection) =>
		{
			serviceCollection.AddSingleton(mockConfiguration.Object);
		});

		// ACT
		var actual = configuration.GetBool("key");

		// ASSERT
		Assert.AreEqual(expectedConfigValueAsBool, actual.Value);
	}

	[TestMethod]
	public void GetBool_WithValidKeyAndNoValueInConfiguration_ReturnsNull()
	{
		// ARRANGE
		var mockConfiguration = new Mock<IConfiguration>();

		mockConfiguration.SetupGet(c => c[It.IsAny<string>()])
			.Returns((string)null);

		var configuration = GetConfiguration((serviceCollection) =>
		{
			serviceCollection.AddSingleton(mockConfiguration.Object);
		});

		// ACT
		var actual = configuration.GetBool("key");

		// ASSERT
		Assert.IsNull(actual);
	}

	[TestMethod]
	public void GetBool_WithValidKeyAndNoValueInConfigurationSpecifyingDefault_ReturnsSpecifiedDefaultValueAsBool()
	{
		// ARRANGE
		var mockConfiguration = new Mock<IConfiguration>();
		var defaultValue = true;

		mockConfiguration.SetupGet(c => c[It.IsAny<string>()])
			.Returns((string)null);

		var configuration = GetConfiguration((serviceCollection) =>
		{
			serviceCollection.AddSingleton(mockConfiguration.Object);
		});

		// ACT
		var actual = configuration.GetBool("key", defaultValue);

		// ASSERT
		Assert.AreEqual(defaultValue, actual);
	}

	[TestMethod]
	public void GetBool_WithValidKeyAndValueInConfigurationSpecifyingDefault_ReturnsConfiguredValueAsBool()
	{
		// ARRANGE
		var mockConfiguration = new Mock<IConfiguration>();
		var defaultValue = false;
		var expectedConfigValueAsBool = true;


		mockConfiguration.SetupGet(c => c[It.IsAny<string>()])
			.Returns(expectedConfigValueAsBool.ToString);

		var configuration = GetConfiguration((serviceCollection) =>
		{
			serviceCollection.AddSingleton(mockConfiguration.Object);
		});

		// ACT
		var actual = configuration.GetBool("key", defaultValue);

		// ASSERT
		Assert.AreEqual(expectedConfigValueAsBool, actual);
	}
	#endregion

	#region Private Helpers
	private IConfiguration GetConfiguration(Action<ServiceCollection> registerOverrides = null)
	{
		var serviceCollection = AssemblyInitialize.CopyServiceCollection();

		registerOverrides?.Invoke(serviceCollection);

		var serviceProvider = serviceCollection.BuildServiceProvider();

		var managerFactory = new ManagerFactory(new AmbientContext(), serviceProvider);

		var configuration = managerFactory.ResolveRequiredService<IConfiguration>();

		return configuration;
	}
	#endregion

}

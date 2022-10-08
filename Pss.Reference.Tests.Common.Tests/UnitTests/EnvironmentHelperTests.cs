using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pss.Reference.Common.Tests.UnitTests;

[TestClass]
[TestCategory(Constants.Testing.UnitTest)]
public class EnvironmentHelperTests
{
	#region Properties
	private static string OriginalEnvironmentValue { get; set; }
	#endregion

	#region Class Initialize/Cleanup
	[ClassInitialize]
	public static void ClassInitialize(TestContext context)
	{
		_ = context; // Avoid CA1801 & IDE0060
		OriginalEnvironmentValue = Environment.GetEnvironmentVariable(Constants.Environments.AspNetCoreEnvironment);
		Assert.AreEqual(Constants.Environments.Local, OriginalEnvironmentValue);
	}

	[ClassCleanup]
	public static void ClassCleanup()
	{
		Environment.SetEnvironmentVariable(Constants.Environments.AspNetCoreEnvironment, OriginalEnvironmentValue);
	}
	#endregion

	[TestMethod]
	public void IsLocal_NoParams_ReturnsTrueForLocalEnvironment()
	{
		// ARRANGE
		Environment.SetEnvironmentVariable(Constants.Environments.AspNetCoreEnvironment, Constants.Environments.Local);

		// ACT
		bool result = EnvironmentHelper.IsLocal();

		// ASSERT
		Assert.IsTrue(result);
	}

	[TestMethod]
	public void IsDevelopment_NoParams_ReturnsTrueForDevelopmentEnvironment()
	{
		// ARRANGE
		Environment.SetEnvironmentVariable(Constants.Environments.AspNetCoreEnvironment, Constants.Environments.Development);

		// ACT
		bool result = EnvironmentHelper.IsDevelopment();

		// ASSERT
		Assert.IsTrue(result);
	}

	[TestMethod]
	public void IsDevelopment_NoParams_ReturnsFalseWhenQAEnvironment()
	{
		// ARRANGE
		Environment.SetEnvironmentVariable(Constants.Environments.AspNetCoreEnvironment, Constants.Environments.QA);

		// ACT
		bool result = EnvironmentHelper.IsDevelopment();

		// ASSERT
		Assert.IsFalse(result);
	}

	[TestMethod]
	public void IsQA_NoParams_ReturnsTrueForQAEnvironment()
	{
		// ARRANGE
		Environment.SetEnvironmentVariable(Constants.Environments.AspNetCoreEnvironment, Constants.Environments.QA);

		// ACT
		bool result = EnvironmentHelper.IsQA();

		// ASSERT
		Assert.IsTrue(result);
	}

	[TestMethod]
	public void IsQA_NoParams_ReturnsFalseWhenProductionEnvironment()
	{
		// ARRANGE
		Environment.SetEnvironmentVariable(Constants.Environments.AspNetCoreEnvironment, Constants.Environments.Production);

		// ACT
		bool result = EnvironmentHelper.IsQA();

		// ASSERT
		Assert.IsFalse(result);
	}

	[TestMethod]
	public void IsStaging_NoParams_ReturnsTrueForStagingEnvironment()
	{
		// ARRANGE
		Environment.SetEnvironmentVariable(Constants.Environments.AspNetCoreEnvironment, Constants.Environments.Staging);

		// ACT
		bool result = EnvironmentHelper.IsStaging();

		// ASSERT
		Assert.IsTrue(result);
	}

	[TestMethod]
	public void IsStaging_NoParams_ReturnsFalseWhenDevelopmentEnvironment()
	{
		// ARRANGE
		Environment.SetEnvironmentVariable(Constants.Environments.AspNetCoreEnvironment, Constants.Environments.Development);

		// ACT
		bool result = EnvironmentHelper.IsStaging();

		// ASSERT
		Assert.IsFalse(result);
	}

	[TestMethod]
	public void IsProduction_NoParams_ReturnsTrueForProductionEnvironment()
	{
		// ARRANGE
		Environment.SetEnvironmentVariable(Constants.Environments.AspNetCoreEnvironment, Constants.Environments.Production);

		// ACT
		bool result = EnvironmentHelper.IsProduction();

		// ASSERT
		Assert.IsTrue(result);
	}

	[TestMethod]
	public void IsProduction_NoParams_ReturnsFalseWhenStagingEnvironment()
	{
		// ARRANGE
		Environment.SetEnvironmentVariable(Constants.Environments.AspNetCoreEnvironment, Constants.Environments.Staging);

		// ACT
		bool result = EnvironmentHelper.IsProduction();

		// ASSERT
		Assert.IsFalse(result);
	}

	[TestMethod]
	[DataRow(Constants.Environments.Development)]
	[DataRow(Constants.Environments.QA)]
	[DataRow(Constants.Environments.Staging)]
	[DataRow(Constants.Environments.Production)]
	public void IsEnvironment_ValidEnvironmentParams_ReturnsTrueForEachEnvironment(string environment)
	{
		// ARRANGE
		Environment.SetEnvironmentVariable(Constants.Environments.AspNetCoreEnvironment, environment);

		// ACT
		bool result = EnvironmentHelper.IsEnvironment(environment);

		// ASSERT
		Assert.IsTrue(result);
	}

	[TestMethod]
	[DataRow("")]
	[DataRow("123")]
	[DataRow("StagingX")]
	[DataRow("__Production")]
	public void IsEnvironment_InvalidEnvironmentParams_ReturnsFalseForEachEnvironment(string environment)
	{
		// ARRANGE
		Environment.SetEnvironmentVariable(Constants.Environments.AspNetCoreEnvironment, Constants.Environments.Development);

		// ACT
		bool result = EnvironmentHelper.IsEnvironment(environment);

		// ASSERT
		Assert.IsFalse(result);
	}

	[TestMethod]
	public void GetEnvironment_NoParams_ReturnsExpectedEnvironment()
	{
		// ARRANGE
		Environment.SetEnvironmentVariable(Constants.Environments.AspNetCoreEnvironment, Constants.Environments.Local);

		// ACT
		var result = EnvironmentHelper.GetEnvironment();

		// ASSERT
		Assert.AreEqual(Constants.Environments.Local, result);
	}

	[TestMethod]
	public void GetEnvironment_SetToNull_ThrowsExpectedException()
	{
		// ARRANGE
		Environment.SetEnvironmentVariable(Constants.Environments.AspNetCoreEnvironment, null);

		// ACT & ASSERT
		Assert.ThrowsException<Exception>(() => EnvironmentHelper.GetEnvironment());
	}
}

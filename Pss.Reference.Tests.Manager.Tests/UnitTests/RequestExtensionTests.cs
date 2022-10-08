using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pss.Reference.Common;
using Pss.Reference.Managers.Extensions;
using Pss.Reference.Shared.Tests;
using Client = Pss.Reference.Contracts.Client.Products;

namespace Pss.Reference.Manager.Tests.UnitTests;

[TestClass]
[TestCategory(Constants.Testing.UnitTest)]
public class RequestExtensionTests
{
	// Test DTO conversions with null.
	// Test DTO conversion with default (empty) contract.
	// Test DTO conversion with fully populated contract.

	[TestMethod]
	public void ToLogic_WithNull_ReturnsNull()
	{
		// ARRANGE
		Client.FindRequest request = null;

		// ACT
		var results = request.ToLogic();

		// ASSERT
		Assert.IsNull(results);
	}

	[TestMethod]
	public void ToLogic_WithDefaultEmptyContract_ReturnsExpectedContract()
	{
		// ARRANGE
		Client.FindRequest request = new();

		// ACT
		var results = request.ToLogic();

		// ASSERT
		Assert.IsNotNull(results);
		TestHelper.ValidatePropertyCount(request, results);
		// NOTE: ProductType is a different enumeration type between Client and Logic contracts.
		TestHelper.ValidatePropertyValues(request, results, "ProductType");
		Assert.AreEqual(request.ProductType.ToString(), results.ProductType.ToString());
	}

	[TestMethod]
	public void ToLogic_WithFullyPopulatedContract_ReturnsExpectedContractValues()
	{
		// ARRANGE
		var productType = TestHelper.GetRandomElement(Enum.GetValues<Client.ProductType>());
		var request = new Fixture()
			.Build<Client.FindRequest>()
			.With(r => r.ProductType, productType)
			.Create();

		// ACT
		var results = request.ToLogic();

		// ASSERT
		Assert.IsNotNull(results);
		TestHelper.ValidatePropertyCount(request, results);
		// NOTE: ProductType is a different enumeration type between Client and Logic contracts.
		TestHelper.ValidatePropertyValues(request, results, "ProductType");
		Assert.AreEqual(request.ProductType.ToString(), results.ProductType.ToString());
	}
}

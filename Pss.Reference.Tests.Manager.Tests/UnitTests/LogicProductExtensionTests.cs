using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pss.Reference.Common;
using Pss.Reference.Managers.Extensions;
using Pss.Reference.Shared.Tests;
using Logic = Pss.Reference.Contracts.Logic.Products;

namespace Pss.Reference.Manager.Tests.UnitTests;

[TestClass]
[TestCategory(Constants.Testing.UnitTest)]
public class LogicProductExtensionTests
{
	// Test DTO conversions (several test cases) with null.
	// Test DTO conversion with default (empty) contract.
	// Test DTO conversion with array of (empty) default contracts of each type.
	// Test DTO conversion with fully populated with all contract types.

	[TestMethod]
	public void ToClient_WithNullArray_ReturnsNull()
	{
		// ARRANGE
		Logic.ProductBase[] products = null;

		// ACT
		var results = products.ToClient();

		// ASSERT
		Assert.IsNull(results);
	}

	[TestMethod]
	public void ToClient_WithNullContract_ReturnsNull()
	{
		// ARRANGE
		Logic.ProductBase products = null;

		// ACT
		var results = products.ToClient();

		// ASSERT
		Assert.IsNull(results);
	}

	[TestMethod]
	public void ToClient_EachProductTypeIsNull_ReturnsNull()
	{
		// ARRANGE
		Logic.Commodity commodity = null;
		Logic.SalonProduct salonProduct = null;
		Logic.Vehicle vehicle = null;

		// ACT
		var commodityResults = commodity.ToClient();
		var salonResults = salonProduct.ToClient();
		var vehicleResults = vehicle.ToClient();

		// ASSERT
		Assert.IsNull(commodityResults);
		Assert.IsNull(salonResults);
		Assert.IsNull(vehicleResults);
	}

	[TestMethod]
	public void ToCommodity_WithNull_ReturnsNull()
	{
		// ARRANGE
		Logic.Commodity commodity = null;

		// ACT
		var commodityResults = commodity.ToCommodity();

		// ASSERT
		Assert.IsNull(commodityResults);
	}

	[TestMethod]
	public void ToSalonProduct_WithNull_ReturnsNull()
	{
		// ARRANGE
		Logic.SalonProduct salonProduct = null;

		// ACT
		var salonResults = salonProduct.ToSalonProduct();

		// ASSERT
		Assert.IsNull(salonResults);
	}

	[TestMethod]
	public void ToVehicle_WithNull_ReturnsNull()
	{
		// ARRANGE
		Logic.Vehicle vehicle = null;

		// ACT
		var vehicleResults = vehicle.ToVehicle();

		// ASSERT
		Assert.IsNull(vehicleResults);
	}

	[TestMethod]
	public void ToClient_AllProductTypesWithEmptyDefaultValues_ReturnsNull()
	{
		// ARRANGE
		List<Logic.ProductBase> products = new()
		{
			new Logic.Commodity(),
			new Logic.SalonProduct(),
			new Logic.Vehicle()
		};
		var productsArray = products.ToArray();

		// ACT
		var results = productsArray.ToClient();

		// ASSERT
		Assert.IsNotNull(results);
		Assert.AreEqual(productsArray.Length, results.Length);
		for (int i = 0; i < productsArray.Length; i++)
		{
			TestHelper.ValidatePropertyCount(productsArray[i], results[i]);
			// NOTE: ProductType is a different enumeration type between Client and Logic contracts.
			TestHelper.ValidatePropertyValues(productsArray[i], results[i], "ProductType");
			Assert.AreEqual(productsArray[i].ProductType.ToString(), results[i].ProductType.ToString());
		}
	}

	[TestMethod]
	public void ToClient_FullyPopulatedWithAllProductTypes_ReturnsExpectedContracts()
	{
		// ARRANGE
		var products = ContractHelpers.GetLogicProducts();

		// ACT
		var results = products.ToClient();

		// ASSERT
		Assert.IsNotNull(results);
		Assert.AreEqual(products.Length, results.Length);
		for (int i = 0; i < products.Length; i++)
		{
			TestHelper.ValidatePropertyCount(products[i], results[i]);
			// NOTE: ProductType is a different enumeration type between Client and Logic contracts.
			TestHelper.ValidatePropertyValues(products[i], results[i], "ProductType");
			Assert.AreEqual(products[i].ProductType.ToString(), results[i].ProductType.ToString());
		}
	}
}

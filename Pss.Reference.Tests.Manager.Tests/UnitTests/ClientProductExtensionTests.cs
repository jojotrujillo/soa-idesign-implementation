using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pss.Reference.Accessors.Products.Extensions;
using Pss.Reference.Common;
using Pss.Reference.Managers.Extensions;
using Pss.Reference.Shared.Tests;
using Client = Pss.Reference.Contracts.Client.Products;

namespace Pss.Reference.Manager.Tests.UnitTests;

[TestClass]
[TestCategory(Constants.Testing.UnitTest)]
public class ClientProductExtensionTests
{
	// Test DTO conversions (several test cases) with null.
	// Test DTO conversion with default (empty) contract.
	// Test DTO conversion with array of (empty) default contracts of each type.
	// Test DTO conversion with fully populated with all contract types.

	[TestMethod]
	public void ToLogic_WithNullArray_ReturnsNull()
	{
		// ARRANGE
		Client.ProductBase[] products = null;

		// ACT
		var results = products.ToLogic();

		// ASSERT
		Assert.IsNull(results);
	}

	[TestMethod]
	public void ToLogic_WithNullContract_ReturnsNull()
	{
		// ARRANGE
		Client.ProductBase products = null;

		// ACT
		var results = products.ToLogic();

		// ASSERT
		Assert.IsNull(results);
	}

	[TestMethod]
	public void ToLogic_EachProductTypeIsNull_ReturnsNull()
	{
		// ARRANGE
		Client.Commodity commodity = null;
		Client.SalonProduct salonProduct = null;
		Client.Vehicle vehicle = null;

		// ACT
		var commodityResults = commodity.ToLogic();
		var salonResults = salonProduct.ToLogic();
		var vehicleResults = vehicle.ToLogic();

		// ASSERT
		Assert.IsNull(commodityResults);
		Assert.IsNull(salonResults);
		Assert.IsNull(vehicleResults);
	}

	[TestMethod]
	public void ToCommodity_WithNull_ReturnsNull()
	{
		// ARRANGE
		Client.Commodity commodity = null;

		// ACT
		var commodityResults = commodity.ToCommodity();

		// ASSERT
		Assert.IsNull(commodityResults);
	}

	[TestMethod]
	public void ToSalonProduct_WithNull_ReturnsNull()
	{
		// ARRANGE
		Client.SalonProduct salonProduct = null;

		// ACT
		var salonResults = salonProduct.ToSalonProduct();

		// ASSERT
		Assert.IsNull(salonResults);
	}

	[TestMethod]
	public void ToVehicle_WithNull_ReturnsNull()
	{
		// ARRANGE
		Client.Vehicle vehicle = null;

		// ACT
		var vehicleResults = vehicle.ToVehicle();

		// ASSERT
		Assert.IsNull(vehicleResults);
	}

	[TestMethod]
	public void ToLogic_AllProductTypesWithEmptyDefaultValues_ReturnsNull()
	{
		// ARRANGE
		List<Client.ProductBase> products = new()
		{
			new Client.Commodity(),
			new Client.SalonProduct(),
			new Client.Vehicle()
		};
		var productsArray = products.ToArray();

		// ACT
		var results = productsArray.ToLogic();

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
	public void ToLogic_FullyPopulatedWithAllProductTypes_ReturnsExpectedContracts()
	{
		// ARRANGE
		var products = ContractHelpers.GetClientProducts();

		// ACT
		var results = products.ToLogic();

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

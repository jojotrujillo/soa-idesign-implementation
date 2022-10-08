using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pss.Reference.Accessors.Common;
using Pss.Reference.Accessors.Contracts;
using Pss.Reference.Accessors.Products;
using Pss.Reference.Accessors.Products.Configuration;
using Pss.Reference.Accessors.Products.Extensions;
using Pss.Reference.Common;
using Pss.Reference.Contracts.Logic.Exceptions;
using Pss.Reference.Shared.Tests;
using Logic = Pss.Reference.Contracts.Logic;

namespace Pss.Reference.Accessor.Tests.IntegrationTests;

[TestClass]
[TestCategory(Constants.Testing.IntegrationTest)]
public class SqlProductAccessorTests
{
	#region Test Cases
	// Find all products (default FindRequest)
	// Find product by Product Type (assumes all types are in the database)
	// Find product by ProductId
	// Find product by Non-existing ProductId
	// Find product by all request parameters
	// Find product by invalid criteria
	// Store new product, verify stored
	// Store (update) existing product, verify stored
	// Remove existing product, verify removed
	// Remove non-existing product should throw NotFoundException, verify exception
	#endregion

	private static readonly IDbContextFactory<ProductDbContext> _dbContextFactory = new DbContextFactory<ProductDbContext>();

	// Find all products (default FindRequest)
	[TestMethod]
	public async Task Find_DefaultFindRequest_ReturnsAllProducts()
	{
		// ARRANGE
		using var context = _dbContextFactory.CreateDbContext(AssemblyInitialize.Configuration);
		int expectedCount = context.Products.Count();
		var accessor = AccessorHelpers.GetAccessor<IProductAccessor>();
		var request = new Logic.Products.FindRequest();

		// ACT
		var result = await accessor.Find(request);

		// ASSERT
		Assert.IsNotNull(result);
		Assert.AreEqual(expectedCount, result.Length);
	}

	// Find product by Product Type (assumes all types are in the database)
	[TestMethod]
	[DataRow(Logic.Products.ProductType.Commodity, typeof(Logic.Products.Commodity))]
	[DataRow(Logic.Products.ProductType.SalonProduct, typeof(Logic.Products.SalonProduct))]
	[DataRow(Logic.Products.ProductType.Vehicle, typeof(Logic.Products.Vehicle))]
	public async Task Find_ByProductType_ReturnsExpectedTypes(Logic.Products.ProductType productType, Type expectedType)
	{
		// ARRANGE
		var accessor = AccessorHelpers.GetAccessor<IProductAccessor>();
		var request = new Logic.Products.FindRequest { ProductType = productType };

		// ACT
		var result = await accessor.Find(request);

		// ASSERT
		Assert.IsNotNull(result);
		CollectionAssert.AllItemsAreInstancesOfType(result, expectedType);
	}

	// Find product by ProductId
	[TestMethod]
	public async Task Find_ByProductId_ReturnsExpectedProduct()
	{
		// ARRANGE
		var expectedProduct = FindRandomDatabaseProduct();

		var accessor = AccessorHelpers.GetAccessor<IProductAccessor>();
		var request = new Logic.Products.FindRequest { ProductId = expectedProduct.ProductId };

		// ACT
		var result = await accessor.Find(request);

		// ASSERT
		Assert.IsNotNull(result);
		TestHelper.ValidatePropertyValues(expectedProduct, result[0]);
	}

	// Find product by Non-existing ProductId
	[TestMethod]
	public async Task Find_ByNonExistentProductId_ThrowsNotFoundException()
	{
		// ARRANGE
		var productId = int.MaxValue;

		var accessor = AccessorHelpers.GetAccessor<IProductAccessor>();
		var request = new Logic.Products.FindRequest { ProductId = productId };

		// ACT & ASSERT
		await Assert.ThrowsExceptionAsync<NotFoundException>(async () => await accessor.Find(request));
	}

	// Find product by all request parameters
	[TestMethod]
	public async Task Find_ByAllFindRequestValues_ReturnsExpectedProduct()
	{
		// ARRANGE
		var expectedProduct = FindRandomDatabaseProduct();

		var accessor = AccessorHelpers.GetAccessor<IProductAccessor>();
		var request = new Logic.Products.FindRequest
		{
			CurrentQuantity = expectedProduct.CurrentQuantity,
			ProductId = expectedProduct.ProductId,
			Description = expectedProduct.Description,
			IsDeleted = expectedProduct.IsDeleted,
			Manufacturer = expectedProduct.Manufacturer,
			Name = expectedProduct.Name,
			ProductType = expectedProduct.ProductType,
			ReorderQuantity = expectedProduct.ReorderQuantity,
			SellPrice = expectedProduct.SellPrice,
			StockKeepingUnit = expectedProduct.StockKeepingUnit,
		};

		// ACT
		var result = await accessor.Find(request);

		// ASSERT
		Assert.IsNotNull(result);
		Assert.IsTrue(result.Length == 1);
		TestHelper.ValidatePropertyValues(expectedProduct, result[0]);
	}

	// Find product by invalid criteria
	[TestMethod]
	public async Task Find_InvalidCriteriaWithoutProductId_ReturnsEmptyArray()
	{
		// ARRANGE
		var accessor = AccessorHelpers.GetAccessor<IProductAccessor>();
		var request = new Logic.Products.FindRequest { Manufacturer = Guid.NewGuid().ToString(), ProductType = Logic.Products.ProductType.Unknown };

		// ACT
		var result = await accessor.Find(request);

		// ASSERT
		Assert.IsNotNull(result);
		Assert.AreEqual(0, result.Length);
	}

	// Store new product, verify stored
	[TestMethod]
	public async Task Store_NewProduct_StoresAndReturnsProductId()
	{
		// ARRANGE
		var accessor = AccessorHelpers.GetAccessor<IProductAccessor>();
		var vehicle = DataSeeder.GetVehicles().First().ToLogic();
		vehicle.ProductId = null;

		// ACT
		var result = await accessor.Store(vehicle);

		// ASSERT
		Assert.IsNotNull(result);
		Assert.IsNotNull(result.ProductId);
		TestHelper.ValidatePropertyValues(vehicle, result, "ProductId");
		// Validate the product was successfully stored.
		using var context = _dbContextFactory.CreateDbContext(AssemblyInitialize.Configuration);
		var storedProduct = context.Products.FirstOrDefault(x => x.ProductId == result.ProductId.Value).ToLogic();
		TestHelper.ValidatePropertyValues(result, storedProduct);
	}

	// Store (update) existing product, verify stored
	[TestMethod]
	public async Task Store_ExistingChangedProduct_StoresSuccessfully()
	{
		// ARRANGE
		var existingProduct = FindRandomDatabaseProduct();
		existingProduct.CurrentQuantity = new Random(DateTime.Now.Millisecond).Next();
		existingProduct.ReorderQuantity = new Random(DateTime.Now.Millisecond).Next();
		existingProduct.Description = $"Modified Description-{new Random(DateTime.Now.Millisecond).Next()}";
		var accessor = AccessorHelpers.GetAccessor<IProductAccessor>();

		// ACT
		var result = await accessor.Store(existingProduct);

		// ASSERT
		Assert.IsNotNull(result);
		TestHelper.ValidatePropertyValues(existingProduct, result);
		// Validate the product was successfully stored.
		using var context = _dbContextFactory.CreateDbContext(AssemblyInitialize.Configuration);
		var storedProduct = context.Products.Find(result.ProductId.Value).ToLogic();
		TestHelper.ValidatePropertyValues(result, storedProduct);
	}

	// Remove existing product, verify removed
	[TestMethod]
	public async Task Remove_ExistingProduct_IsVerifiablyRemoved()
	{
		// ARRANGE
		var randomProduct = FindRandomDatabaseProduct();
		var accessor = AccessorHelpers.GetAccessor<IProductAccessor>();

		// ACT
		await accessor.Remove(randomProduct.ProductId.Value);

		// ASSERT
		using var context = _dbContextFactory.CreateDbContext(AssemblyInitialize.Configuration);
		var model = context.Products.Find(randomProduct.ProductId.Value);
		Assert.IsNull(model);

	}

	// Remove non-existing product should throw NotFoundException, verify exception
	[TestMethod]
	public async Task Remove_NonExistingProduct_ThrowsNotFoundException()
	{
		// ARRANGE
		using var context = _dbContextFactory.CreateDbContext(AssemblyInitialize.Configuration);
		var lastProduct = context.Products.OrderByDescending(x => x.ProductId).First();
		int nonExistentProductId = lastProduct.ProductId + 1000;
		Assert.IsNull(context.Products.Find(nonExistentProductId));
		var accessor = AccessorHelpers.GetAccessor<IProductAccessor>();

		// ACT / ASSERT
		await Assert.ThrowsExceptionAsync<NotFoundException>(async () => await accessor.Remove(nonExistentProductId));
	}

	private static Logic.Products.ProductBase FindRandomDatabaseProduct()
	{
		using var context = _dbContextFactory.CreateDbContext(AssemblyInitialize.Configuration);
		return TestHelper.GetRandomElement(context.Products).ToLogic();
	}
}

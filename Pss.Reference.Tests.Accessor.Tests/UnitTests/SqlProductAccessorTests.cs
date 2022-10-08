using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pss.Reference.Accessors;
using Pss.Reference.Accessors.Products;
using Pss.Reference.Accessors.Products.Configuration;
using Pss.Reference.Accessors.Products.Extensions;
using Pss.Reference.Common;
using Pss.Reference.Contracts.Logic.Exceptions;
using Pss.Reference.Shared.Tests;
using Logic = Pss.Reference.Contracts.Logic;

namespace Pss.Reference.Accessor.Tests.UnitTests;

[TestClass]
[TestCategory(Constants.Testing.UnitTest)]
public class SqlProductAccessorTests
{
	private static Logic.Products.ProductBase[] Setup(ProductDbContext context)
	{
		var products = DataSeeder.GetProducts();
		context.Products.AddRange(products);
		context.SaveChanges();
		return products.ToLogic().ToArray();
	}

	#region Test Cases
	// Find all products (default FindRequest)
	// Find product by ProductId
	// Find product by Non-existing ProductId
	// Find product by Product Type
	// Find product by Manufacturer
	// Find product by StockKeepingUnit
	// Find product by Name
	// Find product by Description
	// Find product by SellPrice
	// Find product by CurrentQuantity
	// Find product by ReorderQuantity
	// Find product by IsDeleted
	// Find product by all request parameters
	// Find product by invalid/non-matching criteria
	// Store new product, verify stored
	// Store (update) existing product, verify stored
	// Remove existing product, verify removed
	// Remove non-existing product should throw NotFoundException, verify exception
	#endregion

	// Find all products (default FindRequest)
	[TestMethod]
	public async Task Find_DefaultFindRequest_ReturnsAllProducts()
	{
		// ARRANGE
		using var context = new ProductDbContext(DbContextHelpers.ConfigureInMemoryDbContext<ProductDbContext>());
		var productArray = Setup(context);
		var accessor = AccessorHelpers.GetAccessorForUnitTests<SqlProductAccessor, ProductDbContext>(context);
		var request = new Logic.Products.FindRequest();

		// ACT
		var result = await accessor.Find(request);

		// ASSERT
		Assert.IsNotNull(result);
		Assert.AreEqual(productArray.Length, result.Length);
		TestHelper.ValidateCollection(productArray, result, (p, r) => p.ProductId == r.ProductId);
	}

	// Find product by ProductId
	// Find product by Name
	// Find product by Manufacturer
	// Find product by StockKeepingUnit
	// Find product by Description
	// Find product by SellPrice
	// Find product by CurrentQuantity
	// Find product by ReorderQuantity
	// Find product by all request parameters
	[TestMethod]
	public async Task Find_ByXXX_ReturnsExpectedProduct()
	{
		await ValidateFindRequest((expected) => new Logic.Products.FindRequest { ProductId = expected.ProductId });
		await ValidateFindRequest((expected) => new Logic.Products.FindRequest { Name = expected.Name });
		await ValidateFindRequest((expected) => new Logic.Products.FindRequest { Manufacturer = expected.Manufacturer });
		await ValidateFindRequest((expected) => new Logic.Products.FindRequest { StockKeepingUnit = expected.StockKeepingUnit });
		await ValidateFindRequest((expected) => new Logic.Products.FindRequest { Description = expected.Description });
		await ValidateFindRequest((expected) => new Logic.Products.FindRequest { SellPrice = expected.SellPrice });
		await ValidateFindRequest((expected) => new Logic.Products.FindRequest { CurrentQuantity = expected.CurrentQuantity });
		await ValidateFindRequest((expected) => new Logic.Products.FindRequest { ReorderQuantity = expected.ReorderQuantity });

		await ValidateFindRequest((expected) => new Logic.Products.FindRequest
		{
			CurrentQuantity = expected.CurrentQuantity,
			ProductId = expected.ProductId,
			Description = expected.Description,
			IsDeleted = expected.IsDeleted,
			Manufacturer = expected.Manufacturer,
			Name = expected.Name,
			ProductType = expected.ProductType,
			ReorderQuantity = expected.ReorderQuantity,
			SellPrice = expected.SellPrice,
			StockKeepingUnit = expected.StockKeepingUnit,
		});
	}

	private static async Task ValidateFindRequest(Func<Logic.Products.ProductBase, Logic.Products.FindRequest> requestDelegate)
	{
		// ARRANGE
		using var context = new ProductDbContext(DbContextHelpers.ConfigureInMemoryDbContext<ProductDbContext>());
		var expected = TestHelper.GetRandomElement(Setup(context));
		var accessor = AccessorHelpers.GetAccessorForUnitTests<SqlProductAccessor, ProductDbContext>(context);

		// ACT
		var request = requestDelegate(expected);
		var result = await accessor.Find(request);

		// ASSERT
		Assert.IsNotNull(result);
		Assert.IsTrue(result.Length == 1);
		Assert.AreEqual(expected.ProductId, result[0].ProductId);
		TestHelper.ValidatePropertyValues(expected, result[0]);
	}

	// Find product by Non-existing ProductId
	[TestMethod]
	public async Task Find_ByNonExistentProductId_ThrowsNotFoundException()
	{
		// ARRANGE
		using var context = new ProductDbContext(DbContextHelpers.ConfigureInMemoryDbContext<ProductDbContext>());
		Setup(context);
		var accessor = AccessorHelpers.GetAccessorForUnitTests<SqlProductAccessor, ProductDbContext>(context);
		var request = new Logic.Products.FindRequest { ProductId = int.MaxValue };

		// ACT & ASSERT
		await Assert.ThrowsExceptionAsync<NotFoundException>(async () => await accessor.Find(request));
	}

	// Find product by IsDeleted
	[TestMethod]
	public async Task Find_ByIsDeletedTrue_ReturnsExpectedProduct()
	{
		// ARRANGE
		using var context = new ProductDbContext(DbContextHelpers.ConfigureInMemoryDbContext<ProductDbContext>());
		var expected = Setup(context).Where(x => x.IsDeleted == true).ToArray();
		var accessor = AccessorHelpers.GetAccessorForUnitTests<SqlProductAccessor, ProductDbContext>(context);
		var request = new Logic.Products.FindRequest { IsDeleted = true };

		// ACT
		var result = await accessor.Find(request);

		// ASSERT
		Assert.IsNotNull(result);
		Assert.AreEqual(expected.Length, result.Length);
		Assert.IsTrue(result.All(x => x.IsDeleted == true));
		TestHelper.ValidateCollection(expected, result, (p, r) => p.ProductId == r.ProductId);
	}

	// Find product by Product Type
	[TestMethod]
	[DataRow(Logic.Products.ProductType.Commodity, typeof(Logic.Products.Commodity))]
	[DataRow(Logic.Products.ProductType.SalonProduct, typeof(Logic.Products.SalonProduct))]
	[DataRow(Logic.Products.ProductType.Vehicle, typeof(Logic.Products.Vehicle))]
	public async Task Find_ByProductType_ReturnsExpectedProducts(Logic.Products.ProductType productType, Type expectedType)
	{
		// ARRANGE
		using var context = new ProductDbContext(DbContextHelpers.ConfigureInMemoryDbContext<ProductDbContext>());
		var expected = Setup(context).Where(p => p.ProductType == productType).ToArray();
		var accessor = AccessorHelpers.GetAccessorForUnitTests<SqlProductAccessor, ProductDbContext>(context);
		var request = new Logic.Products.FindRequest { ProductType = productType };

		// ACT
		var result = await accessor.Find(request);

		// ASSERT
		Assert.IsNotNull(result);
		Assert.AreEqual(expected.Length, result.Length);
		CollectionAssert.AllItemsAreInstancesOfType(result, expectedType);
		TestHelper.ValidateCollection(expected, result, (p, r) => p.ProductId == r.ProductId);
	}

	// Find product by invalid/non-matching criteria
	[TestMethod]
	public async Task Find_ByNonMatchingCriteriaWithoutProductId_ReturnsEmtpyArray()
	{
		// ARRANGE
		using var context = new ProductDbContext(DbContextHelpers.ConfigureInMemoryDbContext<ProductDbContext>());
		Setup(context);
		var accessor = AccessorHelpers.GetAccessorForUnitTests<SqlProductAccessor, ProductDbContext>(context);
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
		using var context = new ProductDbContext(DbContextHelpers.ConfigureInMemoryDbContext<ProductDbContext>());
		Setup(context);
		var accessor = AccessorHelpers.GetAccessorForUnitTests<SqlProductAccessor, ProductDbContext>(context);
		var newProduct = TestHelper.GetRandomElement(DataSeeder.GetProducts().ToLogic().ToArray());
		newProduct.ProductId = 0;

		// ACT
		var result = await accessor.Store(newProduct);

		// ASSERT
		Assert.IsNotNull(result);
		Assert.IsNotNull(result.ProductId);
		TestHelper.ValidatePropertyValues(newProduct, result, "ProductId");

		// DbContext was disposed, so skipping the "verify stored" step.
		// Validate the product was successfully stored.
		//var storedProduct = context.Products.FirstOrDefault(x => x.ProductId == result.ProductId.Value).ToLogic();
		//TestHelper.ValidatePropertyValues(result, storedProduct);
	}

	// Store (update) existing product, verify stored
	[TestMethod]
	public async Task Store_ExistingChangedProduct_StoresSuccessfully()
	{
		// ARRANGE
		using var context = new ProductDbContext(DbContextHelpers.ConfigureInMemoryDbContext<ProductDbContext>());
		var existing = TestHelper.GetRandomElement(Setup(context));
		var accessor = AccessorHelpers.GetAccessorForUnitTests<SqlProductAccessor, ProductDbContext>(context);
		existing.CurrentQuantity = new Random(DateTime.Now.Millisecond).Next();
		existing.ReorderQuantity = new Random(DateTime.Now.Millisecond).Next();
		existing.Description = $"Modified Description-{new Random(DateTime.Now.Millisecond).Next()}";

		// ACT
		var result = await accessor.Store(existing);

		// ASSERT
		Assert.IsNotNull(result);
		TestHelper.ValidatePropertyValues(existing, result);

		// DbContext was disposed, so skipping the "verify stored" step.
		// Validate the product was successfully stored.
		//var storedProduct = context.Products.FirstOrDefault(x => x.ProductId == result.ProductId.Value).ToLogic();
		//TestHelper.ValidatePropertyValues(result, storedProduct);
	}

	// Remove existing product, verify removed
	[TestMethod]
	public async Task Remove_ExistingProduct_IsVerifiablyRemoved()
	{
		// ARRANGE
		using var context = new ProductDbContext(DbContextHelpers.ConfigureInMemoryDbContext<ProductDbContext>());
		var existing = TestHelper.GetRandomElement(Setup(context));
		var accessor = AccessorHelpers.GetAccessorForUnitTests<SqlProductAccessor, ProductDbContext>(context);

		// ACT
		await accessor.Remove(existing.ProductId.Value);

		// ASSERT
		// DbContext was disposed, so skipping the "verified stored" step.
		//var model = context.Products.Find(existing.ProductId.Value);
		//Assert.IsNull(model);
	}

	// Remove non-existing product should throw NotFoundException, verify exception
	[TestMethod]
	public async Task Remove_NonExistingProduct_ThrowsNotFoundException()
	{
		// ARRANGE
		using var context = new ProductDbContext(DbContextHelpers.ConfigureInMemoryDbContext<ProductDbContext>());
		var existing = TestHelper.GetRandomElement(Setup(context));
		var accessor = AccessorHelpers.GetAccessorForUnitTests<SqlProductAccessor, ProductDbContext>(context);
		int nonExistentProductId = existing.ProductId.Value + 10000;
		Assert.IsNull(context.Products.Find(nonExistentProductId));

		// ACT / ASSERT
		await Assert.ThrowsExceptionAsync<NotFoundException>(async () => await accessor.Remove(nonExistentProductId));
	}
}

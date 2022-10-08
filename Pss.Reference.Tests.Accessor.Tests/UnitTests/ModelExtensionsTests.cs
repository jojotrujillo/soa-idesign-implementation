using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pss.Reference.Accessors.Products.Extensions;
using Pss.Reference.Common;
using Pss.Reference.Shared.Tests;
using Logic = Pss.Reference.Contracts.Logic.Products;
using Models = Pss.Reference.Accessors.Products.Models;

namespace Pss.Reference.Accessor.Tests.UnitTests;

[TestClass]
[TestCategory(Constants.Testing.UnitTest)]
[TestCategory("Accessor")]
public class ModelExtensionsTests
{
	[TestMethod]
	public void ToLogic_WithNullProductCollection_ReturnsEmptyArray()
	{
		// ARRANGE
		IEnumerable<Models.Product> entities = null;

		// ACT
		var result = entities.ToLogic();

		// ASSERT
		Assert.IsNotNull(result);
		Assert.IsTrue(result.Any() == false);
	}

	[TestMethod]
	public void ToLogic_WithEmptyProductArray_ReturnsEmptyArray()
	{
		// ARRANGE
		IEnumerable<Models.Product> entities = Enumerable.Empty<Models.Product>();

		// ACT
		var result = entities.ToLogic();

		// ASSERT
		Assert.IsNotNull(result);
		Assert.IsTrue(result.Any() == false);
	}

	[TestMethod]
	public void ToModels_WithProductArray_ReturnsExpectedModelArray()
	{
		// ARRANGE
		var models = ModelHelpers.GetRandomModels().ToArray();

		// ACT
		var results = models.ToLogic().ToArray();

		// ASSERT
		Assert.IsNotNull(results);
		Assert.AreEqual(models.Length, results.Length);
		TestHelper.ValidateCollection(models, results, (m, r) => m.ProductId == r.ProductId.Value, null, "ProductId", "ProductJson");
	}

	[TestMethod]
	public void ToLogic_WithNullEntity_ReturnsNull()
	{
		// ARRANGE
		Models.Product entity = null;

		// ACT
		var result = entity.ToLogic();

		// ASSERT
		Assert.IsNull(result);
	}

	[TestMethod]
	public void ToLogic_WithProductTypeCommodity_ReturnsCommodity()
	{
		// ARRANGE
		var model = ModelHelpers.GetRandomModels().First(m => m.ProductType == Logic.ProductType.Commodity);

		// ACT
		var result = model.ToLogic();

		// ASSERT
		Assert.IsNotNull(result);
		Assert.IsTrue(result.GetType() == typeof(Logic.Commodity));
		TestHelper.ValidatePropertyValues(model, result, "ProductId", "ProductJson");
	}

	[TestMethod]
	public void ToLogic_WithProductTypeSalonProduct_ReturnsSalonProduct()
	{
		// ARRANGE
		var model = ModelHelpers.GetRandomModels().First(m => m.ProductType == Logic.ProductType.SalonProduct);

		// ACT
		var result = model.ToLogic();

		// ASSERT
		Assert.IsNotNull(result);
		Assert.IsTrue(result.GetType() == typeof(Logic.SalonProduct));
		TestHelper.ValidatePropertyValues(model, result, "ProductId", "ProductJson");
	}

	[TestMethod]
	public void ToLogic_WithProductTypeVehicle_ReturnsVehicle()
	{
		// ARRANGE
		var model = ModelHelpers.GetRandomModels().First(m => m.ProductType == Logic.ProductType.Vehicle);

		// ACT
		var result = model.ToLogic();

		// ASSERT
		Assert.IsNotNull(result);
		Assert.IsTrue(result.GetType() == typeof(Logic.Vehicle));
		TestHelper.ValidatePropertyValues(model, result, "ProductId", "ProductJson");
	}

	[TestMethod]
	public void ToLogic_WithProductTypeUnknown_ThrowsExepectedException()
	{
		// ARRANGE
		var model = new Models.Product { ProductType = Logic.ProductType.Unknown };

		// ACT & ASSERT
		Assert.ThrowsException<InvalidOperationException>(() => model.ToLogic());
	}
}

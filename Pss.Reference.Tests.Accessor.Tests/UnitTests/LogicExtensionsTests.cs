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
public class LogicExtensionsTests
{
	[TestMethod]
	public void ToModels_WithNullProductCollection_ReturnsEmptyArray()
	{
		// ARRANGE
		IEnumerable<Logic.ProductBase> entities = null;

		// ACT
		var result = entities.ToModels();

		// ASSERT
		Assert.IsNotNull(result);
		Assert.IsTrue(result.Any() == false);
	}

	[TestMethod]
	public void ToModels_WithEmptyProductArray_ReturnsEmptyArray()
	{
		// ARRANGE
		IEnumerable<Logic.ProductBase> entities = Enumerable.Empty<Logic.ProductBase>();

		// ACT
		var result = entities.ToModels();

		// ASSERT
		Assert.IsNotNull(result);
		Assert.IsTrue(result.Any() == false);
	}

	[TestMethod]
	public void ToModels_WithProductArray_ReturnsExpectedModelArray()
	{
		// ARRANGE
		var models = ModelHelpers.GetRandomModels().ToArray();
		var entityList = new List<Logic.ProductBase>(models.Length);

		foreach (var model in models)
			entityList.Add(model.ToLogic());

		var entities = entityList.ToArray();

		// ACT
		var results = entities.ToModels().ToArray();

		// ASSERT
		Assert.IsNotNull(results);
		Assert.AreEqual(models.Length, results.Length);
		TestHelper.ValidateCollection(models, results, (m, r) => m.ProductId == r.ProductId);
	}

	[TestMethod]
	public void ToModel_WithNullEntity_ReturnsNull()
	{
		// ARRANGE
		Logic.ProductBase entity = null;

		// ACT
		var result = entity.ToModel();

		// ASSERT
		Assert.IsNull(result);
	}

	[TestMethod]
	public void ToModel_WithValidProductAndNullModel_ReturnsExpectedModel()
	{
		// ARRANGE
		var model = ModelHelpers.GetRandomModel();
		var entity = model.ToLogic();

		// ACT
		var result = entity.ToModel();

		// ASSERT
		Assert.IsNotNull(result);
		TestHelper.ValidatePropertyValues(model, result);
	}

	[TestMethod]
	public void ToModel_WithValidProductAndValidModel_ReturnsExpectedModel()
	{
		// ARRANGE
		Models.Product newModel = new();
		var model = ModelHelpers.GetRandomModel();
		var entity = model.ToLogic();

		// ACT
		var result = entity.ToModel(newModel);

		// ASSERT
		Assert.IsNotNull(result);
		TestHelper.ValidatePropertyValues(model, result);
	}
}

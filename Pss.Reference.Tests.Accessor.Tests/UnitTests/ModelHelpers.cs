using Pss.Reference.Accessors.Products.Configuration;
using Pss.Reference.Shared.Tests;
using Models = Pss.Reference.Accessors.Products.Models;

namespace Pss.Reference.Accessor.Tests.UnitTests;

internal static class ModelHelpers
{
	public static Models.Product GetRandomModel()
	{
		var products = DataSeeder.GetProducts();
		return TestHelper.GetRandomElement(products);
	}

	public static IEnumerable<Models.Product> GetRandomModels()
	{
		var products = DataSeeder.GetProducts();
		var models = new List<Models.Product>();
		foreach (var product in products)
			models.Add(product);

		return models;
	}
}

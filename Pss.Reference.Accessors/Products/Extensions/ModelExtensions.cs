using Pss.Reference.Common.Extensions;
using Logic = Pss.Reference.Contracts.Logic.Products;

namespace Pss.Reference.Accessors.Products.Extensions;

internal static class ModelExtensions
{
	public static IEnumerable<Logic.ProductBase> ToLogic(this IEnumerable<Models.Product> entities)
	{
		if (entities == null)
			return Enumerable.Empty<Logic.ProductBase>();

		return entities.Select(c => c.ToLogic());
	}

	public static Logic.ProductBase ToLogic(this Models.Product entity)
	{
		if (entity == null)
			return null;

		Logic.ProductBase result = entity.ProductType switch
		{
			Logic.ProductType.Commodity => entity.ProductJson.FromJson<Logic.Commodity>(),
			Logic.ProductType.SalonProduct => entity.ProductJson.FromJson<Logic.SalonProduct>(),
			Logic.ProductType.Vehicle => entity.ProductJson.FromJson<Logic.Vehicle>(),
			_ => throw new InvalidOperationException($"Unknown product type {entity.ProductType}")
		};

		result.ProductId = entity.ProductId;
		return result;
	}
}

using Pss.Reference.Common.Extensions;
using Logic = Pss.Reference.Contracts.Logic.Products;

namespace Pss.Reference.Accessors.Products.Extensions;

internal static class LogicExtensions
{
	public static IEnumerable<Models.Product> ToModels(this IEnumerable<Logic.ProductBase> entities)
	{
		if (entities == null)
			return Enumerable.Empty<Models.Product>();

		return entities.Select(e => e.ToModel());
	}

	public static Models.Product ToModel(this Logic.ProductBase entity, Models.Product model = null)
	{
		if (entity == null)
			return null;

		model ??= new();

		model.CurrentQuantity = entity.CurrentQuantity;
		model.Description = entity.Description;
		model.IsDeleted = entity.IsDeleted;
		model.Manufacturer = entity.Manufacturer;
		model.Name = entity.Name;
		model.ProductId = entity.ProductId.GetValueOrDefault();
		model.ProductType = entity.ProductType;
		model.ReorderQuantity = entity.ReorderQuantity;
		model.SellPrice = entity.SellPrice;
		model.StockKeepingUnit = entity.StockKeepingUnit;
		model.ProductJson = entity.ToJson();

		return model;
	}
}

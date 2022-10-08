using Client = Pss.Reference.Contracts.Client.Products;
using Logic = Pss.Reference.Contracts.Logic.Products;

namespace Pss.Reference.Managers.Extensions;

internal static class LogicProductExtensions
{
	private static readonly Dictionary<Type, Func<Logic.ProductBase, Client.ProductBase>> _mappingStrategy = new()
	{
		{ typeof(Logic.Commodity), (entity) => ToCommodity((Logic.Commodity)entity) },
		{ typeof(Logic.SalonProduct), (entity) => ToSalonProduct((Logic.SalonProduct)entity) },
		{ typeof(Logic.Vehicle), (entity) => ToVehicle((Logic.Vehicle)entity) }
	};

	public static Client.ProductBase[] ToClient(this Logic.ProductBase[] entities)
	{
		if (entities == null)
			return null;

		return entities.Select(e => e.ToClient()).ToArray();
	}

	public static Client.ProductBase ToClient(this Logic.ProductBase entity)
	{
		if (entity == null)
			return null;

		var type = entity.GetType();

		if (!_mappingStrategy.ContainsKey(type))
			throw new ArgumentException($"Type '{type.Name}' was not found in the mapping strategy.");

		return _mappingStrategy[type].Invoke(entity);
	}

	public static Client.ProductBase ToCommodity(this Logic.Commodity entity)
	{
		if (entity == null)
			return null;

		return new Client.Commodity
		{
			CurrentQuantity = entity.CurrentQuantity,
			Description = entity.Description,
			IsDeleted = entity.IsDeleted,
			Manufacturer = entity.Manufacturer,
			Name = entity.Name,
			ProductId = entity.ProductId,
			ProductType = (Client.ProductType)entity.ProductType,
			ReorderQuantity = entity.ReorderQuantity,
			SellPrice = entity.SellPrice,
			StockKeepingUnit = entity.StockKeepingUnit
		};
	}

	public static Client.ProductBase ToSalonProduct(this Logic.SalonProduct entity)
	{
		if (entity == null)
			return null;

		return new Client.SalonProduct
		{
			CurrentQuantity = entity.CurrentQuantity,
			Description = entity.Description,
			IsDeleted = entity.IsDeleted,
			Manufacturer = entity.Manufacturer,
			Name = entity.Name,
			ProductId = entity.ProductId,
			ProductType = (Client.ProductType)entity.ProductType,
			ReorderQuantity = entity.ReorderQuantity,
			SellPrice = entity.SellPrice,
			StockKeepingUnit = entity.StockKeepingUnit,
			Vendor = entity.Vendor
		};
	}

	public static Client.ProductBase ToVehicle(this Logic.Vehicle entity)
	{
		if (entity == null)
			return null;

		return new Client.Vehicle
		{
			CurrentQuantity = entity.CurrentQuantity,
			Description = entity.Description,
			IsDeleted = entity.IsDeleted,
			Manufacturer = entity.Manufacturer,
			Name = entity.Name,
			ProductId = entity.ProductId,
			ProductType = (Client.ProductType)entity.ProductType,
			ReorderQuantity = entity.ReorderQuantity,
			SellPrice = entity.SellPrice,
			StockKeepingUnit = entity.StockKeepingUnit,
			LicenseNumber = entity.LicenseNumber,
			Make = entity.Make,
			ManufactureYear = entity.ManufactureYear,
			Model = entity.Model,
			VehicleIdentificationNumber = entity.VehicleIdentificationNumber
		};
	}
}

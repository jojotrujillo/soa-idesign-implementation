using Client = Pss.Reference.Contracts.Client.Products;
using Logic = Pss.Reference.Contracts.Logic.Products;

namespace Pss.Reference.Managers.Extensions;

internal static class ClientProductExtensions
{
	private static readonly Dictionary<Type, Func<Client.ProductBase, Logic.ProductBase>> _mappingStrategy = new()
	{
		{ typeof(Client.Commodity), (entity) => ToCommodity((Client.Commodity)entity) },
		{ typeof(Client.SalonProduct), (entity) => ToSalonProduct((Client.SalonProduct)entity) },
		{ typeof(Client.Vehicle), (entity) => ToVehicle((Client.Vehicle)entity) }
	};

	public static Logic.ProductBase[] ToLogic(this Client.ProductBase[] entities)
	{
		if (entities == null)
			return null;

		return entities.Select(e => e.ToLogic()).ToArray();
	}

	public static Logic.ProductBase ToLogic(this Client.ProductBase entity)
	{
		if (entity == null)
			return null;

		var type = entity.GetType();

		if (!_mappingStrategy.ContainsKey(type))
			throw new ArgumentException($"Type '{type.Name}' was not found in the mapping strategy.");

		return _mappingStrategy[type].Invoke(entity);
	}

	public static Logic.ProductBase ToCommodity(this Client.Commodity entity)
	{
		if (entity == null)
			return null;

		return new Logic.Commodity
		{
			CurrentQuantity = entity.CurrentQuantity,
			Description = entity.Description,
			IsDeleted = entity.IsDeleted,
			Manufacturer = entity.Manufacturer,
			Name = entity.Name,
			ProductId = entity.ProductId,
			ProductType = (Logic.ProductType)entity.ProductType,
			ReorderQuantity = entity.ReorderQuantity,
			SellPrice = entity.SellPrice,
			StockKeepingUnit = entity.StockKeepingUnit
		};
	}

	public static Logic.ProductBase ToSalonProduct(this Client.SalonProduct entity)
	{
		if (entity == null)
			return null;

		return new Logic.SalonProduct
		{
			CurrentQuantity = entity.CurrentQuantity,
			Description = entity.Description,
			IsDeleted = entity.IsDeleted,
			Manufacturer = entity.Manufacturer,
			Name = entity.Name,
			ProductId = entity.ProductId,
			ProductType = (Logic.ProductType)entity.ProductType,
			ReorderQuantity = entity.ReorderQuantity,
			SellPrice = entity.SellPrice,
			StockKeepingUnit = entity.StockKeepingUnit,
			Vendor = entity.Vendor
		};
	}

	public static Logic.ProductBase ToVehicle(this Client.Vehicle entity)
	{
		if (entity == null)
			return null;

		return new Logic.Vehicle
		{
			CurrentQuantity = entity.CurrentQuantity,
			Description = entity.Description,
			IsDeleted = entity.IsDeleted,
			Manufacturer = entity.Manufacturer,
			Name = entity.Name,
			ProductId = entity.ProductId,
			ProductType = (Logic.ProductType)entity.ProductType,
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

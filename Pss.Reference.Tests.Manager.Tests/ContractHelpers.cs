using AutoFixture;
using Client = Pss.Reference.Contracts.Client.Products;
using Logic = Pss.Reference.Contracts.Logic.Products;

namespace Pss.Reference.Manager.Tests;

internal static class ContractHelpers
{
	private static readonly IFixture _fixture = new Fixture();
	private const int EntityCount = 10;

	private static int RandomInteger => new Random().Next();

	public static Client.ProductBase[] GetClientProducts()
	{
		var products = new List<Client.ProductBase>(3 * EntityCount);

		for (int i = 0; i < EntityCount; i++)
		{
			var commodity = _fixture.Build<Client.Commodity>()
				.With(c => c.ProductType, Client.ProductType.Commodity)
				.With(c => c.Name, $"Name-{RandomInteger}")
				.With(c => c.Description, $"Description-{RandomInteger}")
				.With(c => c.Manufacturer, $"Manufacturer-{RandomInteger}")
				.With(c => c.StockKeepingUnit, $"SKU-{RandomInteger}")
				.Create();

			products.Add(commodity);
		}

		for (int i = 0; i < EntityCount; i++)
		{
			var salonProduct = _fixture.Build<Client.SalonProduct>()
				.With(c => c.ProductType, Client.ProductType.SalonProduct)
				.With(c => c.Vendor, $"Vendor-{RandomInteger}")
				.With(c => c.Name, $"Name-{RandomInteger}")
				.With(c => c.Description, $"Description-{RandomInteger}")
				.With(c => c.Manufacturer, $"Manufacturer-{RandomInteger}")
				.With(c => c.StockKeepingUnit, $"SKU-{RandomInteger}")
				.Create();

			products.Add(salonProduct);
		}

		for (int i = 0; i < EntityCount; i++)
		{
			var vehicle = _fixture.Build<Client.Vehicle>()
				.With(c => c.ProductType, Client.ProductType.Vehicle)
				.With(c => c.VehicleIdentificationNumber, $"VIN-{RandomInteger}")
				.With(c => c.Make, $"Make-{RandomInteger}")
				.With(c => c.Model, $"Model-{RandomInteger}")
				.With(c => c.LicenseNumber, $"License-{RandomInteger}")
				.With(c => c.Name, $"Name-{RandomInteger}")
				.With(c => c.Description, $"Description-{RandomInteger}")
				.With(c => c.Manufacturer, $"Manufacturer-{RandomInteger}")
				.With(c => c.StockKeepingUnit, $"SKU-{RandomInteger}")
				.Create();

			products.Add(vehicle);
		}

		return products.ToArray();
	}

	public static Logic.ProductBase[] GetLogicProducts()
	{
		var products = new List<Logic.ProductBase>(3 * EntityCount);

		for (int i = 0; i < EntityCount; i++)
		{
			var commodity = _fixture.Build<Logic.Commodity>()
				.With(c => c.ProductType, Logic.ProductType.Commodity)
				.With(c => c.Name, $"Name-{RandomInteger}")
				.With(c => c.Description, $"Description-{RandomInteger}")
				.With(c => c.Manufacturer, $"Manufacturer-{RandomInteger}")
				.With(c => c.StockKeepingUnit, $"SKU-{RandomInteger}")
				.Create();

			products.Add(commodity);
		}

		for (int i = 0; i < EntityCount; i++)
		{
			var salonProduct = _fixture.Build<Logic.SalonProduct>()
				.With(c => c.ProductType, Logic.ProductType.SalonProduct)
				.With(c => c.Vendor, $"Vendor-{RandomInteger}")
				.With(c => c.Name, $"Name-{RandomInteger}")
				.With(c => c.Description, $"Description-{RandomInteger}")
				.With(c => c.Manufacturer, $"Manufacturer-{RandomInteger}")
				.With(c => c.StockKeepingUnit, $"SKU-{RandomInteger}")
				.Create();

			products.Add(salonProduct);
		}

		for (int i = 0; i < EntityCount; i++)
		{
			var vehicle = _fixture.Build<Logic.Vehicle>()
				.With(c => c.ProductType, Logic.ProductType.Vehicle)
				.With(c => c.VehicleIdentificationNumber, $"VIN-{RandomInteger}")
				.With(c => c.Make, $"Make-{RandomInteger}")
				.With(c => c.Model, $"Model-{RandomInteger}")
				.With(c => c.LicenseNumber, $"License-{RandomInteger}")
				.With(c => c.Name, $"Name-{RandomInteger}")
				.With(c => c.Description, $"Description-{RandomInteger}")
				.With(c => c.Manufacturer, $"Manufacturer-{RandomInteger}")
				.With(c => c.StockKeepingUnit, $"SKU-{RandomInteger}")
				.Create();

			products.Add(vehicle);
		}

		return products.ToArray();
	}
}

using AutoFixture;
using Pss.Reference.Accessors.Products.Extensions;
using Logic = Pss.Reference.Contracts.Logic.Products;

namespace Pss.Reference.Accessors.Products.Configuration;

internal static class DataSeeder
{
	private static readonly IFixture _fixture = new Fixture();
	private const int EntityCount = 10;

	public static Models.Product[] GetProducts()
	{
		return GetCommodities().Union(GetSalonProducts()).Union(GetVehicles()).ToArray();
	}

	private static int RandomInteger(int maxValue=99999)
	{
		var random = new Random(DateTime.Now.Millisecond);
		return random.Next(1, maxValue);
	}

	public static IEnumerable<Models.Product> GetCommodities()
	{
		var commodities = new List<Models.Product>(EntityCount);

		for (int i = 0; i < EntityCount; i++)
		{
			var commodity = _fixture.Build<Logic.Commodity>()
				.With(c => c.ProductType, Logic.ProductType.Commodity)
				.With(c => c.Name, $"Name-{RandomInteger()}")
				.With(c => c.Description, $"Description-{RandomInteger()}")
				.With(c => c.Manufacturer, $"X-Manufacturer-{RandomInteger()}")
				.With(c => c.StockKeepingUnit, $"X-SKU-{RandomInteger()}")
				.Create();

			commodities.Add(commodity.ToModel());
		}

		return commodities;
	}

	public static IEnumerable<Models.Product> GetSalonProducts()
	{
		var salonProducts = new List<Models.Product>(EntityCount);

		for (int i = 0; i < EntityCount; i++)
		{
			var salonProduct = _fixture.Build<Logic.SalonProduct>()
				.With(c => c.ProductType, Logic.ProductType.SalonProduct)
				.With(c => c.Vendor, $"Vendor-{RandomInteger()}")
				.With(c => c.Name, $"Name-{RandomInteger()}")
				.With(c => c.Description, $"Description-{RandomInteger()}")
				.With(c => c.Manufacturer, $"X-Manufacturer-{RandomInteger()}")
				.With(c => c.StockKeepingUnit, $"X-SKU-{RandomInteger()}")
				.Create();

			salonProducts.Add(salonProduct.ToModel());
		}

		return salonProducts;
	}

	public static IEnumerable<Models.Product> GetVehicles()
	{
		var vehicles = new List<Models.Product>(EntityCount);

		for (int i = 0; i < EntityCount; i++)
		{
			var vehicle = _fixture.Build<Logic.Vehicle>()
				.With(c => c.ProductType, Logic.ProductType.Vehicle)
				.With(c => c.VehicleIdentificationNumber, $"VIN-{RandomInteger()}")
				.With(c => c.Make, $"Make-{RandomInteger()}")
				.With(c => c.Model, $"Model-{RandomInteger()}")
				.With(c => c.LicenseNumber, $"L-{RandomInteger(9999)}")
				.With(c => c.Name, $"Name-{RandomInteger()}")
				.With(c => c.Description, $"Description-{RandomInteger()}")
				.With(c => c.Manufacturer, $"X-Manufacturer-{RandomInteger()}")
				.With(c => c.StockKeepingUnit, $"X-SKU-{RandomInteger()}")
				.Create();

			vehicles.Add(vehicle.ToModel());
		}

		return vehicles;
	}
}

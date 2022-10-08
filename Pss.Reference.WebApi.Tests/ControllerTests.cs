using System.Net;
using System.Net.Http.Json;
using AutoFixture;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pss.Reference.Common;
using Pss.Reference.Common.Extensions;
using Pss.Reference.WebApi.Controllers;
using Client = Pss.Reference.Contracts.Client;

namespace Pss.Reference.WebApi.Tests;

[TestClass]
[TestCategory(Constants.Testing.IntegrationTest)]
public class ControllerTests
{
	// NOTE: You can use any non-static (accessible) class in the WebApi assembly as the marker class for the factory.
	private static readonly WebApplicationFactory<ProductsController> _webFactory = new();

	private static int RandomInteger => new Random().Next();

	[ClassCleanup]
	public static void ClassCleanup()
	{
		_webFactory.Dispose();
	}

	[TestMethod]
	public async Task WeatherForecast()
	{
		// ARRANGE
		var client = _webFactory.CreateClient();

		// ACT
		var response = await client.GetFromJsonAsync<Client.WeatherForecast[]>("/WeatherForecast");

		// ASSERT
		Assert.IsNotNull(response);
		Assert.AreEqual(5, response.Length);
	}

	[TestMethod]
	public async Task TestMe()
	{
		// ARRANGE
		var client = _webFactory.CreateClient();

		// ACT
		var response = await client.GetStringAsync("/TestMe");

		// ASSERT
		Assert.IsNotNull(response);
		Assert.IsTrue(response.Contains("TestMeController"));
		Assert.IsTrue(response.Contains("ProductManager"));
		Assert.IsTrue(response.Contains("ValidationEngine"));
		Assert.IsTrue(response.Contains("SqlProductAccessor"));
	}

	[TestMethod]
	public async Task Find_Products_WithDefaultRequest_ReturnsAllProducts()
	{
		// ARRANGE
		var client = _webFactory.CreateClient();
		var request = new Client.Products.FindRequest();

		// ACT
		var response = await client.PostAsJsonAsync("/Products/Find", request);

		// ASSERT
		Assert.IsNotNull(response);
		Assert.IsTrue(response.IsSuccessStatusCode);
		Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
		var products = await response.FromJson<Client.Products.ProductBase[]>();
		Assert.IsTrue(products.Length > 10);
	}

	[TestMethod]
	public async Task Store_Product_WithCommodity_StoresAndReturnsProductId()
	{
		// ARRANGE
		var client = _webFactory.CreateClient();
		var commodity = new Fixture().Build<Client.Products.Commodity>()
			.With(c => c.ProductType, Client.Products.ProductType.Commodity)
			.With(c => c.Name, $"Name-{RandomInteger}")
			.With(c => c.Description, $"Description-{RandomInteger}")
			.With(c => c.Manufacturer, $"Manufacturer-{RandomInteger}")
			.With(c => c.StockKeepingUnit, $"M-SKU-{RandomInteger}")
			.Create();

		// ACT
		var response = await client.PostAsJsonAsync("/Products/Commodity/Store", commodity);

		// ASSERT
		Assert.IsNotNull(response);
		Assert.IsTrue(response.IsSuccessStatusCode);
		Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
		var product = await response.FromJson<Client.Products.Commodity>();
		Assert.IsTrue(product.ProductId.HasValue);
	}

	[TestMethod]
	public async Task Store_Product_WithSalonProduct_StoresAndReturnsProductId()
	{
		// ARRANGE
		var client = _webFactory.CreateClient();
		var salonProduct = new Fixture().Build<Client.Products.SalonProduct>()
			.With(c => c.ProductType, Client.Products.ProductType.SalonProduct)
			.With(c => c.Name, $"Name-{RandomInteger}")
			.With(c => c.Description, $"Description-{RandomInteger}")
			.With(c => c.Manufacturer, $"Manufacturer-{RandomInteger}")
			.With(c => c.StockKeepingUnit, $"M-SKU-{RandomInteger}")
			.Create();

		// ACT
		var response = await client.PostAsJsonAsync("/Products/SalonProduct/Store", salonProduct);

		// ASSERT
		Assert.IsNotNull(response);
		Assert.IsTrue(response.IsSuccessStatusCode);
		Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
		var product = await response.FromJson<Client.Products.SalonProduct>();
		Assert.IsTrue(product.ProductId.HasValue);
	}

	[TestMethod]
	public async Task Store_Product_WithVehicle_StoresAndReturnsProductId()
	{
		// ARRANGE
		var client = _webFactory.CreateClient();
		var vehicle = new Fixture().Build<Client.Products.Vehicle>()
			.With(c => c.ProductType, Client.Products.ProductType.Vehicle)
			.With(c => c.Name, $"Name-{RandomInteger}")
			.With(c => c.Description, $"Description-{RandomInteger}")
			.With(c => c.Manufacturer, $"Manufacturer-{RandomInteger}")
			.With(c => c.StockKeepingUnit, $"M-SKU-{RandomInteger}")
			.With(c => c.VehicleIdentificationNumber, $"{RandomInteger}")
			.With(c => c.LicenseNumber, $"{RandomInteger}")
			.Create();

		// ACT
		var response = await client.PostAsJsonAsync("/Products/Vehicle/Store", vehicle);

		// ASSERT
		Assert.IsNotNull(response);
		Assert.IsTrue(response.IsSuccessStatusCode);
		Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
		var product = await response.FromJson<Client.Products.Vehicle>();
		Assert.IsTrue(product.ProductId.HasValue);
	}

	[TestMethod]
	public async Task Remove_Product_ByProductId_CompletesSuccessfully()
	{
		// ARRANGE
		var client = _webFactory.CreateClient();
		var findRequest = new Client.Products.FindRequest { IsDeleted = false };
		var response = await client.PostAsJsonAsync("/Products/Find", findRequest);
		Assert.IsNotNull(response);
		Assert.IsTrue(response.IsSuccessStatusCode);
		var products = await response.FromJson<Client.Products.ProductBase[]>();
		int productId = products.First().ProductId.Value;

		// ACT
		await client.DeleteAsync($"/Products/Remove/{ productId }");

		// ASSERT
		findRequest.ProductId = productId;
		response = await client.PostAsJsonAsync("/Products/Find", findRequest);
		Assert.IsNotNull(response);
		Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
	}

	[TestMethod]
	public async Task Store_Product_WithInvalidCommodity_ReturnsBadRequest()
	{
		// ARRANGE
		var client = _webFactory.CreateClient();

		// ACT
		var response = await client.PostAsJsonAsync("/Products/Commodity/Store", new Client.Products.Commodity());

		// ASSERT
		Assert.IsNotNull(response);
		Assert.IsFalse(response.IsSuccessStatusCode);
		Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
	}
}
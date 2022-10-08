using Microsoft.AspNetCore.Mvc;
using Pss.Reference.Managers;
using Pss.Reference.Managers.Contracts;
using Client = Pss.Reference.Contracts.Client.Products;

namespace Pss.Reference.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
	private readonly ManagerFactory _managerFactory;

	private IProductManager ProductManager => _managerFactory.Create<IProductManager>();

	public ProductsController(ManagerFactory managerFactory)
	{
		_managerFactory = managerFactory;
	}

	[HttpPost]
	[Route("Find")]
	public async Task<Client.ProductBase[]> Find(Client.FindRequest request)
	{
		var results = await ProductManager.Find(request);
		return results;
	}

	[HttpPost]
	[Route("Commodity/Store")]
	public async Task<Client.ProductBase> Store(Client.Commodity commodity)
	{
		var results = await ProductManager.Store(commodity);
		return results;
	}

	[HttpPost]
	[Route("SalonProduct/Store")]
	public async Task<Client.ProductBase> Store(Client.SalonProduct salonProduct)
	{
		var results = await ProductManager.Store(salonProduct);
		return results;
	}

	[HttpPost]
	[Route("Vehicle/Store")]
	public async Task<Client.ProductBase> Store(Client.Vehicle vehicle)
	{
		var results = await ProductManager.Store(vehicle);
		return results;
	}

	[HttpDelete]
	[Route("Remove/{productId}")]
	public async Task Remove(int productId)
	{
		await ProductManager.Remove(productId);
	}
}

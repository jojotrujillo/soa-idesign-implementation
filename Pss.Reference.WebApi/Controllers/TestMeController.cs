using Microsoft.AspNetCore.Mvc;
using Pss.Reference.Managers;
using Pss.Reference.Managers.Contracts;

namespace Pss.Reference.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TestMeController : ControllerBase
{
	private readonly ManagerFactory _managerFactory;

	public TestMeController(ManagerFactory managerFactory)
	{
		_managerFactory = managerFactory;
	}

	[HttpGet(Name = "TestMe")]
	public string TestMe()
	{
		var productManager = _managerFactory.Create<IProductManager>();
		var response = productManager.TestMe($"{GetType().Name}[{_managerFactory.Context.CorrelationId}]");
		return response;
	}
}

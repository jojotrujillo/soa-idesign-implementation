using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pss.Reference.Common;
using Pss.Reference.Common.Contracts;
using Pss.Reference.Shared.Tests;
using Pss.Reference.Utilities;
using Pss.Reference.Utilities.Contracts;

namespace Pss.Reference.Utility.Tests.IntegrationTests;

[TestClass]
[TestCategory(Constants.Testing.IntegrationTest)]
public class HttpUtilityTests
{
	[TestMethod]
	public async Task Send_GoogleUrl_ReturnsHtmlResponse()
	{
		// ARRANGE
		var utilityFactory = new UtilityFactory(new AmbientContext(), AssemblyInitialize.ServiceProvider);
		var httpUtility = utilityFactory.Create<IHttpUtility>();
		var request = new HttpRequestMessage(HttpMethod.Get, "https://google.com");

		// ACT
		var response = await httpUtility.Send(request).ConfigureAwait(false);

		// ASSERT
		Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
		Assert.IsNotNull(response.Body);
	}
}

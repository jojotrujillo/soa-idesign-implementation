using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pss.Reference.Common;
using Pss.Reference.Common.Contracts;
using Pss.Reference.Engines;
using Pss.Reference.Engines.Contracts;
using Pss.Reference.Shared.Tests;
using Logic = Pss.Reference.Contracts.Logic.Products;

namespace Pss.Reference.Engine.Tests.UnitTests;

[TestClass]
[TestCategory(Constants.Testing.UnitTest)]
public class ValidationEngineTests
{
	[TestMethod]
	public void Validate_WithInvalidRequestData_ReturnsExpectedValidationResults()
	{
		// ARRANGE
		string longString = Guid.NewGuid().ToString() + Guid.NewGuid().ToString();

		var request = new Logic.FindRequest
		{
			ProductId = 0,
			ProductType = Logic.ProductType.Unknown,
			Description = longString,
			Manufacturer = longString,
			StockKeepingUnit = longString,
			Name = longString,
		};

		var factory = new EngineFactory(new AmbientContext(), AssemblyInitialize.ServiceProvider);
		var engine = factory.Create<IValidationEngine>();

		// ACT
		var result = engine.Validate(request);

		// ASSERT
		Assert.IsNotNull(result);
		Assert.AreEqual(4, result.Length);
		Assert.IsTrue(result.All(r => !r.IsValid));
	}

	[TestMethod]
	public void Validate_ValidRequestData_ReturnsEmptyValidationResults()
	{
		// ARRANGE
		var request = new Logic.FindRequest
		{
			ProductId = 123,
			ProductType = Logic.ProductType.Commodity,
			Description = "Corn",
			Manufacturer = "Cargill",
			Name = "Corn",
		};

		var factory = new EngineFactory(new AmbientContext(), AssemblyInitialize.ServiceProvider);
		var engine = factory.Create<IValidationEngine>();

		// ACT
		var result = engine.Validate(request);

		// ASSERT
		Assert.IsNotNull(result);
		Assert.AreEqual(0, result.Length);
	}

	[TestMethod]
	public void Validate_InvalidMultiFieldRequestData_ReturnsExpectedValidationResults()
	{
		// ARRANGE
		var request = new Logic.FindRequest
		{
			ProductId = 123,
			ProductType = Logic.ProductType.Commodity,
			Description = "Corn",
			Manufacturer = "Cargill",
			StockKeepingUnit = Guid.NewGuid().ToString(),
			Name = "Corn",
		};

		var factory = new EngineFactory(new AmbientContext(), AssemblyInitialize.ServiceProvider);
		var engine = factory.Create<IValidationEngine>();

		// ACT
		var result = engine.Validate(request);

		// ASSERT
		Assert.IsNotNull(result);
		Assert.AreEqual(1, result.Length);
		Assert.IsTrue(result.All(r => !r.IsValid));
	}
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pss.Reference.Accessors;
using Pss.Reference.Accessors.Contracts;
using Pss.Reference.Common;
using Pss.Reference.Common.Contracts;
using Pss.Reference.Contracts.Client.Products;
using Pss.Reference.Contracts.Logic.Exceptions;
using Pss.Reference.Contracts.Logic.Validations;
using Pss.Reference.Engines;
using Pss.Reference.Engines.Contracts;
using Pss.Reference.Managers;
using Pss.Reference.Managers.Contracts;
using Pss.Reference.Shared.Tests;
using Pss.Reference.Shared.Tests.Extensions;
using Pss.Reference.Utilities;
using Pss.Reference.Utilities.Contracts;
using Logic = Pss.Reference.Contracts.Logic;

namespace Pss.Reference.Manager.Tests.UnitTests;

[TestClass]
[TestCategory(Constants.Testing.UnitTest)]
public class ProductManagerTests
{
	#region Test Cases
	// 1. Find with valid request returns expected data.
	// 2. Find with bad request throws validation exception.
	// 3. Find with no matching records returns empty array.
	// 4. Find with Id not found throws not found exception.
	// 5. Remove with valid Id calls accessor and sends notification.
	// 6. Remove with Id not found throws not found exception and does not send notification.
	// 7. Store with valid request calls accessor and sends notification.
	// 8. Store with bad request throws validation exception and does not send notification. 
	#endregion

	// 1. Find with valid request returns expected data.
	[TestMethod]
	public async Task Find_WithValidRequest_ReturnsExpectedData()
	{
		// ARRANGE
		var serviceCollection = AssemblyInitialize.CopyServiceCollection();
		var accessorResponse = new Logic.Products.ProductBase[]
		{
			new Logic.Products.Commodity{ ProductId = 1 },
			new Logic.Products.Vehicle{ ProductId = 2 }
		};

		var validationResult = new[] { new ValidationResult { IsValid = true } };

		var accessorMock = RegisterProductAccessorMock(serviceCollection, accessorResponse);
		var engineMock = RegisterValidationEngineMock(serviceCollection, e => validationResult);
		IProductManager manager = CreateManager(serviceCollection.BuildServiceProvider());

		FindRequest clientRequest = new FindRequest();

		// ACT
		var result = await manager.Find(clientRequest);

		// ASSERT
		Assert.IsNotNull(result);
		Assert.AreEqual(accessorResponse.Length, result.Length);
		Assert.AreEqual(accessorResponse.First().ProductId, result.First().ProductId);
		engineMock.Verify(e => e.Validate(It.IsAny<object>()), Times.Once);
		accessorMock.Verify(a => a.Find(It.IsAny<Logic.Products.FindRequest>()), Times.Once);
	}

	// 2. Find with bad request throws validation exception.
	[TestMethod]
	public async Task Find_InvalidRequest_ThrowsExpectedException()
	{
		var serviceCollection = AssemblyInitialize.CopyServiceCollection();
		var accessorResponse = Array.Empty<Logic.Products.ProductBase>();

		var validationResult = new[] { new ValidationResult { IsValid = false } };

		var engineMock = RegisterValidationEngineMock(serviceCollection, e => validationResult);
		var accessorMock = RegisterProductAccessorMock(serviceCollection, accessorResponse);
		IProductManager manager = CreateManager(serviceCollection.BuildServiceProvider());

		FindRequest clientRequest = new();

		// ACT & ASSERT
		await Assert.ThrowsExceptionAsync<ValidationException>(() => manager.Find(clientRequest));

		// ASSERT
		accessorMock.Verify(a => a.Find(It.IsAny<Logic.Products.FindRequest>()), Times.Never);
		engineMock.Verify(e => e.Validate(It.IsAny<object>()), Times.Once);
	}

	// 3. Find with no matching records returns empty array.
	[TestMethod]
	public async Task Find_WithNonMatchingRequest_ReturnsEmptyArray()
	{
		// ARRANGE
		var serviceCollection = AssemblyInitialize.CopyServiceCollection();
		var accessorResponse = Array.Empty<Logic.Products.ProductBase>();
		var validationResult = new[] { new ValidationResult { IsValid = true } };

		var accessorMock = RegisterProductAccessorMock(serviceCollection, accessorResponse);
		var engineMock = RegisterValidationEngineMock(serviceCollection, e => validationResult);
		IProductManager manager = CreateManager(serviceCollection.BuildServiceProvider());

		FindRequest clientRequest = new FindRequest();

		// ACT
		var result = await manager.Find(clientRequest);

		// ASSERT
		Assert.IsNotNull(result);
		Assert.IsTrue(result.Any() == false);
		Assert.AreEqual(accessorResponse.Length, result.Length);
		engineMock.Verify(e => e.Validate(It.IsAny<object>()), Times.Once);
		accessorMock.Verify(a => a.Find(It.IsAny<Logic.Products.FindRequest>()), Times.Once);
	}

	// 4. Find with Id not found throws not found exception.
	[TestMethod]
	public async Task Find_WithIdNotFound_ThrowsNotFoundException()
	{
		// ARRANGE
		var serviceCollection = AssemblyInitialize.CopyServiceCollection();
		var validationResult = new[] { new ValidationResult { IsValid = true } };

		var accessorMock = RegisterProductAccessorMockThrowsNotFoundException(serviceCollection);
		var engineMock = RegisterValidationEngineMock(serviceCollection, e => validationResult);
		IProductManager manager = CreateManager(serviceCollection.BuildServiceProvider());

		FindRequest clientRequest = new FindRequest();

		// ACT & ASSERT
		await Assert.ThrowsExceptionAsync<NotFoundException>(() => manager.Find(clientRequest));

		// ASSERT
		engineMock.Verify(e => e.Validate(It.IsAny<object>()), Times.Once);
		accessorMock.Verify(a => a.Find(It.IsAny<Logic.Products.FindRequest>()), Times.Once);
	}

	// 5. Remove with valid Id calls accessor and sends notification.
	[TestMethod]
	public async Task Remove_WithValidRequest_CallsAccessorAndSendsNotification()
	{
		// ARRANGE
		var serviceCollection = AssemblyInitialize.CopyServiceCollection();
		var accessorMock = RegisterProductAccessorMock(serviceCollection, null);
		var utilityMock = RegisterQueueUtilityMock(serviceCollection);
		IProductManager manager = CreateManager(serviceCollection.BuildServiceProvider());

		// ACT
		await manager.Remove(1);

		// ASSERT
		accessorMock.Verify(a => a.Remove(It.IsAny<int>()), Times.Once);
		utilityMock.Verify(e => e.Send(It.IsAny<object>()), Times.Once);
	}

	// 6. Remove with Id not found throws not found exception and does not send notification.
	[TestMethod]
	public async Task Remove_WithIdNotFound_ThrowsNotFoundException()
	{
		// ARRANGE
		var serviceCollection = AssemblyInitialize.CopyServiceCollection();
		var accessorMock = RegisterProductAccessorMockThrowsNotFoundException(serviceCollection);
		var utilityMock = RegisterQueueUtilityMock(serviceCollection);
		IProductManager manager = CreateManager(serviceCollection.BuildServiceProvider());

		// ACT
		await Assert.ThrowsExceptionAsync<NotFoundException>(() => manager.Remove(1));

		// ASSERT
		accessorMock.Verify(a => a.Remove(It.IsAny<int>()), Times.Once);
		utilityMock.Verify(e => e.Send(It.IsAny<object>()), Times.Never);
	}

	// 7. Store with valid request calls accessor and sends notification.
	[TestMethod]
	public async Task Store_WithValidRequest_CallsAccessorAndSendsNotification()
	{
		// ARRANGE
		var serviceCollection = AssemblyInitialize.CopyServiceCollection();
		var accessorResponse = new[] { new Logic.Products.Commodity() };
		var validationResult = new[] { new ValidationResult { IsValid = true } };
		var accessorMock = RegisterProductAccessorMock(serviceCollection, accessorResponse);
		var engineMock = RegisterValidationEngineMock(serviceCollection, e => validationResult);
		var utilityMock = RegisterQueueUtilityMock(serviceCollection);
		IProductManager manager = CreateManager(serviceCollection.BuildServiceProvider());

		var request = new Commodity();

		// ACT
		var result = await manager.Store(request);

		// ASSERT
		Assert.IsNotNull(result);
		accessorMock.Verify(a => a.Store(It.IsAny<Logic.Products.ProductBase>()), Times.Once);
		utilityMock.Verify(e => e.Send(It.IsAny<object>()), Times.Once);
	}

	// 8. Store with bad request throws validation exception and does not send notification. 
	[TestMethod]
	public async Task Store_WithInvalidRequest_ThrowsValidationExceptionAndDoesNotSendNotification()
	{
		// ARRANGE
		var serviceCollection = AssemblyInitialize.CopyServiceCollection();
		var accessorResponse = new[] { new Logic.Products.Commodity() };
		var validationResult = new[] { new ValidationResult { IsValid = false } };
		var accessorMock = RegisterProductAccessorMock(serviceCollection, accessorResponse);
		var engineMock = RegisterValidationEngineMock(serviceCollection, e => validationResult);
		var utilityMock = RegisterQueueUtilityMock(serviceCollection);
		IProductManager manager = CreateManager(serviceCollection.BuildServiceProvider());

		var request = new Commodity();

		// ACT & ASSERT
		await Assert.ThrowsExceptionAsync<ValidationException>(() => manager.Store(request));

		// ASSERT
		engineMock.Verify(e => e.Validate(It.IsAny<object>()), Times.Once);
		accessorMock.Verify(a => a.Store(It.IsAny<Logic.Products.ProductBase>()), Times.Never);
		utilityMock.Verify(u => u.Send(It.IsAny<object>()), Times.Never);
	}

	#region Private Methods
	private static IProductManager CreateManager(ServiceProvider serviceProvider)
	{
		AmbientContext context = new AmbientContext();
		ManagerFactory managerFactory = new ManagerFactory(context, serviceProvider);
		IProductManager manager = managerFactory.Create<IProductManager>();
		return manager;
	}

	private static Mock<IProductAccessor> RegisterProductAccessorMock(ServiceCollection serviceCollection, Logic.Products.ProductBase[] results)
	{
		var accessorMock = new Mock<AccessorBase>().As<IProductAccessor>();
		accessorMock.Setup(a => a.Find(It.IsAny<Logic.Products.FindRequest>())).ReturnsAsync(results);
		accessorMock.Setup(a => a.Remove(It.IsAny<int>()));

		if (results != null && results.Any())
			accessorMock.Setup(a => a.Store(It.IsAny<Logic.Products.ProductBase>())).ReturnsAsync(results.First());

		serviceCollection.RegisterOverride(accessorMock.Object);
		return accessorMock;
	}

	private static Mock<IProductAccessor> RegisterProductAccessorMockThrowsNotFoundException(ServiceCollection serviceCollection)
	{
		var accessorMock = new Mock<AccessorBase>().As<IProductAccessor>();
		accessorMock.Setup(a => a.Find(It.IsAny<Logic.Products.FindRequest>())).ThrowsAsync(new NotFoundException());
		accessorMock.Setup(a => a.Remove(It.IsAny<int>())).ThrowsAsync(new NotFoundException());
		serviceCollection.RegisterOverride(accessorMock.Object);
		return accessorMock;
	}

	private static Mock<IValidationEngine> RegisterValidationEngineMock(ServiceCollection serviceCollection, Func<object, ValidationResult[]> validationResult)
	{
		var engineMock = new Mock<EngineBase>().As<IValidationEngine>();
		engineMock.Setup(e => e.Validate(It.IsAny<object>())).Returns(validationResult);
		serviceCollection.RegisterOverride(engineMock.Object);
		return engineMock;
	}

	private static Mock<IQueueUtility> RegisterQueueUtilityMock(ServiceCollection serviceCollection)
	{
		var utilityMock = new Mock<UtilityBase>().As<IQueueUtility>();
		utilityMock.Setup(u => u.Send(It.IsAny<object>()));
		serviceCollection.RegisterOverride(utilityMock.Object);
		return utilityMock;
	}
	#endregion
}

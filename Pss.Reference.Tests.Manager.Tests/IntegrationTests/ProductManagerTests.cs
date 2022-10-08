using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pss.Reference.Accessors.Common;
using Pss.Reference.Accessors.Products;
using Pss.Reference.Accessors.Products.Configuration;
using Pss.Reference.Accessors.Products.Extensions;
using Pss.Reference.Common;
using Pss.Reference.Common.Contracts;
using Pss.Reference.Contracts.Logic.Exceptions;
using Pss.Reference.Contracts.Logic.Notifications;
using Pss.Reference.Managers;
using Pss.Reference.Managers.Contracts;
using Pss.Reference.Managers.Extensions;
using Pss.Reference.Shared.Tests;
using Pss.Reference.Utilities;
using Pss.Reference.Utilities.Contracts;
using Client = Pss.Reference.Contracts.Client.Products;

namespace Pss.Reference.Manager.Tests.IntegrationTests;

[TestClass]
[TestCategory(Constants.Testing.IntegrationTest)]
public class ProductManagerTests
{
	private static readonly IDbContextFactory<ProductDbContext> _dbContextFactory = new DbContextFactory<ProductDbContext>();

	#region Test Cases
	// 1. Find with valid request, returns expected results.
	// 2. Find with valid non-matchng request, returns empty results.
	// 3. Find with invalid request, throws Validation Exception.
	//
	// 4. Store with valid request, stores data and queues message.
	// 5. Store with invalid request, throws Validation Exception.
	//
	// 6. Remove with valid request, removes product and queues message.
	// 7. Remove with invalid request, throws Not Found exception. 
	#endregion

	[TestMethod]
	public void TestMe_IntegrationTest_ReturnsExpectedResult()
	{
		// ARRANGE
		IProductManager productManager = CreateManager();

		// ACT
		var result = productManager.TestMe(Constants.Testing.IntegrationTest);

		// ASSERT
		Assert.IsTrue(result.Contains("ProductManager"));
	}

	// 1. Find with valid request, returns expected results.
	[TestMethod]
	public async Task Find_WithValidRequest_ReturnsExpectedResults()
	{
		// ARRANGE
		using var dbContext = _dbContextFactory.CreateDbContext(AssemblyInitialize.Configuration);
		int expectedCount = dbContext.Products.Count();

		IProductManager productManager = CreateManager();
		Client.FindRequest request = new();

		// ACT
		var results = await productManager.Find(request);

		// ASSERT
		Assert.IsNotNull(results);
		Assert.AreEqual(expectedCount, results.Length);
	}

	// 2. Find with valid non-matchng request, returns empty results.
	[TestMethod]
	public async Task Find_WithNonMatchingCriteria_ReturnsEmptyCollection()
	{
		// ARRANGE
		IProductManager productManager = CreateManager();
		var request = new Client.FindRequest { Name = Guid.NewGuid().ToString() };

		// ACT
		var results = await productManager.Find(request);

		// ASSERT
		Assert.IsNotNull(results);
		Assert.IsTrue(results.Any() == false);
	}

	// 3. Find with invalid request, throws Validation Exception.
	[TestMethod]
	public async Task Find_WithInvalidRequest_ThrowsValidationException()
	{
		// ARRANGE
		IProductManager productManager = CreateManager();
		var request = new Client.FindRequest { ProductType = Client.ProductType.Unknown };

		// ACT & ASSERT
		await Assert.ThrowsExceptionAsync<ValidationException>(async () => await productManager.Find(request));
	}

	// 4. Store with valid request, stores data and queues message.
	[TestMethod]
	public async Task Store_WithValidRequest_StoresProductAndQueuesMessage()
	{
		// ARRANGE
		IProductManager productManager = CreateManager();
		using var dbContext = _dbContextFactory.CreateDbContext(AssemblyInitialize.Configuration);
		var product = dbContext.Products.First().ToLogic().ToClient();
		product.Description = Guid.NewGuid().ToString();

		using var cancellationTokenSource = new CancellationTokenSource();
		NotificationBase notification = null;
		SetupQueueUtility((message) =>
		{
			notification = (NotificationBase)message;
		}, 
		cancellationTokenSource.Token);

		// ACT
		var results = await productManager.Store(product);

		// ASSERT
		cancellationTokenSource.Cancel();
		Assert.IsNotNull(results);
		TestHelper.ValidatePropertyValues(product, results);
		Assert.IsNotNull(notification);
		Assert.IsTrue(notification.Message.Contains("Stored"));
		cancellationTokenSource.Token.WaitHandle.WaitOne();
	}

	// 5. Store with invalid request, throws Validation Exception.
	[TestMethod]
	public async Task Store_WithInvalidRequest_ThrowsValidationException()
	{
		// ARRANGE
		IProductManager productManager = CreateManager();

		// ACT & ASSERT
		await Assert.ThrowsExceptionAsync<ValidationException>(async () => await productManager.Store(new Client.Commodity()));
	}

	// 6. Remove with valid request, removes product and queues message.
	[TestMethod]
	public async Task Remove_WithValidRequest_RemovesProductAndQueuesMessage()
	{
		// ARRANGE
		var salonProduct = DataSeeder.GetSalonProducts().First();
		salonProduct.ProductId = 0;
		using var dbContext = _dbContextFactory.CreateDbContext(AssemblyInitialize.Configuration);
		dbContext.Products.Add(salonProduct);
		await dbContext.SaveChangesAsync();

		IProductManager productManager = CreateManager();
		var productId = salonProduct.ProductId;

		using var cancellationTokenSource = new CancellationTokenSource();
		NotificationBase notification = null;
		SetupQueueUtility((message) =>
		{
			notification = (NotificationBase)message;
		},
		cancellationTokenSource.Token);

		// ACT
		await productManager.Remove(productId);

		// ASSERT
		cancellationTokenSource.Cancel();
		Assert.IsNull(dbContext.Products.FirstOrDefault(p => p.ProductId == productId));
		Assert.IsNotNull(notification);
		Assert.IsTrue(notification.Message.Contains("Removed"));
		cancellationTokenSource.Token.WaitHandle.WaitOne();
	}

	// 7. Remove with invalid request, throws Not Found exception.
	[TestMethod]
	public async Task Remove_WithInvalidRequest_ThrowsNotFoundException()
	{
		// ARRANGE
		IProductManager productManager = CreateManager();

		// ACT & ASSERT
		await Assert.ThrowsExceptionAsync<NotFoundException>(async () => await productManager.Remove(int.MaxValue));
	}

	private static IProductManager CreateManager()
	{
		var context = new AmbientContext { CorrelationId = Guid.NewGuid().ToString() };
		var factory = new ManagerFactory(context, AssemblyInitialize.ServiceProvider);
		var productManager = factory.Create<IProductManager>();
		return productManager;
	}

	private static void SetupQueueUtility(Action<object> action, CancellationToken cancellationToken)
	{
		var factory = new UtilityFactory(new AmbientContext(), AssemblyInitialize.ServiceProvider);
		IQueueUtility queueUtility = factory.Create<IQueueUtility>();

		Task.Factory.StartNew(() => queueUtility.RegisterMessageHandler(action, cancellationToken), cancellationToken);
	}
}

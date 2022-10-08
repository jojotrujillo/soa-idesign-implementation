using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pss.Reference.Common;
using Pss.Reference.Common.Contracts;
using Pss.Reference.Contracts.Logic.Notifications;
using Pss.Reference.Shared.Tests;
using Pss.Reference.Utilities;
using Pss.Reference.Utilities.Contracts;

namespace Pss.Reference.Utility.Tests.IntegrationTests;

[TestClass]
[TestCategory(Constants.Testing.IntegrationTest)]
public class NetMqQueueUtilityTests
{
	[TestMethod]
	public void Send_NotificationMessage_TransmitsMessage()
	{
		// ARRANGE
		using var criticalSection = new ManualResetEventSlim(false);
		using var cancellationTokenSource = new CancellationTokenSource();
		StoreProductNotification notificationSent = new StoreProductNotification { Message = Guid.NewGuid().ToString(), ProductId = 123 };
		NotificationBase notificationReceived = null;
		var queueUtility = SetupQueueUtility(cancellationTokenSource.Token, (message) =>
		{
			notificationReceived = (NotificationBase)message;
			criticalSection.Set();
		});

		// ACT
		queueUtility.Send(notificationSent);

		// ASSERT
		Assert.IsTrue(criticalSection.Wait(500));
		cancellationTokenSource.Cancel();
		Assert.IsNotNull(notificationReceived);
		Assert.AreEqual(notificationSent.Message, notificationReceived.Message);
		Assert.AreEqual(notificationSent.ProductId, notificationReceived.ProductId);
		cancellationTokenSource.Token.WaitHandle.WaitOne();
	}

	private static IQueueUtility SetupQueueUtility(CancellationToken cancellationToken, Action<object> action)
	{
		var factory = new UtilityFactory(new AmbientContext(), AssemblyInitialize.ServiceProvider);
		IQueueUtility queueUtility = factory.Create<IQueueUtility>();

		Task.Factory.StartNew(() => queueUtility.RegisterMessageHandler(action, cancellationToken), cancellationToken);
		return queueUtility;
	}
}

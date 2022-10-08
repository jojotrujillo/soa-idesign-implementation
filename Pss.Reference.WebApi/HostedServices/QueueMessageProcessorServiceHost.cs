using Pss.Reference.Contracts.Logic.Notifications;
using Pss.Reference.Managers.Contracts;

namespace Pss.Reference.WebApi.HostedServices;

public class QueueMessageProcessorServiceHost : QueueMessageProcessorBase
{
	public QueueMessageProcessorServiceHost(IServiceProvider serviceProvider)
		: base(serviceProvider)
	{
	}

	protected override void MessageHandler(object message)
	{
		if (message == null)
			throw new ArgumentNullException(nameof(message));

		ManagerFactory.Create<INotificationManager>().Notify((NotificationBase)message);
	}
}

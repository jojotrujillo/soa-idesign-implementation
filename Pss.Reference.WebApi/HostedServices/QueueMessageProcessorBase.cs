using Pss.Reference.Common.Contracts;
using Pss.Reference.Managers;
using Pss.Reference.Utilities;
using Pss.Reference.Utilities.Contracts;

namespace Pss.Reference.WebApi.HostedServices;

public abstract class QueueMessageProcessorBase : IHostedService
{
	private readonly IServiceProvider _serviceProvider;

	protected QueueMessageProcessorBase(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
		ManagerFactory = new ManagerFactory(new AmbientContext(), serviceProvider);
	}

	protected ManagerFactory ManagerFactory { get; }

	public Task StartAsync(CancellationToken cancellationToken)
	{
		RegisterQueueMessageHandler(cancellationToken);
		return Task.CompletedTask;
	}

	protected abstract void MessageHandler(object message);

	private void RegisterQueueMessageHandler(CancellationToken cancellationToken)
	{
		var factory = new UtilityFactory(new AmbientContext(), _serviceProvider);
		IQueueUtility queueUtility = factory.Create<IQueueUtility>();

		Task.Factory.StartNew(() => queueUtility.RegisterMessageHandler(MessageHandler, cancellationToken), cancellationToken);
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		return Task.CompletedTask;
	}
}

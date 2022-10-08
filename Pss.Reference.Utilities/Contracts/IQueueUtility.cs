using Pss.Reference.Common.Contracts;

namespace Pss.Reference.Utilities.Contracts;

public interface IQueueUtility : IServiceComponent
{
	bool Send(object dataContract);

	void RegisterMessageHandler(Action<object> handler, CancellationToken cancellationToken);
}


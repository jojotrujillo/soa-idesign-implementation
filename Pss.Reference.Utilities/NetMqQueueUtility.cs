using NetMQ;
using NetMQ.Sockets;
using Pss.Reference.Common.Extensions;
using Pss.Reference.Utilities.Contracts;

namespace Pss.Reference.Utilities;

internal class NetMqQueueUtility : UtilityBase, IQueueUtility
{
	private static readonly string _address = $"tcp://127.0.0.1:{PortHelper.FindAvailablePort() ?? 5555}";
	private const string Acknowledge = "Acknowledge";

	public void RegisterMessageHandler(Action<object> handler, CancellationToken cancellationToken)
	{
		using var server = new ResponseSocket();
		server.Bind(_address);

		while (!cancellationToken.IsCancellationRequested)
		{
			if (server.TryReceiveFrameString(TimeSpan.FromMilliseconds(100), out string typeName))
			{
				server.SendFrame(Acknowledge);
				string json = server.ReceiveFrameString();
				server.SendFrame(Acknowledge);
				var dataContract = json.FromJson(typeName);
				handler.Invoke(dataContract);
			}
		}
	}

	public bool Send(object dataContract)
	{
		using var sender = new RequestSocket();
		sender.Connect(_address);

		string typeName = dataContract.GetType().AssemblyQualifiedName;
		sender.SendFrame(typeName); // Send the type name.

		string json = dataContract.ToJson();

		if (sender.TryReceiveFrameString(TimeSpan.FromSeconds(5), out string acknowledge))
			sender.SendFrame(json); // Send the type as JSON.

		return sender.TryReceiveFrameString(TimeSpan.FromSeconds(5), out acknowledge);
	}
}

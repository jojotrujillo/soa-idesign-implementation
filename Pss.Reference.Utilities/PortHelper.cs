using System.Net.NetworkInformation;

namespace Pss.Reference.Utilities;

internal static class PortHelper
{
	private const ushort MinimumPort = 5000;
	private const ushort MaximumPort = ushort.MaxValue;

	public static int? FindAvailablePort(ushort lowerPort = MinimumPort, ushort upperPort = MaximumPort)
	{
		var ipProperties = IPGlobalProperties.GetIPGlobalProperties();
		var usedPorts = Enumerable.Empty<int>()
			.Concat(ipProperties.GetActiveTcpConnections().Select(c => c.LocalEndPoint.Port))
			.Concat(ipProperties.GetActiveTcpListeners().Select(l => l.Port))
			.Concat(ipProperties.GetActiveUdpListeners().Select(l => l.Port))
			.ToHashSet();
		for (int port = lowerPort; port <= upperPort; port++)
		{
			if (!usedPorts.Contains(port))
				return port;
		}

		return null;
	}
}
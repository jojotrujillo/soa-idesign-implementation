using Microsoft.AspNetCore.SignalR;
using Pss.Reference.Common;

namespace Pss.Reference.WebApi.Hubs;

public class NotificationHub : Hub
{
	public async Task SendNotification(string message, int productId)
	{
		await Clients.All.SendAsync(Constants.SignalR.ReceiveMethod, message, productId);
	}
}

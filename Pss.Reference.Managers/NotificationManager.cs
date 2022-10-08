using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Pss.Reference.Common;
using Pss.Reference.Contracts.Logic.Notifications;
using Pss.Reference.Managers.Contracts;

namespace Pss.Reference.Managers;

internal class NotificationManager : ManagerBase, INotificationManager
{
	#region Fields
	private HubConnection _hubConnection;
	private ILogger<NotificationManager> _logger;
	private IConfiguration _configuration;
	#endregion

	#region Properties
	private ILogger<NotificationManager> Logger
	{
		get
		{
			if (_logger == null)
				_logger = Factory.ResolveRequiredService<ILoggerFactory>().CreateLogger<NotificationManager>();

			return _logger;
		}
	}

	private IConfiguration Configuration
	{
		get
		{
			if (_configuration == null)
				_configuration = Factory.ResolveRequiredService<IConfiguration>();

			return _configuration;
		}
	}

	private HubConnection HubConnection
	{
		get
		{
			if (_hubConnection == null)
			{
				string applicationUrl = Configuration["applicationUrl"];
				_hubConnection = new HubConnectionBuilder().WithUrl($"{applicationUrl}{Constants.SignalR.HubPath}").Build();
			}

			return _hubConnection;
		}
	}
	#endregion

	public async Task Notify(NotificationBase notification)
	{
		Logger.LogInformation($"Notification received for ProductId={notification.ProductId} and Message='{notification.Message}'");

		if (HubConnection.State != HubConnectionState.Connected)
			await HubConnection.StartAsync();

		if (HubConnection.State == HubConnectionState.Connected)
			await HubConnection.SendAsync(Constants.SignalR.SendMethod, notification.Message, notification.ProductId);
	}
}

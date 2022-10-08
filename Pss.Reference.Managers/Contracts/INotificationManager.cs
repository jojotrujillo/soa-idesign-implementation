using Pss.Reference.Common.Contracts;
using Pss.Reference.Contracts.Logic.Notifications;

namespace Pss.Reference.Managers.Contracts;

public interface INotificationManager : IServiceComponent
{
	Task Notify(NotificationBase notification);
}

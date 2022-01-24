using System;
using System.Threading.Tasks;
using TestSendNotifications.Storage;

namespace TestSendNotifications.Services
{
    public interface INotificationService
    {
        bool IsSended(Guid notificationId, out string error);

        NotificationDto SendNotification(NotificationDto notification);
    }
}
using System;

namespace TestSendNotifications.Storage
{
    public interface INotificationRepository
    {
        NotificationDto AddNotification(NotificationDto notification);

        bool IsNotificationDelivered(Guid notificationId, out string error);

        void ChangeSendedStateNotification(Guid notificationId, bool isDelivered);

        NotificationDto GetNotification(Guid notificationId);
    }
}
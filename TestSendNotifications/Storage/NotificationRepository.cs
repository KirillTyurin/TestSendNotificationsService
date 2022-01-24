using System;
using System.Collections.Generic;

namespace TestSendNotifications.Storage
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly Dictionary<Guid, NotificationDto> Notifications;
        
        public NotificationRepository()
        {
            Notifications = new Dictionary<Guid, NotificationDto>();
        }
        
        public NotificationDto AddNotification(NotificationDto notification)
        {
            notification.Id = Guid.NewGuid();
            Notifications.Add(notification.Id, notification);
            return notification;
        }

        public bool IsNotificationDelivered(Guid notificationId, out string error)
        {
            error = string.Empty;
            if (Notifications.TryGetValue(notificationId, out NotificationDto notification))
            {
                return notification.IsDelivered;
            }
            error = "Notification with this id doesn't exist";
            return false;
        }

        public void ChangeSendedStateNotification(Guid notificationId, bool isDelivered)
        {
            UpdateNotificationField(notificationId, x => x.IsDelivered = isDelivered);
        }

        private void UpdateNotificationField(Guid notificationId, Action<NotificationDto> action)
        {
            if (Notifications.TryGetValue(notificationId, out NotificationDto notification))
            {
                action(notification);
            }
        }

        public NotificationDto GetNotification(Guid notificationId)
        {
            return Notifications.TryGetValue(notificationId, out NotificationDto notification) ? notification : null;
        }
    }
}
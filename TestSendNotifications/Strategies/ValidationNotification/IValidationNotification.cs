using System.Collections.Generic;
using TestSendNotifications.Models;

namespace TestSendNotifications.Strategies.ValidationNotification
{
    public interface IValidationNotification
    {
        bool IsValid(NotificationModel notification, out string error);
    }
}
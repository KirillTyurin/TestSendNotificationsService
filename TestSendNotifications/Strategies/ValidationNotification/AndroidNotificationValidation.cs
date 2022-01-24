using System.Collections.Generic;
using TestSendNotifications.Models;

namespace TestSendNotifications.Strategies.ValidationNotification
{
    //TODO: add unit tests
    public class AndroidNotificationValidation : IValidationNotification
    {
        public bool IsValid(NotificationModel notification, out string error)
        {
            error = string.Empty;
            if (string.IsNullOrWhiteSpace(notification.DeviceTokenReciever) ||
                notification.DeviceTokenReciever.Length != 50)
            {
                error = $"Invalid {nameof(NotificationModel.DeviceTokenReciever)} field, value length must be equal 50";
                return false;
            }

            if (string.IsNullOrWhiteSpace(notification.Message) || notification.Message.Length > 2000)
            {
                error = $"Invalid {nameof(NotificationModel.Message)} field, value length must be less than 2000 letters";
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(notification.Title) || notification.Title.Length > 255)
            {
                error = $"Invalid {nameof(NotificationModel.Title)} field, value length must be less than 255 letters";
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(notification.Condition) || notification.Condition.Length > 2000)
            {
                error = $"Invalid {nameof(NotificationModel.Condition)} field, value length must be less than 2000 letters";
                return false;
            }

            return true;
        }
    }
}
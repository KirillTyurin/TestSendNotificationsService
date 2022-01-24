using TestSendNotifications.Models;

namespace TestSendNotifications.Strategies.ValidationNotification
{
    public class IosValidationNotification : IValidationNotification
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

            return true;
        }
    }
}
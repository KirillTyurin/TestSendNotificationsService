using System;

namespace TestSendNotifications.Entities
{
    public interface INotification
    {
        NotificationType NotificationType { get; set; }
        
        string Message { get; set; }
        
        string DeviceTokenReciever { get; set; }
    }
}
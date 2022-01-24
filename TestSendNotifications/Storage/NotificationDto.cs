using System;
using TestSendNotifications.Entities;

namespace TestSendNotifications.Storage
{
    public class NotificationDto
    {
        // General
        public Guid Id { get; set; }
        
        public NotificationType NotificationType { get; set; }
        
        public string TokenReciever { get; set; }
        
        public string Message { get; set; }
        
        public bool IsDelivered { get; set; }
        
        // only IOS
        
        public int? Priority { get; set; }
        
        public bool? IsBackground { get; set; }
        
        // only Android
        
        public string? Title { get; set; }
        
        public string? Condition { get; set; }
    }
}
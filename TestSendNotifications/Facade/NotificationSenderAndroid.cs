using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TestSendNotifications.Entities;
using TestSendNotifications.Storage;

namespace TestSendNotifications.Facade
{
    public class NotificationSenderAndroid : INotificationSender
    {
        private readonly ILogger<NotificationSenderAndroid> _logger;
        public NotificationSenderAndroid(ILogger<NotificationSenderAndroid> logger)
        {
            _logger = logger;
        }
        public bool IsMatch(NotificationDto notification)
        {
            return notification.NotificationType == NotificationType.Android;
        }

        public async Task<bool> Send()
        {
            _logger.LogInformation($"Notification sender: {nameof(NotificationSenderAndroid)}, sending time 1 minute.");
            await Task.Delay(new TimeSpan(0, 1, 0));
            return true;
        }
    }
}
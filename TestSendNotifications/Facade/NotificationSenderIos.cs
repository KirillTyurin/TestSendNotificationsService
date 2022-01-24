using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TestSendNotifications.Entities;
using TestSendNotifications.Storage;

namespace TestSendNotifications.Facade
{
    public class NotificationSenderIos : INotificationSender
    {
        private readonly ILogger<NotificationSenderIos> _logger;

        public NotificationSenderIos(ILogger<NotificationSenderIos> logger)
        {
            _logger = logger;
        }
        
        public bool IsMatch(NotificationDto notification)
        {
            return notification.NotificationType == NotificationType.Ios;
        }

        public async Task<bool> Send()
        {
            _logger.LogInformation($"Notification sender: {nameof(NotificationSenderIos)}, sending time 2 minutes");
            await Task.Delay(new TimeSpan(0, 2, 0));
            return true;
        }
    }
}
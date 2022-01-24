using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TestSendNotifications.Facade;
using TestSendNotifications.Storage;

namespace TestSendNotifications.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationSenderFacade _notificationSenderFacade;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(
            INotificationRepository notificationRepository,
            INotificationSenderFacade notificationSenderFacade,
            ILogger<NotificationService> logger)
        {
            _notificationRepository = notificationRepository;
            _notificationSenderFacade = notificationSenderFacade;
            _logger = logger;
        }
        
        public bool IsSended(Guid notificationId, out string error)
        {
            return _notificationRepository.IsNotificationDelivered(notificationId, out error);
        }

        public NotificationDto SendNotification(NotificationDto notification)
        {
            notification.IsDelivered = false;
            var notificationDto = _notificationRepository.AddNotification(notification);
            Task.Run(async () =>
            {
                try
                {
                    await _notificationSenderFacade.Send(notificationDto);
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    return;
                }
                _notificationRepository.ChangeSendedStateNotification(notification.Id, true);
            });

            return notificationDto;
        }
    }
}
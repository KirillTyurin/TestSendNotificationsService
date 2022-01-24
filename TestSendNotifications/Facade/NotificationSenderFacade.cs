using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestSendNotifications.Storage;

namespace TestSendNotifications.Facade
{
    public class NotificationSenderFacade : INotificationSenderFacade
    {
        private readonly IEnumerable<INotificationSender> _notificationSenders;

        public NotificationSenderFacade(IEnumerable<INotificationSender> notificationSenders)
        {
            _notificationSenders = notificationSenders;
        }
        
        public async Task Send(NotificationDto notification)
        {
            var sender = _notificationSenders.FirstOrDefault(x => x.IsMatch(notification));
            if (sender == null)
            {
                return;
            }

            await sender.Send();
        }
    }
}
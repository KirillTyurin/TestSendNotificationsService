using System.Threading.Tasks;
using TestSendNotifications.Storage;

namespace TestSendNotifications.Facade
{
    public interface INotificationSenderFacade
    {
        Task Send(NotificationDto notification);
    }
}
using System.Threading.Tasks;
using TestSendNotifications.Storage;

namespace TestSendNotifications.Facade
{
    public interface INotificationSender
    {
        bool IsMatch(NotificationDto notification);
        Task<bool> Send();
    }
}
using TestSendNotifications.Entities;
using TestSendNotifications.Facade;
using TestSendNotifications.Storage;
using Xunit;

namespace Test.TestSendNotifications.NotificationSender
{
    public class TestNotificationSenderIos
    {
        [Fact]
        public void TestIsMatchMethod()
        {
            var senderIos = new NotificationSenderIos(null);
            
            var iosNotificaiton = new NotificationDto()
            {
                NotificationType = NotificationType.Ios,
                TokenReciever = "12345678901234567890123456789012345678901234567890",
                Message = "Test Message"
            };

            var androidNotification = new NotificationDto()
            {
                NotificationType = NotificationType.Android,
                TokenReciever = "12345678901234567890123456789012345678901234567890",
                Message = "Test Message"
            };
            
            Assert.True(senderIos.IsMatch(iosNotificaiton));
            Assert.False(senderIos.IsMatch(androidNotification));
        }
    }
}
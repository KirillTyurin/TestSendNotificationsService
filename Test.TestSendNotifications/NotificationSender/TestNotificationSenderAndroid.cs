using TestSendNotifications.Entities;
using TestSendNotifications.Facade;
using TestSendNotifications.Storage;
using Xunit;

namespace Test.TestSendNotifications.NotificationSender
{
    public class TestNotificationSenderAndroid
    {
        [Fact]
        public void TestIsMatchMethod()
        {
            var senderAndroid = new NotificationSenderAndroid(null);
            
            var iosNotificaiton = new NotificationDto()
            {
                NotificationType = NotificationType.Ios,
                TokenReciever = "12345678901234567890123456789012345678901234567890",
                Message = "Test Message",
            };

            var androidNotification = new NotificationDto()
            {
                NotificationType = NotificationType.Android,
                TokenReciever = "12345678901234567890123456789012345678901234567890",
                Message = "Test Message",
            };
            
            Assert.False(senderAndroid.IsMatch(iosNotificaiton));
            Assert.True(senderAndroid.IsMatch(androidNotification));
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using TestSendNotifications.Entities;
using TestSendNotifications.Facade;
using TestSendNotifications.Storage;
using Xunit;

namespace Test.TestSendNotifications.NotificationSender
{
    public class TestNotificationSenderFacade
    {
        [Fact]
        public void TestSendMethod()
        {
            bool callIsMatchMethod = false;
            bool callSendMethod = false;
            var particularNotificationSender = new Mock<INotificationSender>();
            
            var notification = new NotificationDto()
            {
                NotificationType = NotificationType.Ios,
                TokenReciever = "12345678901234567890123456789012345678901234567890",
                Message = "Test Message"
            };
            
            particularNotificationSender.Setup(x => x.Send()).Callback(() =>
            {
                callSendMethod = true;
            });
            particularNotificationSender.Setup(x => x.IsMatch(notification)).Callback(() =>
            {
                callIsMatchMethod = true;
            }).Returns(true);
            
            var testSenderFacade = new NotificationSenderFacade(new List<INotificationSender>() { particularNotificationSender.Object });

            testSenderFacade.Send(notification);
            
            Assert.True(callIsMatchMethod);
            Assert.True(callSendMethod);
        }
    }
}
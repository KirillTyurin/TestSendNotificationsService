using System;
using TestSendNotifications.Entities;
using TestSendNotifications.Storage;
using Xunit;

namespace Test.TestSendNotifications.Repository
{
    
    public class TestRepository
    {
        [Fact]
        public void TestAddNotification()
        {
            var repository = new NotificationRepository();

            var notification = new NotificationDto()
            {
                NotificationType = NotificationType.Ios,
                TokenReciever = "12345678901234567890123456789012345678901234567890",
                Message = "Test Message",
                Priority = 15,
                IsBackground = false
            };

            var addedNotification = repository.AddNotification(notification);

            Assert.NotEqual(Guid.Empty, addedNotification.Id);

            var notificationFromDb = repository.GetNotification(addedNotification.Id);
            Assert.NotNull(notificationFromDb);
        }
        
        [Fact]
        public void TestChangeSendedStateNotificationMethod()
        {
            var repository = new NotificationRepository();

            var notification = new NotificationDto()
            {
                NotificationType = NotificationType.Ios,
                TokenReciever = "12345678901234567890123456789012345678901234567890",
                Message = "Test Message",
                Priority = 15,
                IsBackground = false,
                IsDelivered = false
            };

            var addedNotification = repository.AddNotification(notification);

            repository.ChangeSendedStateNotification(addedNotification.Id, true);

            var changedNotification = repository.GetNotification(addedNotification.Id);
            Assert.True(changedNotification.IsDelivered);
        }
        
        [Fact]
        public void TestIsNotificationDeliveredMethod()
        {
            var repository = new NotificationRepository();

            var tokenReciever = "12345678901234567890123456789012345678901234567890";
            var notification = new NotificationDto()
            {
                NotificationType = NotificationType.Ios,
                TokenReciever = tokenReciever,
                Message = "Test Message",
                Priority = 15,
                IsBackground = false,
                IsDelivered = true
            };

            var addedNotification = repository.AddNotification(notification);

            var stateNotification = repository.IsNotificationDelivered(notification.Id, out string error);
            Assert.True(stateNotification);
            Assert.True(string.IsNullOrEmpty(error));

            stateNotification = repository.IsNotificationDelivered(Guid.Empty, out error);
            Assert.False(stateNotification);
            Assert.NotEmpty(error);
        }
    }
}
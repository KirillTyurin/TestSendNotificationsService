using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Moq;
using TestSendNotifications.Entities;
using TestSendNotifications.Facade;
using TestSendNotifications.Services;
using TestSendNotifications.Storage;
using Xunit;

namespace Test.TestSendNotifications.Services
{
    public class TestNotificationService
    {
        [Fact]
        public void TestIsSendedMethod()
        {
            var callDataIsSendedFromRepository = false;
            var mockNotificationReposiory = new Mock<INotificationRepository>();
            var testId = Guid.NewGuid();
            string error;
            mockNotificationReposiory.Setup(x => x.IsNotificationDelivered(testId, out error))
                .Callback(() =>
                {
                    callDataIsSendedFromRepository = true;
                });

            var notificationService = new NotificationService(mockNotificationReposiory.Object, null, null);

            notificationService.IsSended(testId, out error);
            Assert.True(callDataIsSendedFromRepository);
        }
        
        [Fact]
        public void TestSendNotificationMethod()
        {
            var callDataFacadeSendMethod = false;
            var callAddNotificationMethod = false;
            var callUpdateStateNotificationMethod = false;
            var mockNotificationRepostiory = new Mock<INotificationRepository>();
            var mockNotificationSenderFacade = new Mock<INotificationSenderFacade>();
            var iosNotificaiton = new NotificationDto()
            {
                NotificationType = NotificationType.Ios,
                TokenReciever = "12345678901234567890123456789012345678901234567890",
                Message = "Test Message"
            };
            
            /// 1 - call method for add notificaiton in database
            /// 2 - call method for sending notification
            /// 3 - update notificaiotn after sending
            var actionsSequence = new Queue<int>();

            var addedId = Guid.NewGuid();

            mockNotificationRepostiory.Setup(x => x.AddNotification(iosNotificaiton))
                .Callback(() =>
                {
                    iosNotificaiton.Id = addedId;
                    callAddNotificationMethod = true;
                    actionsSequence.Enqueue(1);
                })
                .Returns(iosNotificaiton);

            mockNotificationRepostiory.Setup(x => x.ChangeSendedStateNotification(addedId, true))
                .Callback(() =>
                {
                    callUpdateStateNotificationMethod = true;
                    actionsSequence.Enqueue(3);
                });

            mockNotificationSenderFacade.Setup(x => x.Send(iosNotificaiton))
                .Callback(() =>
                {
                    callDataFacadeSendMethod = true;
                    actionsSequence.Enqueue(2);
                });

            var notificationService = new NotificationService(mockNotificationRepostiory.Object,
                mockNotificationSenderFacade.Object, null);

            notificationService.SendNotification(iosNotificaiton);
            Thread.Sleep(1000);
            
            Assert.True(callAddNotificationMethod);
            Assert.True(callDataFacadeSendMethod);
            Assert.True(callUpdateStateNotificationMethod);
            var correctActionsSequence = new Queue<int>(3);
            correctActionsSequence.Enqueue(1);
            correctActionsSequence.Enqueue(2);
            correctActionsSequence.Enqueue(3);
            Assert.Equal(correctActionsSequence, actionsSequence);
        }
    }
}
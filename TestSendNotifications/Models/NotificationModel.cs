using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using TestSendNotifications.Entities;
using TestSendNotifications.Storage;
using TestSendNotifications.Strategies.ValidationNotification;

namespace TestSendNotifications.Models
{
    public class NotificationModel : INotification, INotificationIos, 
        INotificationAndroid
    {
        private IValidationNotification _validationStrategy;

        public NotificationModel()
        {
        }
        
        public NotificationType NotificationType { get; set; }
        
        public string Message { get; set; }
        
        public string DeviceTokenReciever { get; set; }

        //Android
        public string? Title { get; set; }
        
        public string? Condition { get; set; }
        
        //IOS

        [DefaultValue(10)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public int? Priority { get; set; } = 10;

        [DefaultValue(true)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool? IsBackground { get; set; } = true;

        public bool IsValid(out string error)
        {
            if (_validationStrategy == null)
            {
                SetValidationFactory();
            }
            return _validationStrategy.IsValid(this, out error);
        }

        private void SetValidationFactory()
        {
            switch (NotificationType)
            {
                case NotificationType.Android:
                    _validationStrategy = new AndroidNotificationValidation();
                    break;
                case NotificationType.Ios:
                    _validationStrategy = new IosValidationNotification();
                    break;
            }
        }

        //Mark: better way use mappers (e.g. Automapper, ...)
        // TODO: replace on automap
        public NotificationDto Convert()
        {
            return new NotificationDto()
            {
                NotificationType = this.NotificationType,
                TokenReciever = this.DeviceTokenReciever,
                Message = this.Message,
                Priority = NotificationType == NotificationType.Ios ? Priority : null,
                Title = NotificationType == NotificationType.Android ? Title : null,
                IsBackground = NotificationType == NotificationType.Ios ? IsBackground : null,
                Condition = NotificationType == NotificationType.Android ? Condition : null
            };
        }
    }
}
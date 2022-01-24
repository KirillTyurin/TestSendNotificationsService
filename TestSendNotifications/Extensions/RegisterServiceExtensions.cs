using Microsoft.Extensions.DependencyInjection;
using TestSendNotifications.Facade;
using TestSendNotifications.Services;
using TestSendNotifications.Storage;

namespace TestSendNotifications.Extensions
{
    public static class RegisterServiceExtensions
    {
        public static IServiceCollection RegisterNotificationService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<INotificationRepository, NotificationRepository>();
            serviceCollection.AddScoped<INotificationService, NotificationService>();
            serviceCollection.AddScoped<INotificationSenderFacade, NotificationSenderFacade>();
            serviceCollection.AddScoped<INotificationSender, NotificationSenderAndroid>();
            serviceCollection.AddScoped<INotificationSender, NotificationSenderIos>();

            return serviceCollection;
        }
    }
}
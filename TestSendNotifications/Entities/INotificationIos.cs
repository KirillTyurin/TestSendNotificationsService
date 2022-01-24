namespace TestSendNotifications.Entities
{
    public interface INotificationIos
    {
        int? Priority { get; set; }
        
        bool? IsBackground { get; set; }
    }
}
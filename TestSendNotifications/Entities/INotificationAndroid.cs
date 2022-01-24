namespace TestSendNotifications.Entities
{
    public interface INotificationAndroid
    {
        string Title { get; set; }
        
        string Condition { get; set; }
    }
}
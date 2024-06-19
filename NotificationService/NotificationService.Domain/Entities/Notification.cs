namespace NotificationService.Domain.Entities;

public class Notification
{
    public Guid Id { get; set; }
    public string Message { get; set; }
    public string Recipient { get; set; }
    public Guid OrderId { get; set; }
    public DateTime SentAt { get; set; }
}
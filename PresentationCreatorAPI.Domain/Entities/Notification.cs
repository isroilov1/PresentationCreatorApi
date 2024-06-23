namespace PresentationCreatorAPI.Domain.Entites;

public class Notification : BaseEntity
{
    public string Message { get; set; } = string.Empty;
    public NotificationStatus Status { get; set; } = NotificationStatus.NotRead;
    public NotificationType Type { get; set; } 
    public int SenderId { get; set; }

    public List<int> RecipientIds { get; set; } = null!;
}

namespace Domain.Models;

public class Notification : BaseEntity
{
    public string Message { get; set; } = string.Empty;
    public NotificationStatus Status { get; set; } = NotificationStatus.NotRead;
    public int SenderId { get; set; }

    [ForeignKey(nameof(SenderId))]
    public User User { get; set; } = null!;

    public List<int> RecipientIds { get; set; } = null!;
}



namespace Application.DTOs;
public class NotificationDto : AddNotificationDto
{
    public int Id { get; set; }
    public int SenderId { get; set; }
    public NotificationStatus Status { get; set; } = NotificationStatus.NotRead;

    public static implicit operator NotificationDto(Notification notification)
    {
        return new NotificationDto()
        {
            Id = notification.Id,
            SenderId = notification.SenderId,
            Message = notification.Message,
            Status = notification.Status,
            RecipientIds = notification.RecipientIds
        };
    }
}

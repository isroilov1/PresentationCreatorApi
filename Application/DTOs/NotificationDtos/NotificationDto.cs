using Domain.Models;

namespace Application.DTOs;
public class NotificationDto : AddNotificationDto
{
    public int Id { get; set; }
    public int SenderId { get; set; }
    public NotificationStatus Status { get; set; }
    public NotificationType Type { get; set; }
    public string Created { get; set; } = string.Empty;
    
    public static implicit operator NotificationDto(Notification notification)
    {
        var tzTashkent = TimeZoneInfo.FindSystemTimeZoneById("Asia/Tashkent");
        var tashkentTime = TimeZoneInfo.ConvertTimeFromUtc(notification.CreatedAt, tzTashkent);
        string formattedDate = tashkentTime.ToString("dd-MM-yyyy HH");

        return new NotificationDto()
        {
            Id = notification.Id,
            SenderId = notification.SenderId,
            Message = notification.Message,
            Status = notification.Status,
            Type = notification.Type,
            Created = formattedDate,
            RecipientIds = notification.RecipientIds
        };
    }
}

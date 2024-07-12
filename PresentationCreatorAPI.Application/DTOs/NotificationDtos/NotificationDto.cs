using PresentationCreatorAPI.Application.Common.Helpers;
using PresentationCreatorAPI.Domain.Entites;

namespace PresentationCreatorAPI.Application.DTOs;
public class NotificationDto : AddNotificationDto
{
    public int Id { get; set; }
    public int SenderId { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Created { get; set; } = string.Empty;
    
    public static implicit operator NotificationDto(Notification notification)
    {
        string formattedDate = TimeHelper.TimeFormat(notification.CreatedAt);

        return new NotificationDto()
        {
            Id = notification.Id,
            SenderId = notification.SenderId,
            Message = notification.Message,
            Status = notification.Status.ToString(),
            Type = notification.Type.ToString(),
            Created = formattedDate,
            RecipientIds = notification.RecipientIds
        };
    }
}

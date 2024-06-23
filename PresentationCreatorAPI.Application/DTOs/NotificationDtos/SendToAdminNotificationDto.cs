using PresentationCreatorAPI.Domain.Entites;

namespace PresentationCreatorAPI.Application.DTOs;
public class SendToAdminNotificationDto
{
    public string Message { get; set; } = string.Empty;


    public static implicit operator Notification(SendToAdminNotificationDto dto)
    {
        return new Notification
        {
            Message = dto.Message,
            RecipientIds = {1, 2}
        };
    }
}

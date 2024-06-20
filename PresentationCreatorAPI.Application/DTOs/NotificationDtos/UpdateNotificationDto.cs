using PresentationCreatorAPI.Entites;

namespace PresentationCreatorAPI.Application.DTOs;

public class UpdateNotificationDto
{
    public string Message { get; set; } = string.Empty;
    public List<int> RecipientIds { get; set; } = null!;

    public static implicit operator Notification(UpdateNotificationDto dto)
    {
        return new Notification
        {
            Message = dto.Message,
            RecipientIds = dto.RecipientIds
        };
    }
}

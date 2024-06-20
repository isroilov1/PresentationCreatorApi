using PresentationCreatorAPI.Entites;

namespace PresentationCreatorAPI.Application.DTOs;
public class AddNotificationDto
{
    public string Message { get; set; } = string.Empty;

    public List<int>? RecipientIds { get; set; }

    public static implicit operator Notification(AddNotificationDto dto)
    {
        return new Notification
        {
            Message = dto.Message,
            RecipientIds = dto.RecipientIds
        };
    }
}

using Domain.Models;

namespace Application.DTOs.MovieDtos;
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

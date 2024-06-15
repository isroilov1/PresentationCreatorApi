namespace Application.DTOs.NotificationDtos;

public class UpdateNotificationDto
{
    public int Id { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<int> RecipientIds { get; set; } = null!;

    public static implicit operator Notification(UpdateNotificationDto dto)
    {
        return new Notification
        {
            Id = dto.Id,
            Message = dto.Message,
            RecipientIds = dto.RecipientIds
        };
    }
}

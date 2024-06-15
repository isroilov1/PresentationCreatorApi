namespace Application.Interfaces;

public interface INotificationService
{
    Task<int> CreateAsync(int senderId, AddNotificationDto dto);
    Task<NotificationDto> GetByIdAsync(int userId, int id);
    Task<List<NotificationDto>> GetByUserIdAsync(int userId);
    Task<List<NotificationDto>> GetAllAsync();
    Task UpdateAsync(int id, UpdateNotificationDto dto);
    Task DeleteAsync(int id);
}

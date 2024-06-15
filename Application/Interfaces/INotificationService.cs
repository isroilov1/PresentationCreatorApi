namespace Application.Interfaces;

public interface INotificationService
{
    //Task SendMessageByIdToUser(string message, int id);
    //Task SendMessageByPhoneNumberToUser(string message, string phoneNumber);
    //Task<int> SendMessageToAllUsers(string message);

    Task<int> CreateAsync(int senderId, AddNotificationDto dto);
    Task<NotificationDto> GetByIdAsync(int id);
    Task<List<NotificationDto>> GetByUserIdAsync(int userId);
    Task<List<NotificationDto>> GetAllAsync();
    Task UpdateAsync(int id, UpdateNotificationDto dto);
    Task DeleteAsync(int id);
}

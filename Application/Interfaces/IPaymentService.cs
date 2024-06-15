namespace Application.Interfaces;

public interface IPaymentService
{
    Task CreateAsync(AddPaymentDto dto);
    Task<PaymentDto> GetByIdAsync(int id);
    Task<NotificationDto> GetByPhoneNumberAsync(string phoneNumber);
    Task<List<NotificationDto>> GetAllAsync();
    Task UpdateAsync(int id, UpdateUserDto dto);
    Task DeleteAsync(int id);
}

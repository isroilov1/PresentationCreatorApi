namespace Application.Interfaces;

public interface IPaymentService
{
    Task CreateAsync(int id, AddPaymentDto dto);
    Task<PaymentDto> GetByIdAsync(int id);
    Task<PaymentDto> GetByPhoneNumberAsync(string phoneNumber);
    Task<List<PaymentDto>> GetAllAsync();
    Task UpdateAsync(int id, UpdatePaymentDto dto);
    Task DeleteAsync(int id);
}

using PresentationCreatorAPI.Application.DTOs;
using PresentationCreatorAPI.Enums;

namespace PresentationCreatorAPI.Application.Interfaces;

public interface IPaymentService
{
    Task CreateAsync(int id, AddPaymentDto dto);
    Task<PaymentDto> GetByIdAsync(int id);
    Task<List<PaymentDto>> GetByUserIdAsync(int userId);
    Task<List<PaymentDto>> GetByPhoneNumberAsync(string phoneNumber);
    Task<List<PaymentDto>> GetAllAsync();
    Task UpdateAsync(UpdatePaymentDto dto);
    Task AcceptPaymentAsync(int id, PaymentStatus status, string caption, int accepterId);
    Task RejectPaymentAsync(int id, PaymentStatus status, string caption, int accepterId);
    Task DeleteAsync(int id);
}


using MimeKit;

namespace Application.Services;

public class PaymentServicce(IUnitOfWork unitOfWork,
                          IValidator<Payment> validator) : IPaymentService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidator<Payment> _validator = validator;

    public async Task CreateAsync(int id, AddPaymentDto dto)
    {
        var valid = await _validator.ValidateAsync(dto);
        if (!valid.IsValid)
            throw new ValidationException(valid.GetErrorMessages());

        var payment = (Payment)dto;
        var user = await _unitOfWork.User.GetByIdAsync(id);
        if (user is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi!");
        payment.UserId = id;
        payment.User = user;
        await _unitOfWork.Payment.CreateAsync(payment);
    }

    public async Task DeleteAsync(int id)
    {
        var payment = await _unitOfWork.Payment.GetByIdAsync(id);
        if (payment is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "To'lov mavjud emas");
        await _unitOfWork.Payment.DeleteAsync(payment);
    }

    public async Task<List<PaymentDto>> GetAllAsync()
    {
        var payment = await _unitOfWork.Payment.GetAllAsync();
        return payment.Select(x => (PaymentDto)x).ToList();
    }

    public async Task<PaymentDto> GetByIdAsync(int id)
    {
        var payment = await _unitOfWork.Payment.GetByIdAsync(id);
        if (payment is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "To'lov mavjud emas");
        return payment;
    }

    public async Task<PaymentDto> GetByPhoneNumberAsync(string phoneNumber)
    {
        var user = await _unitOfWork.User.GetByPhoneNumberAsync(phoneNumber);
        if (user is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "Ushbu telefon raqam orqali foydalanuvchi topilmadi!");
        var payment = user.TotalPayments;
        if (payment is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "To'lov mavjud emas");
        return payment;
    }

    public async Task UpdateAsync(int id, UpdatePaymentDto dto)
    {
        var payment = await _unitOfWork.Payment.GetByIdAsync(id);
        if (payment is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "To'lov mavjud emas");

        await _unitOfWork.Payment.UpdateAsync(payment);
    }
}

namespace Application.Services;

public class PaymentServicce(IUnitOfWork unitOfWork,
                          IValidator<Payment> validator) : IPaymentService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidator<Payment> _validator = validator;

    public async Task AcceptOrRejectAsync(int id, PaymentStatus status, string caption)
    {
        var payment = await _unitOfWork.Payment.GetByIdAsync(id);
        if (payment is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "To'lov mavjud emas");
        payment.Status = status;
        payment.AdminCaption = caption;
        await _unitOfWork.Payment.UpdateAsync(payment);
    }

    public async Task CreateAsync(int id, AddPaymentDto dto)
    {
        var valid = await _validator.ValidateAsync(dto);
        if (!valid.IsValid)
            throw new ValidationException(valid.GetErrorMessages());

        var payment = (Payment)dto;
        var user = await _unitOfWork.User.GetByIdIncludeAsync(id);
        if (user is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi!");
        payment.UserId = id;
        //payment.User = user;
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
        var payments = await _unitOfWork.Payment.GetAllAsync();
        return payments.Select(x => (PaymentDto)x).ToList();
    }

    public async Task<PaymentDto> GetByIdAsync(int id)
    {
        var payment = await _unitOfWork.Payment.GetByIdAsync(id);
        if (payment is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "To'lov mavjud emas");
        return payment;
    }

    public async Task<List<PaymentDto>> GetByPhoneNumberAsync(string phoneNumber)
    {
        var user = await _unitOfWork.User.GetByPhoneNumberAsync(phoneNumber);
        if (user is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "Ushbu telefon raqam orqali foydalanuvchi topilmadi!");
        var payments = user.Payments;
        if (payments is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "To'lovlar mavjud emas");
        return payments.Select(x => (PaymentDto)x).ToList();
    }

    public async Task<List<PaymentDto>> GetByUserIdAsync(int userId)
    {
        var user = await _unitOfWork.User.GetByIdIncludeAsync(userId);
        if (user is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi");

        var payments = user.Payments;
        if (payments is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "Ushbu foydalanuvchining to'lovlari mavjud emas!");
        return payments.Select(x => (PaymentDto)x).ToList();
    }

    public async Task UpdateAsync(UpdatePaymentDto dto)
    {
        var payment = await _unitOfWork.Payment.GetByIdAsync(dto.Id);
        if (payment is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "To'lov mavjud emas");

        await _unitOfWork.Payment.UpdateAsync(payment);
    }
}

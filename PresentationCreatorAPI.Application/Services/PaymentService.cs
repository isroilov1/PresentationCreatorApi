using FluentValidation;
using PresentationCreatorAPI.Application.Common.Exceptions;
using PresentationCreatorAPI.Application.Common.Helpers;
using PresentationCreatorAPI.Application.Common.Validators;
using PresentationCreatorAPI.Application.DTOs;
using PresentationCreatorAPI.Application.Interfaces;
using PresentationCreatorAPI.Data.Interfaces;
using PresentationCreatorAPI.Entites;
using PresentationCreatorAPI.Enums;
using System.Net;

namespace PresentationCreatorAPI.Application.Services;

public class PaymentServicce(IUnitOfWork unitOfWork,
                          IValidator<Payment> validator) : IPaymentService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidator<Payment> _validator = validator;

    public async Task AcceptOrRejectAsync(int id, PaymentStatus status, string caption, int accepterId)
    {
        var payment = await _unitOfWork.Payment.GetByIdAsync(id);
        if (payment is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "To'lov mavjud emas");
        if (payment.Status == PaymentStatus.Accepted)
            throw new StatusCodeException(HttpStatusCode.AlreadyReported, "Ushbu to'lov allaqachon qabul qilingan!");
        if (status == PaymentStatus.Expected)
            throw new StatusCodeException(HttpStatusCode.BadRequest, "To'lov statusini kutilayotgan kabi o'zgartira olmaysiz!");
        payment.Status = status;
        payment.AdminCaption = caption;
        await _unitOfWork.Payment.UpdateAsync(payment);

        var user = await _unitOfWork.User.GetByIdAsync(payment.UserId);
        if (user is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Ushbu to'lov egasi topilmadi");
        user.Balance = user.Balance + payment.Summa;

        var message = status == PaymentStatus.Rejected
        ? $"To'lov rad qilindi!\n\nPayment Id: {id}\nSumma: {payment.Summa}"
        : $"To'lov qabul qilindi!\n\nPayment Id: {id}\nSumma: {payment.Summa}";

        var senderUser = await _unitOfWork.User.GetByIdAsync(accepterId);
        if (senderUser is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Tasdiqlayotgan admin akaunti bilan bog'liq muammo yuzaga keldi!");

        var recipientIds = new List<int> { payment.UserId };
        var notificationToUser = new Notification
        {
            Message = message,
            Status = NotificationStatus.NotRead,
            Type = NotificationType.Input,
            SenderId = accepterId,
            RecipientIds = recipientIds
        };
        await _unitOfWork.Notification.CreateAsync(notificationToUser);
        if (user.Notifications == null)
            user.Notifications = new List<Notification>();
        user.Notifications.Add(notificationToUser);
        user.TotalPayments = user.TotalPayments + payment.Summa;
        await _unitOfWork.User.UpdateAsync(user);

        var notificationToAdmin = new Notification
        {
            Message = message,
            Status = NotificationStatus.NotRead,
            Type = NotificationType.Output,
            SenderId = accepterId,
            RecipientIds = recipientIds
        };
        await _unitOfWork.Notification.CreateAsync(notificationToAdmin);

        if (senderUser.Notifications == null)
            senderUser.Notifications = new List<Notification>();

        senderUser.Notifications.Add(notificationToAdmin);
        await _unitOfWork.User.UpdateAsync(senderUser);
    }

    public async Task CreateAsync(int id, AddPaymentDto dto)
    {
        var valid = await _validator.ValidateAsync(dto);
        if (!valid.IsValid)
            throw new ValidationException(valid.GetErrorMessages());

        var payment = (Payment)dto;
        string rootPath = "uploads/payments";
        string filePath = FileHelper.SaveFile(dto.File, rootPath);
        payment.FilePath = filePath;

        var user = await _unitOfWork.User.GetByIdIncludeAsync(id);
        if (user is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi!");
        payment.UserId = id;
        await _unitOfWork.Payment.CreateAsync(payment);
    }

    public async Task DeleteAsync(int id)
    {
        var payment = await _unitOfWork.Payment.GetByIdAsync(id);
        if (payment is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "To'lov mavjud emas");

        string filePath = payment.FilePath;
        if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
        {
            File.Delete(filePath);
        }

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
            throw new StatusCodeException(HttpStatusCode.NotFound, "To'lov mavjud emas");
        return payment;
    }

    public async Task<List<PaymentDto>> GetByPhoneNumberAsync(string phoneNumber)
    {
        var user = await _unitOfWork.User.GetByPhoneNumberAsync(phoneNumber);
        if (user is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Ushbu telefon raqam orqali foydalanuvchi topilmadi!");
        var payments = user.Payments;
        if (payments is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "To'lovlar mavjud emas");
        return payments.Select(x => (PaymentDto)x).ToList();
    }

    public async Task<List<PaymentDto>> GetByUserIdAsync(int userId)
    {
        var user = await _unitOfWork.User.GetByIdIncludeAsync(userId);
        if (user is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi");

        var payments = user.Payments;
        if (payments is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Ushbu foydalanuvchining to'lovlari mavjud emas!");
        return payments.Select(x => (PaymentDto)x).ToList();
    }

    public async Task UpdateAsync(UpdatePaymentDto dto)
    {
        var payment = await _unitOfWork.Payment.GetByIdAsync(dto.Id);
        if (payment is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Ushu Id raqam bilan to'lov topilmadi!");
        if (payment.Status == PaymentStatus.Accepted)
            throw new StatusCodeException(HttpStatusCode.AlreadyReported, "Ushbu to'lov allaqachon qabul qilingan. Qabul qilingan to'lovni o'zgartirish mumkin emas!");
        else if (payment.Status == PaymentStatus.Rejected)
            throw new StatusCodeException(HttpStatusCode.AlreadyReported, "Ushbu to'lov rad etilgan. Rad etilgan to'lovni o'zgartirish mumkin emas!");

        string oldFilePath = payment.FilePath;
        if (!string.IsNullOrEmpty(oldFilePath) && File.Exists(oldFilePath))
            File.Delete(oldFilePath);

        string rootPath = "uploads/payments";
        string filePath = FileHelper.SaveFile(dto.File, rootPath);
        payment.FilePath = filePath;
        if (payment is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "To'lov mavjud emas");

        await _unitOfWork.Payment.UpdateAsync(payment);
    }
}


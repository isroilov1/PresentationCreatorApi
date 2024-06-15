using Application.Common.Exceptions;
using Application.Common.Validators;
using Application.DTOs;
using Application.DTOs.MovieDtos;
using Application.DTOs.NotificationDtos;
using Application.Interfaces;
using Data.Interfaces;
using Domain.Models;
using FluentValidation;
using System.Net;
namespace Application.Services;

public class NotificationService(IUnitOfWork unitOfWork,
                          IValidator<Notification> validator) : INotificationService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidator<Notification> _validator = validator;

    public async Task CreateAsync(int senderId, AddNotificationDto dto)
    {
        var result = await _validator.ValidateAsync(dto);
        if (!result.IsValid)
            throw new ValidationException(result.GetErrorMessages());
        var notification = (Notification)dto;
        notification.SenderId = senderId;
        var senderUser = await _unitOfWork.User.GetByIdAsync(senderId);
        if (senderUser is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "Bunday foydalanuvchi mavjud emas!");

        await _unitOfWork.Notification.CreateAsync(notification);
        if (senderUser.Notifications == null)
        {
            senderUser.Notifications = new List<Notification>();
        }
        notification.Type = Domain.Enums.NotificationType.Output;
        senderUser.Notifications.Add(notification);
        await _unitOfWork.User.UpdateAsync(senderUser);

        var inputNotification = (Notification)dto;
        var recipients = dto.RecipientIds;
        if (recipients == null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "Ushbu recipientId raqam orqali foydalanuvchi topilmadi!");
        
        foreach (var recipient in recipients)
        {
            var recipientUser = await _unitOfWork.User.GetByIdAsync(recipient);
            if (recipientUser is null)
                throw new StatusCodeExeption(HttpStatusCode.NotFound, "Bunday foydalanuvchi mavjud emas!");

            await _unitOfWork.Notification.CreateAsync(inputNotification);
            if (recipientUser.Notifications == null)
            {
                recipientUser.Notifications = new List<Notification>();
            }
            inputNotification.Type = Domain.Enums.NotificationType.Input;
            recipientUser.Notifications.Add(inputNotification);
            await _unitOfWork.User.UpdateAsync(recipientUser);
        }
    }

    public async Task DeleteAsync(int id)
    {
        var notification = await _unitOfWork.Notification.GetByIdAsync(id);
        if (notification is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "Bildirishnoma mavjud emas");
        await _unitOfWork.Notification.DeleteAsync(notification);
    }

    public async Task<List<NotificationDto>> GetAllAsync()
    {
        var notifications = await _unitOfWork.Notification.GetAllAsync();
        return notifications.Select(x => (NotificationDto)x).ToList();
    }

    public async Task<NotificationDto> GetByIdAsync(int id)
    {
        var notification = await _unitOfWork.Notification.GetByIdAsync(id);
        if (notification is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "Bildirishnoma mavjud emas");
        return notification;
    }

    public async Task<List<NotificationDto>> GetByUserIdAsync(int userId)
    {
        var user = await _unitOfWork.User.GetByIdAsync(userId);
        if (user is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi");

        var notifications = user.Notifications;
        if (notifications is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "Ushbu foydalanuvchining xabarlari mavjud emas!");
        return notifications.Select(x => (NotificationDto)x).ToList();
    }

    public async Task UpdateAsync(int id, UpdateNotificationDto dto)
    {
        var notification = await _unitOfWork.Notification.GetByIdAsync(id);
        if (notification is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "Bildirishnoma mavjud emas");

        await _unitOfWork.Notification.UpdateAsync(notification);
    }
}

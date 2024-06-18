namespace Application.Services;

public class NotificationService(IUnitOfWork unitOfWork,
                          IValidator<Notification> validator) : INotificationService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidator<Notification> _validator = validator;

    public async Task<int> CreateAsync(int senderId, AddNotificationDto dto)
    {
        var result = await _validator.ValidateAsync(dto);
        if (!result.IsValid)
            throw new ValidationException(result.GetErrorMessages());
        var notification = (Notification)dto;
        notification.SenderId = senderId;
        notification.Type = NotificationType.Output;
        var senderUser = await _unitOfWork.User.GetByIdAsync(senderId);
        if (senderUser is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "Bunday foydalanuvchi mavjud emas!");

        await _unitOfWork.Notification.CreateAsync(notification);
        if (senderUser.Notifications == null)
        {
            senderUser.Notifications = new List<Notification>();
        }
        senderUser.Notifications.Add(notification);
        await _unitOfWork.User.UpdateAsync(senderUser);

        var recipientIds = dto.RecipientIds;
        if (recipientIds == null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "Yuborish uchun foydalanuuvchilar ID raqami kiritilmagan");
        recipientIds = recipientIds.Distinct().ToList();

        int totalCountRecipients = 0;
        foreach (var recipientId in recipientIds)
        {
            if (recipientId == senderId) continue;
            var notification2 = new Notification
            {
                Message = dto.Message,
                Status = NotificationStatus.NotRead,
                Type = NotificationType.Input,
                SenderId = senderId,
                RecipientIds = recipientIds
            };
            var recipientUser = await _unitOfWork.User.GetByIdAsync(recipientId);
            if (recipientUser is not null)
            {
                if (recipientUser.Notifications == null)
                {
                    recipientUser.Notifications = new List<Notification>();
                }
                recipientUser.Notifications.Add(notification2);
                await _unitOfWork.User.UpdateAsync(recipientUser);
                totalCountRecipients++;
            }
        }
        return totalCountRecipients;
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

    public async Task<NotificationDto> GetByIdAsync(int userId, int id)
    {
        var notification = await _unitOfWork.Notification.GetByIdAsync(id);
        if (notification is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "Bildirishnoma mavjud emas");
        
        if (notification.Status == NotificationStatus.NotRead && notification.RecipientIds.Contains(userId))
        {
            notification.Status = NotificationStatus.Read;
            await _unitOfWork.Notification.UpdateAsync(notification);
        }
        return notification;
    }

    public async Task<List<NotificationDto>> GetByUserIdAsync(int userId)
    {
        var user = await _unitOfWork.User.GetByIdIncludeAsync(userId);
        if (user is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "Foydalanuvchi topilmadi");

        var notifications = user.Notifications;
        if (notifications is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "Ushbu foydalanuvchining xabarlari mavjud emas!");
        return notifications.Select(x => (NotificationDto)x).ToList();
    }

    public async Task UpdateAsync(int id, UpdateNotificationDto dto)
    {
        var model = await _unitOfWork.Notification.GetByIdAsync(id);
        if (model is null)
            throw new StatusCodeExeption(HttpStatusCode.NotFound, "Bildirishnoma mavjud emas");
        var notification = (Notification)dto;
        notification.SenderId = model.SenderId;
        notification.Id = model.Id;
        notification.Status = model.Status;
        notification.Type = model.Type;

        await _unitOfWork.Notification.UpdateAsync(notification);
    }
}

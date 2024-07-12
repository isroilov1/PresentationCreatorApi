using PresentationCreatorAPI.Application.Common.Helpers;
using PresentationCreatorAPI.Domain.Entites;
using PresentationCreatorAPI.Enums;

namespace PresentationCreatorAPI.Application.DTOs.UserDtos;
public class UserDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int Balance { get; set; } = 4000;
    public int? ReferalId { get; set; }
    public bool ReferalBonus { get; set; }
    public string Role { get; set; } = string.Empty;
    public bool IsVerified { get; set; } = false;
    public int PresentationCount { get; set; } = 0;
    public int? TotalPayments { get; set; }
    public string CreatedAt { get; set; } = string.Empty;
    public int? TelegramId { get; set; }
    public int PaymentsCount { get; set; }
    public int NotificationsCount { get; set; }
    public int PresentationsCount { get; set; }

    public static implicit operator UserDto(User user)
    {
        string formattedDate = TimeHelper.TimeFormat(user.CreatedAt);

        return new UserDto()
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Balance = user.Balance,
            Password = user.Password,
            IsVerified = user.IsVerified,
            Role = user.Role.ToString(),
            CreatedAt = formattedDate,
            ReferalId = user.ReferalId,
            ReferalBonus = user.ReferalBonus,
            TelegramId = user.TelegramId,
            TotalPayments = user.TotalPayments,
            PresentationCount = user.PresentationCount,
            PresentationsCount = user.Presentations?.Count() ?? 0,
            PaymentsCount = user.Payments?.Select(u => u.Status = PaymentStatus.Accepted).Count() ?? 0,
            NotificationsCount = user.Notifications?.Count() ?? 0
            //Notifications = user.Notifications?.Select(n => new Notification
            //{
            //    Id = n.Id,
            //    Message = n.Message,
            //    Status = n.Status,
            //    SenderId = n.SenderId,
            //    RecipientIds = n.RecipientIds
            //}).ToList(),
            //Payments = user.Payments?.Select(n => new Payment
            //{
            //    Id = n.Id,
            //    Summa = n.Summa,
            //    Caption = n.Caption,
            //    Status = n.Status,
            //    FilePath = n.FilePath,
            //    UserId = n.UserId
            //}).ToList()
        };
    }
}



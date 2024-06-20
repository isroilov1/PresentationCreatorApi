using Domain.Models;

namespace Application.DTOs.UserDtos;
public class UserDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int Balance { get; set; } = 4000;
    public int ReferalId { get; set; }
    public string Role { get; set; } = string.Empty;
    public bool IsVerified { get; set; } = false;
    public int PresentationCount { get; set; } = 0;
    public int? TotalPayments { get; set; }
    public List<Payment>? Payments { get; set; }
    public string CreatedAt { get; set; } = string.Empty;
    public List<Notification>? Notifications { get; set; }
    public List<Presentation>? PresentationPaths { get; set; }

    public static implicit operator UserDto(User user)
    {
        var tzTashkent = TimeZoneInfo.FindSystemTimeZoneById("Asia/Tashkent");
        var tashkentTime = TimeZoneInfo.ConvertTimeFromUtc(user.CreatedAt, tzTashkent);
        string formattedDate = tashkentTime.ToString("dd-MM-yyyy HH:mm");

        return new UserDto()
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            ReferalId = user.ReferalId,
            IsVerified = user.IsVerified,
            Balance = user.Balance,
            Password = user.Password,
            PresentationCount = user.PresentationCount,
            TotalPayments = user.TotalPayments,
            PresentationPaths = user.PresentationPaths,
            CreatedAt = formattedDate,
            Role =  user.Role.ToString(),
            Notifications = user.Notifications?.Select(n => new Notification
            {
                Id = n.Id,
                Message = n.Message,
                Status = n.Status,
                SenderId = n.SenderId,
                RecipientIds = n.RecipientIds
            }).ToList(),
            Payments = user.Payments?.Select(n => new Payment
            {
                Id = n.Id,
                Summa = n.Summa,
                Caption = n.Caption,
                Status = n.Status,
                FilePath = n.FilePath,
                UserId = n.UserId
            }).ToList()
        };
    }
}

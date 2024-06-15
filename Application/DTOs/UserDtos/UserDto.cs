using Domain.Enums;
using Domain.Models;

namespace Application.DTOs.UserDtos;
public class UserDto : AddUserDto
{
    public int Id { get; set; }
    public bool IsVerified { get; set; } = false;
    public int Balance { get; set; } = 4000;
    public int PresentationCount { get; set; } = 0;
    public Payment? TotalPayments { get; set; }
    public List<Notification>? Notifications { get; set; }
    public List<Presentation>? PresentationPaths { get; set; }


    public static implicit operator UserDto(User user)
    {
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
            Notifications = user.Notifications?.Select(n => new Notification
            {
                Id = n.Id,
                Message = n.Message,
                Status = n.Status,
                SenderId = n.SenderId,
                RecipientIds = n.RecipientIds
            }).ToList()
        };
    }
}

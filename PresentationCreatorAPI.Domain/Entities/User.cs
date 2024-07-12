
using System.ComponentModel.DataAnnotations;

namespace PresentationCreatorAPI.Domain.Entites;

public class User : BaseEntity
{
    [Required]
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    [Required]
    public string PhoneNumber { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
    public bool IsVerified { get; set; } = false;
    public int Balance { get; set; } = 4000;
    public int PresentationCount { get; set; } = 0;
    public int? ReferalId { get; set; }
    public bool ReferalBonus { get; set; } = false;
    public Role Role { get; set; } = Role.User;
    public int TotalPayments { get; set; } = 0;
    public int? TelegramId { get; set; }
    public List<Payment>? Payments { get; set; } = new();
    public List<Notification>? Notifications { get; set; } = new();
    public List<Presentation>? Presentations { get; set; }
}

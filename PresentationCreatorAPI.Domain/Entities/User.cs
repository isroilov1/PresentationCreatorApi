namespace PresentationCreatorAPI.Entites;

public class User : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool IsVerified { get; set; } = false;
    public int Balance { get; set; } = 4000;
    public int PresentationCount { get; set; } = 0;
    public int ReferalId { get; set; }
    public bool ReferalBonus { get; set; } = false;
    public Role Role { get; set; } = Role.User;
    public int TotalPayments { get; set; } = 0;
    public int TelegramId { get; set; }
    public List<Payment>? Payments { get; set; } = new();
    public List<Notification>? Notifications { get; set; } = new();
    public List<Presentation>? PresentationPaths { get; set; }
}

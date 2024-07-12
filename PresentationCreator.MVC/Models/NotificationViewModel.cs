using PresentationCreatorAPI.Domain.Entites;
using PresentationCreatorAPI.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PresentationCreator.MVC.Models;

public class NotificationViewModel : BaseEntity
{
    [Required]
    [DisplayName("Xabar matni")]
    public string Message { get; set; } = string.Empty;
    public NotificationStatus Status { get; set; }
    public NotificationType Type { get; set; }
    public int SenderId { get; set; }
    public List<int> RecipientIds { get; set; } = null!;
}

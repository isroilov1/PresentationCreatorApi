using System.ComponentModel.DataAnnotations;

namespace PresentationCreatorAPI.Domain.Entites;

public class Payment : BaseEntity
{
    [Required]
    public int Summa { get; set; }
    public string? Caption { get; set; } = string.Empty;
    public string? AdminCaption { get; set; } = string.Empty;
    [Required]
    public string FilePath { get; set; } = null!;
    public PaymentStatus Status { get; set; } = PaymentStatus.Expected;
    public int UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
}

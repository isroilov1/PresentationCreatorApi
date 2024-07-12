using PresentationCreatorAPI.Application.Common.Helpers;
using PresentationCreatorAPI.Domain.Entites;

namespace PresentationCreatorAPI.Application.DTOs;
public class PaymentDto
{
    public int Id { get; set; }
    public int Summa { get; set; }
    public string? Caption { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public string? AdminCaption { get; set; } = string.Empty;
    public int UserId { get; set; }
    public string CreatedAt { get; set; } = string.Empty;

    public static implicit operator PaymentDto(Payment payment)
    {
        string formattedDate = TimeHelper.TimeFormat(payment.CreatedAt);

        return new PaymentDto
        {
            Id = payment.Id,
            Summa = payment.Summa,
            Caption = payment.Caption,
            Status = payment.Status.ToString(),
            UserId = payment.UserId,
            FilePath = payment.FilePath,
            AdminCaption = payment.AdminCaption,
            CreatedAt = formattedDate
        };
    }
}

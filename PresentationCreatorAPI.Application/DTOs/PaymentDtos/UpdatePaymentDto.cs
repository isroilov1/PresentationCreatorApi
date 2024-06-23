using Microsoft.AspNetCore.Http;
using PresentationCreatorAPI.Domain.Entites;
using PresentationCreatorAPI.Enums;

namespace PresentationCreatorAPI.Application.DTOs;
public class UpdatePaymentDto
{
    public int Id { get; set; }
    public int Summa { get; set; }
    public string Caption { get; set; } = string.Empty;
    public IFormFile File { get; set; } = null!;
    public PaymentStatus Status { get; set; }
    public string AdminCaption { get; set; } = string.Empty;

    public static implicit operator Payment(UpdatePaymentDto dto)
    {
        return new Payment
        {
            Id = dto.Id,
            Summa = dto.Summa,
            Caption = dto.Caption,
            Status = dto.Status,
            AdminCaption = dto.AdminCaption
        };
    }
}

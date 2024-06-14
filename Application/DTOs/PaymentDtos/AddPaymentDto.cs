using Application.Common.Helper;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Application.DTOs;
public class AddPaymentDto
{
    public int Summa { get; set; }
    public string Caption { get; set; } = string.Empty;
    public IFormFile File { get; set; } = null!;
    public int UserId { get; set; }

    public static implicit operator Payment(AddPaymentDto dto)
    {
        // Faylni saqlash (Masalan, "wwwroot/uploads" katalogiga)
        string rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/payments");
        string filePath = FileHelper.SaveFile(dto.File, rootPath);
        return new Payment
        {
            Summa = dto.Summa,
            Caption = dto.Caption,
            FilePath = filePath,
            Status = PaymentStatus.Expected,
            UserId = dto.UserId
        };
    }
}

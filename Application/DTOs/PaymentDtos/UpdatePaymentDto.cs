namespace Application.DTOs;
public class UpdatePaymentDto : AddPaymentDto
{
    public int Id { get; set; }
    public PaymentStatus Status { get; set; }

    public static implicit operator Payment(UpdatePaymentDto dto)
    {
        string rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/payments");
        string filePath = FileHelper.SaveFile(dto.File, rootPath);
        return new Payment
        {
            Id = dto.Id,
            Summa = dto.Summa,
            Caption = dto.Caption,
            FilePath = filePath,
            Status = dto.Status,
            UserId = dto.UserId
        };
    }
}

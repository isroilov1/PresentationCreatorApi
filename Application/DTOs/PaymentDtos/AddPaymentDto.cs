namespace Application.DTOs;
public class AddPaymentDto
{
    public int Summa { get; set; }
    public string Caption { get; set; } = string.Empty;
    public IFormFile File { get; set; } = null!;

    public static implicit operator Payment(AddPaymentDto dto)
    {
        string rootPath = "wwwroot/uploads/payments";
        string filePath = FileHelper.SaveFile(dto.File, rootPath);
        return new Payment
        {
            Summa = dto.Summa,
            Caption = dto.Caption,
            FilePath = filePath,
            Status = PaymentStatus.Expected
        };
    }
}

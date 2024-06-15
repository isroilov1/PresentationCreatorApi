namespace Application.DTOs;
public class PaymentDto
{
    public int Summa { get; set; }
    public string Caption { get; set; } = string.Empty;
    public PaymentStatus Status {  get; set; }
    public string FilePath { get; set; } = string.Empty;
    public int UserId { get; set; }

    public static implicit operator PaymentDto(Payment payment)
    {
        return new PaymentDto
        {
            Summa = payment.Summa,
            Caption = payment.Caption,
            Status = payment.Status,
            UserId = payment.UserId,
            FilePath = payment.FilePath,
        };
    }
}

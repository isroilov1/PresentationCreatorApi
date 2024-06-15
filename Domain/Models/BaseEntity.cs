namespace Domain.Models;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public BaseEntity()
    {
        var tzTashkent = TimeZoneInfo.FindSystemTimeZoneById("Asia/Tashkent");
        CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzTashkent);
    }
}

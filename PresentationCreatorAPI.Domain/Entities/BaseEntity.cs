﻿namespace PresentationCreatorAPI.Domain.Entites;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public BaseEntity()
    {
        var tzTashkent = TimeZoneInfo.FindSystemTimeZoneById("Asia/Tashkent");
        var tashkentTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzTashkent);
        var adjustedTime = tashkentTime.AddHours(-6);
        CreatedAt = adjustedTime;
    }
}

using PresentationCreatorAPI.Entites;

namespace PresentationCreatorAPI.Application.Common.Helpers;

public class TimeHelper
{
    public static string TimeFormat(DateTime time)
    {
        var tzTashkent = TimeZoneInfo.FindSystemTimeZoneById("Asia/Tashkent");
        var tashkentTime = TimeZoneInfo.ConvertTimeFromUtc(time, tzTashkent);
        return tashkentTime.ToString("dd-MM-yyyy HH:mm");
    }
}

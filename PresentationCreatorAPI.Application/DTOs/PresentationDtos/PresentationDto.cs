using PresentationCreatorAPI.Entites;

namespace PresentationCreatorAPI.Application.presntations.Presentationpresntations;

public class PresentationDto
{
    public int Id { get; set; }
    public string Theme { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public byte PageCount { get; set; }
    public int Template { get; set; }
    public string Language { get; set; } = string.Empty;
    public List<Page> Pages { get; set; } = null!;
    public string FilePath { get; set; } = string.Empty;
    public string CreatedAt { get; set; } = string.Empty;

    public static implicit operator PresentationDto(Presentation presntation)
    {
        var tzTashkent = TimeZoneInfo.FindSystemTimeZoneById("Asia/Tashkent");
        var tashkentTime = TimeZoneInfo.ConvertTimeFromUtc(presntation.CreatedAt, tzTashkent);
        string formattedDate = tashkentTime.ToString("dd-MM-yyyy HH");

        return new PresentationDto
        {
            Id = presntation.Id,
            Theme = presntation.Theme,
            Author = presntation.Author,
            PageCount = presntation.PageCount,
            Template = presntation.Template,
            Language = presntation.Language,
            FilePath = presntation.FilePath,
            CreatedAt = formattedDate,
            Pages = presntation.Pages.Select(n => new Page
            {
                Id = n.Id,
                Title = n.Title,
                Text = n.Text,
                ImagesPath = n.ImagesPath,
                PageType = n.PageType,
                PresentationId = n.PresentationId
            }).ToList()
            //Notifications = user.Notifications?.Select(n => new Notification
            //{
            //    Id = n.Id,
            //    Message = n.Message,
            //    Status = n.Status,
            //    SenderId = n.SenderId,
            //    RecipientIds = n.RecipientIds
            //}).ToList(),
        };
    }
}

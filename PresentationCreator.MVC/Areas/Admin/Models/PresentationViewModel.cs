using Application.DTOs.PageDtos;
using PresentationCreatorAPI.Application.Common.Helpers;
using PresentationCreatorAPI.Domain.Entites;

namespace PresentationCreator.MVC.Areas.Admin.Models;

public class PresentationViewModel
{
    public IEnumerable<PresentationDtoMvc> Presentations { get; set; } = null!;
}

public class PresentationDtoMvc
{
    public int Id { get; set; }
    public string Theme { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public byte PageCount { get; set; }
    public int Template { get; set; }
    public string Language { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public string CreatedAt { get; set; } = string.Empty;
    public int UserId { get; set; }
    public List<string> Titles { get; set; } = new List<string>();
    public List<string> ImagesPaths { get; set; } = new List<string>();
    public List<PageDto> Pages { get; set; } = null!;

    public static implicit operator PresentationDtoMvc(Presentation presntation)
    {
        var tzTashkent = TimeZoneInfo.FindSystemTimeZoneById("Asia/Tashkent");
        var tashkentTime = TimeZoneInfo.ConvertTimeFromUtc(presntation.CreatedAt, tzTashkent);
        string formattedDate = TimeHelper.TimeFormat(presntation.CreatedAt);

        return new PresentationDtoMvc
        {
            Id = presntation.Id,
            Theme = presntation.Theme,
            Author = presntation.Author,
            PageCount = presntation.PageCount,
            Template = presntation.Template,
            Language = presntation.Language.ToString(),
            FilePath = presntation.FilePath,
            CreatedAt = formattedDate,
            UserId = presntation.UserId,
            Titles = presntation.Titles,
            ImagesPaths = presntation.ImagesPaths,
            Pages = presntation.Pages.Select(u => (PageDto)u).ToList()
        };
    }
}


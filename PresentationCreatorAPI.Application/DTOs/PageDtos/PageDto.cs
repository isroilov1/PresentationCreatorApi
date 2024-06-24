using PresentationCreatorAPI.Application.Common.Helpers;
using PresentationCreatorAPI.Domain.Entites;

namespace Application.DTOs.PageDtos;

public class PageDto
{
    public int Id { get; set; }
    public byte PageNumber { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;
    public string PageType { get; set; } = string.Empty;
    public string CreatedAt { get; set; } = string.Empty;
    public int PresentationId { get; set; }

    public static implicit operator PageDto(Page page)
    {
        return new PageDto()
        {
            Id = page.Id,
            PageNumber = page.PageNumber,
            Title = page.Title,
            Text = page.Text,
            ImagePath = page.ImagesPath,
            PageType = page.PageType.ToString(),
            PresentationId = page.PresentationId,
            CreatedAt = TimeHelper.TimeFormat(page.CreatedAt)
        };
    }
}

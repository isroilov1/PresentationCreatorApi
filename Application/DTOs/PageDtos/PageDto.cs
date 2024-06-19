namespace Application.DTOs.PageDtos;

public class PageDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;
    public string PageType { get; set; } = string.Empty;
    public int PresentationId { get; set; }

    public static implicit operator PageDto(Page page)
    {
        return new PageDto()
        {
            Id = page.Id,
            Title = page.Title,
            Text = page.Text,
            ImagePath = page.ImagesPath,
            PageType = page.PageType.ToString(),
            PresentationId = page.PresentationId,
        };
    }
}

using PresentationCreatorAPI.Domain.Entites;

namespace PresentationCreatorAPI.Application.DTOs.PageDtos;

public class UpdatePageDto : AddPageDto
{

    public string Text { get; set; } = string.Empty;
    public int Id { get; set; }
    
    public static implicit operator Page(UpdatePageDto dto)
    {
        return new Page()
        {
            Id = dto.Id,
            Title = dto.Title,
            Text = dto.Text,
            PageType = dto.PageType,
            ImagesPath = ""
        };
    }
}

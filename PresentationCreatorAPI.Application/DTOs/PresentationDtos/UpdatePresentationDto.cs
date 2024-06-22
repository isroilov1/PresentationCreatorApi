using PresentationCreatorAPI.Entites;

namespace PresentationCreatorAPI.Application.DTOs.PresentationDtos;

public class UpdatePresentationDto : AddPresentationDto
{
    public int Id { get; set; }
    public static implicit operator Presentation(UpdatePresentationDto dto)
    {
        return new Presentation
        {
            Id = dto.Id,
            Theme = dto.Theme,
            Author = dto.Author,
            PageCount = dto.PageCount,
            Template = dto.Template,
            Language = dto.Language,
        };
    }
}

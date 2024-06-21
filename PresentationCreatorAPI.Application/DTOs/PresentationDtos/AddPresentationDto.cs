using Microsoft.AspNetCore.Http;
using PresentationCreatorAPI.Entites;

namespace PresentationCreatorAPI.Application.DTOs.PresentationDtos;

public class AddPresentationDto
{
    public string Theme { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public byte PageCount { get; set; }
    public int Template { get; set; }
    public string Language { get; set; } = string.Empty;
    public IFormFile File { get; set; } = null!;
    public int UserId { get; set; }

    public static implicit operator Presentation(AddPresentationDto dto)
    {
        return new Presentation
        {
            Theme = dto.Theme,
            Author = dto.Author,
            PageCount = dto.PageCount,
            Template = dto.Template,
            Language = dto.Language,
            UserId = dto.UserId
        };
    }
}

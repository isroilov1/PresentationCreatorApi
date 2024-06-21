using PresentationCreatorAPI.Entites;

namespace PresentationCreatorAPI.Application.DTOs.PageDtos;

public class AddThemePageDto
{
    public string Theme { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    
}

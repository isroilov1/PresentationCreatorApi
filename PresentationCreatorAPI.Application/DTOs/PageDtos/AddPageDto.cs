using Microsoft.AspNetCore.Http;
using PresentationCreatorAPI.Domain.Entites;
using PresentationCreatorAPI.Enums;

namespace PresentationCreatorAPI.Application.DTOs.PageDtos;

public class AddPageDto
{
    public string Title { get; set; } = string.Empty; // request with
    public PresentationPageType PageType { get; set; }  
    public IFormFile Image { get; set; } = null!;
    
    public static implicit operator Page(AddPageDto dto)
    {
        return new Page()
        {
            Title = dto.Title,
            PageType = dto.PageType,
            ImagesPath = ""
        };
    }
}

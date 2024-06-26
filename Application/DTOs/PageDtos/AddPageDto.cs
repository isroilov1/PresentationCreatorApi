﻿namespace Application.DTOs.PageDtos;

public class AddPageDto
{
    public string Title { get; set; } = string.Empty; // request with title
    public IFormFile Image { get; set; } = null!;
    
    public static implicit operator Page(AddPageDto dto)
    {
        return new Page()
        {
            Title = dto.Title,
            ImagesPath = ""
        };
    }
}
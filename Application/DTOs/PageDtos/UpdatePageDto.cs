namespace Application.DTOs.PageDtos;

public class UpdatePageDto : AddPageDto
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;

    public static implicit operator Page(UpdatePageDto dto)
    {
        return new Page()
        {
            Id = dto.Id,
            Title = dto.Title,
            Text = dto.Text,
            ImagesPath = ""
        };
    }
}

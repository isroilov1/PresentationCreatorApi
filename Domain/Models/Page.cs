namespace Domain.Models;

public class Page : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public List<string> ImagesPaths { get; set; } = null!;
    public int PresentationId { get; set; }
    [ForeignKey(nameof(PresentationId))]
    public Presentation Presentation { get; set; } = null!;
}

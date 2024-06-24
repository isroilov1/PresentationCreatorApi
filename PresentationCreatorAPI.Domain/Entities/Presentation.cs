using PresentationCreatorAPI.Domain.Enums;

namespace PresentationCreatorAPI.Domain.Entites;

public class Presentation : BaseEntity
{
    public string Theme { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public byte PageCount { get; set; }
    public int Template { get; set; }
    public PresentationLanguage Language { get; set; }
    public List<Page> Pages { get; set; } = null!;
    public List<string> Titles { get; set; } = null!;
    public List<string> ImagesPaths { get; set; } = null!;
    public string FilePath { get; set; } = string.Empty;
    public int UserId {  get; set; }
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
}

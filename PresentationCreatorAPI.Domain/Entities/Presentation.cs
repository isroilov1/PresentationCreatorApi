using PresentationCreatorAPI.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PresentationCreatorAPI.Domain.Entites;
public class Presentation : BaseEntity
{
    [Required]
    public string Theme { get; set; } = string.Empty;
    [Required]
    public string Author { get; set; } = string.Empty;
    [Required]
    public byte PageCount { get; set; }
    [Required]
    public int Template { get; set; }
    [Required]
    public PresentationLanguage Language { get; set; }
    public List<Page> Pages { get; set; } = null!;
    public List<string> Titles { get; set; } = null!;
    public List<string> ImagesPaths { get; set; } = null!;
    public string FilePath { get; set; } = string.Empty;
    public int UserId {  get; set; }
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
}

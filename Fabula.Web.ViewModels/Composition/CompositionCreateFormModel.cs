namespace Fabula.Web.ViewModels.Composition;

using Tag;
using Genre;
using Attributes;
using Attributes.Enums;
using static Common.ValidationConstants.Shared;
using static Common.ValidationConstants.Composition;

using System.ComponentModel.DataAnnotations;

public class CompositionCreateFormModel
{
    public CompositionCreateFormModel()
    {
        Tags = new HashSet<TagFormModel>();
        Genres = new HashSet<int>();
        GenresToSelect = new HashSet<GenreViewModel>();
    }

    [Required]
    [MinLength(TitleMinLength)]
    [MaxLength(TitleMaxLength)]

    public string Title { get; set; } = null!;

    [Required]
    [MinLength(UrlMinLength)]
    [MaxLength(UrlMaxLength)]

    public string CoverUrl { get; set; } = null!;

    [Required]
    [MinLength(ContentMinLength)]
    [MaxLength(ContentMaxLength)]

    public string Content { get; set; } = null!;

    [Required]
    [MinLength(SynopsysMinLength)]
    [MaxLength(SynopsysMaxLength)]

    public string Synopsys { get; set; } = null!;

    public string? AuthorId { get; set; } = null!;

    public DateTime PublishedOn { get; set; }

    public bool HasAdultContent { get; set; }

    public ICollection<TagFormModel> Tags { get; set; }

    [ElementCount(1, CountStrategy.Minimum,
        ErrorMessage = "A composition must have at least 1 genre!")]

    public IEnumerable<int> Genres { get; set; }

    public IEnumerable<GenreViewModel>? GenresToSelect { get; set; }
}

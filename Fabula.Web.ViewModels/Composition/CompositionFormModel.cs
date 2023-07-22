namespace Fabula.Web.ViewModels.Composition;

using Tag;
using Genre;
using Attributes;
using Attributes.Enums;
using static Common.ValidationConstants.Shared;
using static Common.ValidationConstants.Composition;

using System.ComponentModel.DataAnnotations;

public class CompositionFormModel
{
    public CompositionFormModel()
    {
        Tags = new HashSet<TagFormModel>();
        Genres = new HashSet<int>();
        GenresToSelect = new HashSet<GenreViewModel>();
    }

    public string? Id { get; set; }

    [MinLength(TitleMinLength)]
    [MaxLength(TitleMaxLength)]
    [Required(AllowEmptyStrings = false)]

    public string Title { get; set; } = null!;

    [MinLength(UrlMinLength)]
    [MaxLength(UrlMaxLength)]
    [Required(AllowEmptyStrings = false)]

    public string CoverUrl { get; set; } = null!;

    [MinLength(ContentMinLength)]
    [MaxLength(ContentMaxLength)]
    [Required(AllowEmptyStrings = false)]

    public string Content { get; set; } = null!;

    [MinLength(SynopsisMinLength)]
    [MaxLength(SynopsisMaxLength)]
    [Required(AllowEmptyStrings = false)]

    public string Synopsis { get; set; } = null!;

    public string? AuthorId { get; set; }

    public bool HasAdultContent { get; set; }

    public ICollection<TagFormModel> Tags { get; set; }

    [ElementCount(1, CountStrategy.Minimum,
        ErrorMessage = "A composition must have at least 1 genre!")]

    public IEnumerable<int> Genres { get; set; }

    public IEnumerable<GenreViewModel>? GenresToSelect { get; set; }
}

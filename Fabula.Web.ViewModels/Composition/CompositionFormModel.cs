namespace Fabula.Web.ViewModels.Composition;

using Tag;
using Genre;
using Attributes;
using Attributes.Enums;
using static Common.ErrorMessages.Shared;
using static Common.ErrorMessages.Composition;
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

    [Required(AllowEmptyStrings = false)]
    [StringLength(TitleMaxLength, MinimumLength = TitleMinLength,
        ErrorMessage = StringLengthErrorMessage)]

    public string Title { get; set; } = null!;

    
    [Required(AllowEmptyStrings = false)]
    [StringLength(UrlMaxLength, MinimumLength = UrlMinLength,
        ErrorMessage = StringLengthErrorMessage)]

    public string CoverUrl { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    [StringLength(ContentMaxLength, MinimumLength = ContentMinLength,
        ErrorMessage = StringLengthErrorMessage)]

    public string Content { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    [StringLength(SynopsisMaxLength, MinimumLength = SynopsisMinLength,
        ErrorMessage = StringLengthErrorMessage)]

    public string Synopsis { get; set; } = null!;

    public string? AuthorId { get; set; }

    public bool HasAdultContent { get; set; }

    public ICollection<TagFormModel> Tags { get; set; }

    [ElementCount(1, CountStrategy.Minimum,
        ErrorMessage = GenreCountErrorMessage)]

    public IEnumerable<int> Genres { get; set; }

    public IEnumerable<GenreViewModel>? GenresToSelect { get; set; }
}

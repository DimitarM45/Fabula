namespace Fabula.Web.ViewModels.Composition;

using Web.ViewModels.Tag;
using Web.ViewModels.Genre;
using Core.Infrastructure.Enums;
using Core.Infrastructure.Attributes;
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

    public IEnumerable<int> Genres { get; set; }

    public IEnumerable<GenreViewModel> GenresToSelect { get; set; }
}

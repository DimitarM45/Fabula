namespace Fabula.Web.ViewModels.Composition;

using Web.ViewModels.Tag;
using Web.ViewModels.Genre;
using Core.Infrastructure.Attributes;
using static Common.ValidationConstants.Shared;
using static Common.ValidationConstants.Composition;

using System.ComponentModel.DataAnnotations;

public class CompositionCreateFormModel
{
    public CompositionCreateFormModel()
    {
        Tags = new HashSet<TagFormModel>();
        Genres = new HashSet<GenreViewModel>();
        GenresToSelect = new HashSet<GenreViewModel>();
    }

    [Required]
    [MinLength(TitleMinLength)]
    [MaxLength(TitleMaxLength)]

    public string Title { get; set; } = null!;

    [Required]
    [MinLength(UrlMinLength)]
    [MaxLength(UrlMaxLength)]

    public byte[] CoverPicture { get; set; } = null!;

    [Required]
    [MinLength(ContentMinLength)]
    [MaxLength(ContentMaxLength)]

    public string Content { get; set; } = null!;

    [Required]
    [MinLength(SynopsysMinLength)]
    [MaxLength(SynopsysMaxLength)]

    public string Synopsys { get; set; } = null!;

    [Required]

    public string AuthorId { get; set; } = null!;

    [Required]

    public DateTime PublishedOn { get; set; }

    [Required]

    public bool HasAdultContent { get; set; }

    public ICollection<TagFormModel> Tags { get; set; }

    [ElementCount(1,
        ErrorMessage = "Your composition should have at least 1 genre!")]

    public ICollection<GenreViewModel> Genres { get; set; }

    public IEnumerable<GenreViewModel>? GenresToSelect { get; set; }
}

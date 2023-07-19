namespace Fabula.Data.Models;

using static Common.ValidationConstants.Composition;
using static Common.ValidationConstants.Shared;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

[Comment("User-written compositions")]

public class Composition
{
    public Composition()
    {
        Tags = new HashSet<Tag>();
        Genres = new HashSet<Genre>();
        Ratings = new HashSet<Rating>();
        Comments = new HashSet<Comment>();
        Favorites = new HashSet<UserFavoriteComposition>();
    }

    [Comment("Id of the composition")]

    public Guid Id { get; set; }

    [Required]
    [MaxLength(TitleMaxLength)]
    [Comment("Title of the composition")]

    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(UrlMaxLength)]
    [Comment("A url which leads to the composition's cover art")]

    public string CoverUrl { get; set; } = null!;

    [Required]
    [MaxLength(ContentMaxLength)]
    [Comment("The composition itself")]

    public string Content { get; set; } = null!;

    [Required]
    [MaxLength(SynopsysMaxLength)]
    [Comment("Synopsys of the composition")]

    public string Synopsys { get; set; } = null!;

    [Required]
    [Comment("Id of composition author")]

    public Guid AuthorId { get; set; }

    public ApplicationUser Author { get; set; } = null!;

    [Required]
    [Comment("Date of publishing")]

    public DateTime PublishedOn { get; set; }

    [Comment("Date and time of deletion of the composition. " +
        "Note: A nullable type is used for the purposes of documenting " +
        "both whether a composition has been deleted and also when the operation took place.")]

    public DateTime? DeletedOn { get; set; }

    [Required]
    [Comment("Adult content flag of the composition")]

    public bool hasAdultContent { get; set; }

    public IEnumerable<Tag> Tags { get; set; }

    public IEnumerable<Genre> Genres { get; set; }

    public IEnumerable<Rating> Ratings { get; set; }

    public IEnumerable<Comment> Comments { get; set; }

    public IEnumerable<UserFavoriteComposition> Favorites { get; set; }
}

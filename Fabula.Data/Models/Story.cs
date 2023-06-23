namespace Fabula.Data.Models;

using static Common.ValidationConstants.Story;
using static Common.ValidationConstants.Shared;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

[Comment("User-written stories")]

public class Story
{
    public Story()
    {
        Id = Guid.NewGuid();

        Genres = new HashSet<Genre>();
        Likes = new HashSet<UsersLikedStories>();
    }

    public Guid Id { get; set; }

    [Required]
    [MaxLength(TitleMaxLength)]
    [Comment("Title of the story")]

    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(UrlMaxLength)]
    [Comment("A url which leads to the story's cover art")]

    public string CoverUrl { get; set; } = null!;

    [Required]
    [MaxLength(ContentMaxLength)]
    [Comment("The story itself")]

    public string Content { get; set; } = null!;

    [Required]
    [MaxLength(SynopsysMaxLength)]
    [Comment("Synopsys of the story")]

    public string Synopsys { get; set; } = null!;

    [Required]

    public string AuthorId { get; set; } = null!;

    public ApplicationUser Author { get; set; } = null!;

    [Required]
    [Comment("Date of publishing")]

    public DateTime PublishedOn { get; set; }

    [Comment("Date and time of deletion of the story. " +
        "Note: A nullable type is used for the purposes of documenting " +
        "both whether a story has been deleted and also when the operation took place.")]

    public DateTime? DeletedOn { get; set; }

    [Required]
    [Comment("Users who've liked a story")]

    public IEnumerable<UsersLikedStories> Likes { get; set; }

    public IEnumerable<Genre> Genres { get; set; }
}

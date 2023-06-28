namespace Fabula.Data.Models;

using static Common.ValidationConstants.Piece;
using static Common.ValidationConstants.Shared;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

[Comment("User-written pieces")]

public class Piece
{
    public Piece()
    {
        Tags = new HashSet<Tag>();
        Genres = new HashSet<Genre>();
        Ratings = new HashSet<Rating>();
        Comments = new HashSet<Comment>();
        Favorites = new HashSet<UserFavoritePiece>();
    }

    [Comment("Id of the piece")]

    public Guid Id { get; set; }

    [Required]
    [MaxLength(TitleMaxLength)]
    [Comment("Title of the piece")]

    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(UrlMaxLength)]
    [Comment("A url which leads to the piece's cover art")]

    public string CoverUrl { get; set; } = null!;

    [Required]
    [MaxLength(ContentMaxLength)]
    [Comment("The piece itself")]

    public string Content { get; set; } = null!;

    [Required]
    [MaxLength(SynopsysMaxLength)]
    [Comment("Synopsys of the piece")]

    public string Synopsys { get; set; } = null!;

    [Required]
    [Comment("Id of piece author")]

    public Guid AuthorId { get; set; }

    public ApplicationUser Author { get; set; } = null!;

    [Required]
    [Comment("Date of publishing")]

    public DateTime PublishedOn { get; set; }

    [Comment("Date and time of deletion of the piece. " +
        "Note: A nullable type is used for the purposes of documenting " +
        "both whether a piece has been deleted and also when the operation took place.")]

    public DateTime? DeletedOn { get; set; }

    [Required]
    [Comment("Adult content flag of the piece")]

    public bool hasAdultContent { get; set; }

    [Required]
    [Comment("Id of form")]

    public int FormId { get; set; }

    [Required]

    public Form Form { get; set; } = null!;

    public IEnumerable<Tag> Tags { get; set; }

    public IEnumerable<Genre> Genres { get; set; }

    public IEnumerable<Rating> Ratings { get; set; }

    public IEnumerable<Comment> Comments { get; set; }

    public IEnumerable<UserFavoritePiece> Favorites { get; set; }
}

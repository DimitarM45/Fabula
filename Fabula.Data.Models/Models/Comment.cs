namespace Fabula.Data.Models;

using static Common.ValidationConstants.Comment;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

[Comment("Comments made by users")]

public class Comment
{
    public Comment()
    {
        Likes = new HashSet<UserLikedComment>();
    }

    [Comment("Id of the comment")]

    public Guid Id { get; set; }

    [Required]
    [MaxLength(ContentMaxLength)]
    [Comment("Content of the comment")]

    public string Content { get; set; } = null!;

    [Required]
    [Comment("Id of the comment author")]

    public Guid AuthorId { get; set; }

    [Required]

    public ApplicationUser Author { get; set; } = null!;

    [Required]
    [Comment("Id of composition")]

    public Guid CompositionId { get; set; }

    [Required]

    public Composition Composition { get; set; } = null!;

    [Required]
    [Comment("Date of publishing")]

    public DateTime PublishedOn { get; set; }

    [Comment("Date and time of deletion of the comment. " +
        "Note: A nullable type is used for the purposes of documenting " +
        "both whether a comment has been deleted and also when the operation took place.")]

    public DateTime? DeletedOn { get; set; }

    public IEnumerable<UserLikedComment> Likes { get; set; }

    public IEnumerable<Comment> Replies { get; set; }
}

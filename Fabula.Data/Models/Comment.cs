namespace Fabula.Data.Models;

using static Common.ValidationConstants.Comment;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

[Comment("Comments made by users")]

public class Comment
{
    public Comment()
    {
        Id = Guid.NewGuid();

        Likes = new HashSet<UsersLikedComments>();
    }

    [Comment("Id of the comment")]

    public Guid Id { get; set; }

    [Required]
    [MaxLength(ContentMaxLength)]
    [Comment("Content of the comment")]

    public string Content { get; set; } = null!;

    [Required]
    [Comment("Id of the comment author")]

    public string AuthorId { get; set; } = null!;

    public ApplicationUser Author { get; set; } = null!;

    [Required]
    [Comment("Date of publishing")]

    public DateTime PublishedOn { get; set; }

    [Comment("Date and time of deletion of the comment. " +
        "Note: A nullable type is used for the purposes of documenting " +
        "both whether a comment has been deleted and also when the operation took place.")]

    public DateTime? DeletedOn { get; set; }

    public IEnumerable<UsersLikedComments> Likes { get; set; }
}

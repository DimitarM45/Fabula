namespace Fabula.Data.Models;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

[Comment("Mapping table for users and the comments they've liked")]

public class UserLikedComment
{
    [Required]
    [Comment("Id of user")]

    public Guid UserId { get; set; }

    [Required]

    public ApplicationUser User { get; set; } = null!;

    [Required]
    [Comment("Id of comment")]

    public Guid CommentId { get; set; }

    [Required]

    public Comment Comment { get; set; } = null!;
}

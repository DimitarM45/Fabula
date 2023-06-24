namespace Fabula.Data.Models;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

[Comment("Mapping table for users and the comments they've liked")]

public class UsersLikedComments
{
    [Required]
    [Comment("Id of user")]

    public string UserId { get; set; } = null!;

    public ApplicationUser User { get; set; } = null!;

    [Required]
    [Comment("Id of comment")]

    public Guid CommentId { get; set; }

    public Comment Comment { get; set; } = null!;
}

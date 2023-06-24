namespace Fabula.Data.Models;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

[Comment("Mapping table for users and the posts they've liked")]

public class UsersLikedPosts
{
    [Required]
    [Comment("Id of user")]

    public string UserId { get; set; } = null!;

    public ApplicationUser User { get; set; } = null!;

    [Required]
    [Comment("Id of post")]

    public Guid PostId { get; set; }

    public Post Post { get; set; } = null!;
}

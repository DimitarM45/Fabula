namespace Fabula.Data.Models;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

[Comment("Mapping table for users and the lists they've liked")]

public class UserLikedList
{
    [Required]
    [Comment("Id of user")]

    public Guid UserId { get; set; }

    [Required]

    public ApplicationUser User { get; set; } = null!;

    [Required]
    [Comment("Id of list")]

    public Guid ListId { get; set; }

    [Required]

    public List List { get; set; } = null!;
}

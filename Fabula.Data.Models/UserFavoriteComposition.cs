namespace Fabula.Data.Models;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

[Comment("Mapping table for users and the compositions they've favorited")]

public class UserFavoriteComposition
{
    [Required]
    [Comment("Id of user")]

    public Guid UserId { get; set; }

    [Required]

    public ApplicationUser User { get; set; } = null!;

    [Required]
    [Comment("Id of composition")]

    public Guid CompositionId { get; set; }

    [Required]

    public Composition Composition { get; set; } = null!;
}

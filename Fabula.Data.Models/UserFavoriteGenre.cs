namespace Fabula.Data.Models;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

[Comment("Mapping table for users and the genres they've favorited")]

public class UserFavoriteGenre
{
    [Required]
    [Comment("Id of user")]

    public Guid UserId { get; set; }

    [Required]

    public ApplicationUser User { get; set; } = null!;

    [Required]
    [Comment("Id of genre")]

    public int GenreId { get; set; }

    [Required]

    public Genre Genre { get; set; } = null!;
}

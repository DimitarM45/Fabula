namespace Fabula.Data.Models;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

[Comment("Mapping table for users and the pieces they've favorited")]

public class UserFavoritePiece
{
    [Required]
    [Comment("Id of user")]

    public Guid UserId { get; set; }

    [Required]

    public ApplicationUser User { get; set; } = null!;

    [Required]
    [Comment("Id of piece")]

    public Guid PieceId { get; set; }

    [Required]

    public Piece Piece { get; set; } = null!;
}

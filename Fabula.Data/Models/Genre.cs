namespace Fabula.Data.Models;

using static Common.ValidationConstants.Subgenre;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

[Comment("Genre of a piece")]

public class Genre
{
    public Genre()
    {
        Stories = new HashSet<Piece>();
    }

    [Required]
    [Comment("Id of genre")]

    public int Id { get; set; }

    [Required]
    [MaxLength(NameMaxLength)]
    [Comment("Name of genre")]

    public string Name { get; set; } = null!;

    public IEnumerable<Piece> Stories { get; set; }
}

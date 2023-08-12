namespace Fabula.Data.Models;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

[Comment("Mapping table for compositions and their genres")]

public class CompositionGenre
{
    [Required]
    [Comment("Id of composition")]

    public Guid CompositionId { get; set; }

    [Required]

    public Composition Composition { get; set; } = null!;

    [Required]
    [Comment("Id of genre")]

    public int GenreId { get; set; }

    [Required]

    public Genre Genre { get; set; } = null!;
}

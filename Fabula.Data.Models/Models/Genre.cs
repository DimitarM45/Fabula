namespace Fabula.Data.Models;

using static Common.ValidationConstants.Genre;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

[Comment("Genre of a composition")]

public class Genre
{
    public Genre()
    {
        Compositions = new HashSet<Composition>();
    }

    [Required]
    [Comment("Id of genre")]

    public int Id { get; set; }

    [Required]
    [MaxLength(NameMaxLength)]
    [Comment("Name of genre")]

    public string Name { get; set; } = null!;

    public IEnumerable<Composition> Compositions { get; set; }

    public IEnumerable<ApplicationUser> Favorites { get; set; }
}

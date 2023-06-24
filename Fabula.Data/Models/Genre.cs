namespace Fabula.Data.Models;

using static Common.ValidationConstants.Genre;
using static Common.ValidationConstants.Shared;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

[Comment("Story genres")]

public class Genre
{
    public Genre()
    {
        Stories = new HashSet<Story>();
    }

    [Required]
    [Comment("Id of the genre")]

    public int Id { get; set; }

    [Required]
    [MaxLength(NameMaxLength)]
    [Comment("Name of genre")]

    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(UrlMaxLength)]
    [Comment("Picture for genre page")]

    public string PictureUrl { get; set; } = null!;

    public IEnumerable<Story> Stories { get; set; }
}

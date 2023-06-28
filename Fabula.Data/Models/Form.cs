namespace Fabula.Data.Models;

using static Common.ValidationConstants.Form;
using static Common.ValidationConstants.Shared;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

[Comment("Literary forms (e.g. prose, poetry")]

public class Form
{
    public Form()
    {
        Pieces = new HashSet<Piece>();
    }

    [Required]
    [Comment("Id of the form")]

    public int Id { get; set; }

    [Required]
    [MaxLength(NameMaxLength)]
    [Comment("Name of form")]

    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(UrlMaxLength)]
    [Comment("Picture that loosely describes form")]

    public string PictureUrl { get; set; } = null!;

    public IEnumerable<Piece> Pieces { get; set; }

    // TODO Maybe add a relation to subgenre
}

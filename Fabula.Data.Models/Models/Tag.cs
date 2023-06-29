namespace Fabula.Data.Models;

using static Common.ValidationConstants.Tag;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

[Comment("Tags for better categorisation of literary works")]

public class Tag
{
    public Tag()
    {
        Pieces = new HashSet<Piece>();    
    }

    [Required]
    [Comment("Id of tag")]

    public int Id { get; set; }

    [Required]
    [Comment("Name of tag")]
    [MaxLength(NameMaxLength)]

    public string Name { get; set; } = null!;

    public IEnumerable<Piece> Pieces { get; set; }
}

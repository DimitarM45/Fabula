namespace Fabula.Data.Models;

using static Common.ValidationConstants.Tag;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

[Comment("Tags for better categorisation of literary works")]

public class Tag
{
    public Tag()
    {
        Compositions = new HashSet<Composition>();    
    }

    [Required]
    [Comment("Id of tag")]

    public int Id { get; set; }

    [Required]
    [Comment("Name of tag")]
    [MaxLength(NameMaxLength)]

    public string Name { get; set; } = null!;

    public IEnumerable<Composition> Compositions { get; set; }
}

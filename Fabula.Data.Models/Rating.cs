namespace Fabula.Data.Models;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

[Comment("Rating of composition")]

public class Rating
{
    [Required]
    [Comment("Id of rating")]

    public Guid Id { get; set; }

    [Required]
    [Comment("Value of rating")]

    public byte Value { get; set; }

    [Required]
    [Comment("Date of publishing")]

    public DateTime PublishedOn { get; set; }

    [Comment("Date and time of deletion of the rating. " +
        "Note: A nullable type is used for the purposes of documenting " +
        "both whether a rating has been deleted and also when the operation took place.")]

    public DateTime? DeletedOn { get; set; }

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
